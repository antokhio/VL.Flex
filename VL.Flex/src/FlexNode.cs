using System.Runtime.Intrinsics.X86;
using VL.Flex.Internals;
using VL.Lib.Collections;
using YogaSharp;
using static VL.Flex.Internals.Delegates;

namespace VL.Flex
{
    public abstract class FlexBase : IDisposable
    {
        protected unsafe YGNode* _handle = YGNode.New();
        /// <summary>
        /// Node pointer.
        /// </summary>
        public abstract unsafe YGNode* Handle { get; }

        protected Spread<FlexBase?>? _children = null;
        /// <summary>
        /// Node children.
        /// </summary>
        public abstract Spread<FlexBase?>? Children { internal get; set; }

        protected IFlexStyle? _style;
        /// <summary>
        /// Node style.
        /// </summary>
        public abstract IFlexStyle? Style { internal get; set; }

        #region Layout
        protected FlexLayout _layout;
        /// <summary>
        /// Node calculated Layout
        /// </summary>
        public abstract FlexLayout Layout { get; set; }
        /// <summary>
        /// Applies parent layout to node layout
        /// </summary>
        public abstract void ApplyLayout(FlexLayoutArgs args, FlexLayout? ownerLayout);
        /// <summary>
        /// Event fired on layout change, used in Breakpoints to conditionally change style.
        /// </summary>
        public abstract event Action<FlexLayoutArgs>? OnLayoutChanged;
        /// <summary>
        /// Node layout should be re-calculated. 
        /// </summary>
        public abstract bool IsDirty { get; }
        /// <summary>
        /// Should apply new layout. 
        /// </summary>
        public abstract bool HasNewLayout { internal get; set; }
        /// <summary>
        /// Calculates layout using this node as a root - upstream. 
        /// </summary>
        public abstract void CalculateLayout(float? ownerWidth, float? ownerHeight, YGDirection? ownerDirection);
        #endregion
        /// <summary>
        /// Used to calculate layout from current node to root, deprecated.
        /// </summary>
        #region Owner
        protected WeakReference<FlexBase>? _owner = null;
        /// <summary>
        /// Sets owner.
        /// </summary>
        public abstract void SetOwner(FlexNode node);
        /// <summary>
        /// Traverse to tree root. 
        /// </summary>
        public abstract FlexBase GetRoot();
        #endregion

        /* Measure Func
        *  Since we can pass measure function as static only, 
        *  we need to create node repository.
        */
        protected FlexBase()
        {
            Store.GetRegistry().AddNode(this);
        }

        // MeasureFunc
        protected MeasureFunc? _measureFunc;
        protected nint _measureFuncPtr;
        public abstract MeasureFunc? MeasureFunc { internal get; set; }
        public abstract bool HasMeasureFunc();

        // BaselineFunc
        protected BaselineFunc? _baselineFunc;
        protected nint _baselineFuncPtr;
        public abstract BaselineFunc? BaselineFunc { internal get; set; }
        public abstract bool HasBaselineFunc();

        // Node Context
        // Basically anything we want to pass to measure func.
        protected object? _nodeContext;
        public abstract object? NodeContext { internal get; set; }

        // Dispose
        public abstract void Dispose();

    }

    public class FlexNode : FlexBase
    {
        public override unsafe YGNode* Handle => _handle;
        public override unsafe bool IsDirty => _handle->IsDirty();
        public override Spread<FlexBase?>? Children
        {
            internal get => _children;
            set
            {
                if (value != _children)
                {
                    var validChildren = value?.Where(child => child != null).ToSpread();
                    var validChildrenCount = validChildren?.Count;

                    if (validChildren is not null && validChildrenCount > 0)
                    {
                        unsafe
                        {
                            YGNode*[] refs = new YGNode*[validChildrenCount.Value];
                            for (int i = 0; i < validChildrenCount; i++)
                            {
                                refs[i] = validChildren[i]!.Handle;
                                validChildren[i]!.SetOwner(this);
                            }

                            _handle->SetChildren(refs);
                        }
                    }
                    else
                    {
                        unsafe
                        {
                            _handle->RemoveAllChildren();
                        }
                    }

                    _children = value;
                }
            }
        }

        public override IFlexStyle? Style
        {
            internal get => _style;
            set
            {
                if (value != _style)
                {
                    unsafe
                    {
                        _handle->CopyStyle(Store.GetInternals().BulkNode);
                    }

                    value?.ApplyStyle(this);
                    _style = value;
                }
            }
        }

        public override unsafe bool HasNewLayout
        {
            internal get => _handle->GetHasNewLayout();
            set
            {
                _handle->SetHasNewLayout(value);
            }
        }

        public override FlexLayout Layout
        {
            get => _layout;
            set => _layout = value;
        }

        public override event Action<FlexLayoutArgs>? OnLayoutChanged;
        public override void ApplyLayout(FlexLayoutArgs args, FlexLayout? ownerLayout = null)
        {
            if (HasNewLayout)
            {
                Layout = new FlexLayout(this, ownerLayout);
                Children?.ForEach(c =>
                {
                    c?.ApplyLayout(args, Layout);
                });

                OnLayoutChanged?.Invoke(args);
            }

            HasNewLayout = false;
        }

        public override void CalculateLayout(float? ownerWidth, float? ownerHeight, YGDirection? ownerDirection)
        {
            unsafe
            {
                _handle->CalculateLayout(ownerWidth ?? float.NaN, ownerHeight ?? float.NaN, ownerDirection ?? YGDirection.LTR);
            }

            ApplyLayout(new FlexLayoutArgs(ownerWidth, ownerHeight, ownerDirection));
        }

        public override void SetOwner(FlexNode node) => _owner = node;
        public override FlexBase GetRoot() => _owner == null ? this : _owner.GetRoot();

        public override MeasureFunc? MeasureFunc
        {
            internal get => _measureFunc;
            set
            {
                if (_measureFunc != value)
                {
                    if (value != null)
                    {
                        unsafe
                        {
                            _measureFuncPtr = (nint)(delegate* unmanaged[Cdecl]<YGNode*, float, YGMeasureMode, float, YGMeasureMode, YGSize>)&MeasureFuncAdapter;
                            Handle->SetMeasureFunc(_measureFuncPtr);
                        }
                    }
                    else
                    {
                        unsafe
                        {
                            _measureFuncPtr = 0;
                            Handle->SetMeasureFunc(_measureFuncPtr);
                        }
                    }
                    _measureFunc = value;
                }
            }
        }
        public override bool HasMeasureFunc()
        {
            unsafe
            {
                return _handle->HasMeasureFunc();
            }
        }

        public override BaselineFunc? BaselineFunc { 
            internal get => _baselineFunc;
            set
            {

                if (_baselineFunc != value)
                {
                    if (value != null) {
                        unsafe
                        {
                            _baselineFuncPtr = (nint)(delegate* unmanaged[Cdecl]<YGNode*, float, float, float>)&BaselineFuncAdapter;
                            Handle->SetBaselineFunc(_baselineFuncPtr);
                        }
                    } else
                    {
                        unsafe
                        {
                            _baselineFuncPtr = 0;
                            Handle->SetBaselineFunc(_baselineFuncPtr);
                        }
                    }
                }
            }
        }

        public override bool HasBaselineFunc()
        {
            unsafe
            {
                return Handle->HasBaselineFunc();
            }
        }

        public override object? NodeContext
        {
            internal get => _nodeContext;
            set => _nodeContext = value;
        }

        public override void Dispose()
        {
            unsafe
            {
                if (_handle != null)
                {
                    _measureFuncPtr = 0;
                    _baselineFuncPtr = 0;

                    Store.GetRegistry().RemoveNode(_handle);
                    _handle->FinalizeNode();
                }

                GC.SuppressFinalize(this);
            }
        }
    }
}

