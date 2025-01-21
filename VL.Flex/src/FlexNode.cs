using VL.Flex.Internals;
using VL.Lib.Collections;
using YogaSharp;
using static VL.Flex.Internals.Delegates;

namespace VL.Flex
{
    /// <summary>
    /// Base class for handling Flex Layouts
    /// </summary>
    public class FlexNode : IDisposable
    {
        #region NodePtr
        protected unsafe YGNode* _handle = YGNode.New();
        /// <summary>
        /// Yoga Node Pointer.
        /// </summary>
        public unsafe virtual YGNode* Handle => _handle;
        #endregion

        #region Children
        protected Spread<FlexNode?>? _children = null;
        /// <summary>
        /// FlexNode Children.
        /// </summary>
        public virtual Spread<FlexNode?>? Children
        {
            internal get => _children;
            set
            {
                if (value != _children)
                {
                    var validChildren = value?.Where(child => child != null).ToSpread();
                    var validChildrenCount = validChildren?.Count;

                    if (validChildren != null && validChildrenCount > 0)
                    {
                        unsafe
                        {
                            YGNode*[] refs = new YGNode*[validChildrenCount.Value];
                            for (int i = 0; i < validChildrenCount; i++)
                            {
                                refs[i] = validChildren[i]!.Handle;
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
        #endregion

        #region Style
        protected IFlexStyle? _style;

        /// <summary>
        /// FlexNode Style.
        /// </summary>
        public virtual IFlexStyle? Style
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
        #endregion

        #region Layout
        protected FlexLayout _layout;

        /// <summary>
        /// Node calculated Layout
        /// </summary>
        public virtual FlexLayout Layout
        {
            get => _layout;
            set => _layout = value;
        }

        /// <summary>
        /// Event fired on layout change, used in Breakpoints to conditionally change style.
        /// </summary>
        public virtual event Action<FlexLayoutArgs>? OnLayoutChanged;

        /// <summary>
        /// Applies parent layout to node layout
        /// </summary>
        public virtual void ApplyLayout(FlexLayoutArgs args, FlexLayout? ownerLayout = null)
        {
            if (HasNewLayout)
            {
                Layout = new FlexLayout(this, ownerLayout);
                Children?.ForEach(child =>
                {
                    child?.ApplyLayout(args, Layout);
                });

                OnLayoutChanged?.Invoke(args);
            }

            HasNewLayout = false;
        }

        /// <summary>
        /// Node layout should be re-calculated. 
        /// </summary>
        public virtual bool IsDirty
        {
            get
            {
                unsafe
                {
                    return _handle->IsDirty();
                }
            }
        }

        /// <summary>
        /// Marks a node with custom measure function as dirty.
        /// </summary>
        public virtual void MarkDirty()
        {
            unsafe
            {
                _handle->MarkDirty();
            }
        }

        /// <summary>
        /// Should apply new layout. 
        /// </summary>
        public virtual unsafe bool HasNewLayout
        {
            internal get => _handle->GetHasNewLayout();
            set
            {
                _handle->SetHasNewLayout(value);
            }
        }

        /// <summary>
        /// Calculates layout using this node as a root - upstream. 
        /// </summary>
        public virtual void CalculateLayout(float? ownerWidth, float? ownerHeight, YGDirection? ownerDirection)
        {
            unsafe
            {
                _handle->CalculateLayout(ownerWidth ?? float.NaN, ownerHeight ?? float.NaN, ownerDirection ?? YGDirection.LTR);
            }

            ApplyLayout(new FlexLayoutArgs(ownerWidth, ownerHeight, ownerDirection));
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Since delegates are static methods, we need to store pointers somewhere
        /// </summary>
        public FlexNode()
        {
            Store.GetRegistry().RegisterNode(this);
        }
        #endregion

        #region MeasureFunc
        protected MeasureFunc? _measureFunc;
        protected nint _measureFuncPtr;

        /// <inheritdoc cref="Internals.Delegates.MeasureFunc"/>
        public virtual MeasureFunc? MeasureFunc
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

        /// <summary>
        /// Whether a measure function is set.
        /// </summary>
        public virtual bool HasMeasureFunc
        {
            get
            {
                unsafe
                {
                    return _handle->HasMeasureFunc();
                }
            }
        }
        #endregion

        #region BaselineFunc
        protected BaselineFunc? _baselineFunc;
        protected nint _baselineFuncPtr;

        /// <inheritdoc cref="Internals.Delegates.BaselineFunc"/>
        public virtual BaselineFunc? BaselineFunc
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
        /// <summary>
        ///  Whether a baseline function is set.
        /// </summary>
        public virtual bool HasBaselineFunc
        {
            get
            {
                unsafe
                {
                    return Handle->HasBaselineFunc();
                }
            }
        }

        protected bool? _isReferenceBaseline;

        /// <summary>
        /// Node should be considered the reference baseline among siblings.
        /// </summary>
        public virtual bool? IsReferenceBaseline
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
        #endregion

        #region Misc Props
        protected YGNodeType? _nodeType;

        /// <summary>
        /// Sets whether a leaf node's layout results may be truncated during layout rounding.
        /// </summary>
        public virtual YGNodeType? NodeType
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

        protected bool? _alwaysFormsContainingBlock;

        /// <summary>
        /// Make it so that this node will always form a containing block for any descendant nodes.<br/>
        /// This is useful for when a node has a property outside of of Yoga that will form a containing block.<br/>
        /// For example, transforms or some of the others listed in <see href="https://developer.mozilla.org/en-US/docs/Web/CSS/Containing_block"/>
        /// </summary>
        public virtual bool? AlwaysFormsContainingBlock
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
        #endregion

        #region NodeContext

        /// <summary>
        /// Basically anything (object), that we want to pass to measure func, baseline func.
        /// </summary>
        protected object? _nodeContext;
        public virtual object? NodeContext
        {
            internal get => _nodeContext;
            set => _nodeContext = value;
        }
        #endregion

        #region Dispose
        public virtual void Dispose()
        {
            unsafe
            {
                if (_handle != null)
                {
                    Store.GetRegistry().UnregisterNode(_handle);

                    _measureFuncPtr = 0;
                    _baselineFuncPtr = 0;

                    _handle->FinalizeNode();
                }

                GC.SuppressFinalize(this);
            }
        }
        #endregion  
    }
}

