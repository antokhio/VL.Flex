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
        /// Node Pointer.
        /// </summary>
        public abstract unsafe YGNode* Handle { get; }

        protected Spread<FlexBase?>? _children = null;
        /// <summary>
        /// Node Children.
        /// </summary>
        public abstract Spread<FlexBase?>? Children { internal get; set; }

        protected IFlexStyle? _style;

        /// <summary>
        /// Node Style.
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
        /// Marks a node with custom measure function as dirty.
        /// </summary>
        public abstract void MarkDirty();

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
        protected WeakReference? _owner = null;

        /// <summary>
        /// Sets owner.
        /// </summary>
        public abstract void SetOwner(FlexNode node);

        /// <summary>
        /// Traverse to tree root. 
        /// </summary>
        public abstract FlexBase? GetRoot();
        #endregion

        /// <summary>
        /// Since delegates are static methods, we need to store pointers somewhere
        /// </summary>
        protected FlexBase()
        {
            Store.GetRegistry().AddNode(this);
        }

        #region MeasureFunc
        protected MeasureFunc? _measureFunc;
        protected nint _measureFuncPtr;

        /// <inheritdoc cref="Internals.Delegates.MeasureFunc"/>
        public abstract MeasureFunc? MeasureFunc { internal get; set; }

        /// <summary>
        /// Whether a measure function is set.
        /// </summary>
        public abstract bool HasMeasureFunc();
        #endregion

        #region BaselineFunc
        protected BaselineFunc? _baselineFunc;
        protected nint _baselineFuncPtr;

        /// <inheritdoc cref="Internals.Delegates.BaselineFunc"/>
        public abstract BaselineFunc? BaselineFunc { internal get; set; }
        public abstract bool HasBaselineFunc();

        protected bool? _isReferenceBaseline;

        /// <summary>
        /// Node should be considered the reference baseline among siblings.
        /// </summary>
        public abstract bool? IsReferenceBaseline { internal get; set; }
        #endregion

        #region Misc Props
        protected YGNodeType? _nodeType;

        /// <summary>
        /// Sets whether a leaf node's layout results may be truncated during layout rounding.
        /// </summary>
        public abstract YGNodeType? NodeType { internal get; set; }

        protected bool? _alwaysFormsContainingBlock;

        /// <summary>
        /// Make it so that this node will always form a containing block for any descendant nodes.<br/>
        /// This is useful for when a node has a property outside of of Yoga that will form a containing block.<br/>
        /// For example, transforms or some of the others listed in <see href="https://developer.mozilla.org/en-US/docs/Web/CSS/Containing_block"/>
        /// </summary>
        public abstract bool? AlwaysFormsContainingBlock { internal get; set; }
        #endregion

        #region NodeContext

        /// <summary>
        /// Basically anything (object), that we want to pass to measure func, baseline func.
        /// </summary>
        protected object? _nodeContext;
        public abstract object? NodeContext { internal get; set; }
        #endregion
        public abstract void Dispose();
    }

    public class FlexNode : FlexBase
    {
        public override unsafe YGNode* Handle => _handle;
        public override bool IsDirty
        {
            get
            {
                unsafe
                {
                    return _handle->IsDirty();
                }
            }
        }

        public override void MarkDirty()
        {
            unsafe
            {
                _handle->MarkDirty();
            }
        }

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

        public override void SetOwner(FlexNode node) => _owner = new WeakReference(node, false);
        public override FlexBase? GetRoot() => _owner == null ? this : (_owner.Target as FlexBase)?.GetRoot();

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

        public override BaselineFunc? BaselineFunc
        {
            internal get => _baselineFunc;
            set
            {

                if (_baselineFunc != value)
                {
                    if (value != null)
                    {
                        unsafe
                        {
                            _baselineFuncPtr = (nint)(delegate* unmanaged[Cdecl]<YGNode*, float, float, float>)&BaselineFuncAdapter;
                            Handle->SetBaselineFunc(_baselineFuncPtr);
                        }
                    }
                    else
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

        public override bool? IsReferenceBaseline
        {
            internal get
            {
                unsafe
                {
                    _isReferenceBaseline = Handle->IsReferenceBaseline();
                }
                return _isReferenceBaseline;
            }
            set
            {
                if (_isReferenceBaseline != value)
                {
                    unsafe
                    {
                        Handle->SetIsReferenceBaseline(value ?? false);
                    }

                    _isReferenceBaseline = value;
                }
            }
        }

        public override bool? AlwaysFormsContainingBlock
        {
            internal get
            {
                unsafe
                {
                    _alwaysFormsContainingBlock = Handle->GetAlwaysFormsContainingBlock();
                }
                return _alwaysFormsContainingBlock;
            }
            set
            {
                if (_alwaysFormsContainingBlock != value)
                {
                    unsafe
                    {
                        Handle->SetAlwaysFormsContainingBlock(value ?? false);
                    }

                    _alwaysFormsContainingBlock = value;
                }
            }
        }

        public override YGNodeType? NodeType
        {
            internal get
            {
                unsafe
                {
                    _nodeType = Handle->GetNodeType();
                }
                return _nodeType;
            }
            set
            {
                if (_nodeType != value)
                {
                    unsafe
                    {
                        Handle->SetNodeType(value ?? YGNodeType.Default);
                    }

                    _nodeType = value;
                }
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

