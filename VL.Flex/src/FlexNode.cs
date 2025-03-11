using Stride.Core.Mathematics;
using VL.Core;
using VL.Flex.Internals;
using VL.Lib.Collections;
using YogaSharp;
using static VL.Flex.Internals.Delegates;

namespace VL.Flex;

/// <summary>
/// Arguments used in layout calculation, passed to breakpoints
/// </summary>
public record struct FlexCalculateLayoutArgs(RectangleF? OwnerBounds, YGDirection? OwnerDirection) { }

public abstract class FlexNodeBase<TNode, TNodeStyle> : IDisposable
{
    #region IntPtr
    protected unsafe YGNode* _handle = YGNode.New();
    /// <summary>
    /// Yoga Node Pointer.
    /// </summary>
    public unsafe YGNode* Handle { get => _handle; }
    #endregion

    #region Abstract TNode, TNodeStyle
    /// <summary>
    /// FlexNodeBase Abstract Children
    /// </summary>
    public abstract Spread<TNode?>? Children { internal get; set; }

    /// <summary>
    /// FlexNodeBase Abstract Style
    /// </summary>
    public abstract TNodeStyle? Style { internal get; set; }
    #endregion

    #region Layout
    protected RectangleF _layout;

    /// <summary>
    /// Node calculated Layout
    /// </summary>
    public RectangleF Layout
    {
        get => _layout;
        set => _layout = value;
    }

    /// <summary>
    /// Event fired on layout change, used in Breakpoints to conditionally change style.
    /// </summary>
    public abstract event Action<FlexCalculateLayoutArgs>? OnLayoutChanged;

    /// <summary>
    /// Applies layout to node layout
    /// </summary>
    /// <param name="args"></param>
    /// <param name="ownerLayout"></param>
    public abstract void ApplyLayout(FlexCalculateLayoutArgs args, RectangleF? ownerLayout = null);


    /// <summary>
    /// Builds Rectangle
    /// </summary>
    /// <param name="ownerLayout"></param>
    public unsafe void BuildLayout(RectangleF? ownerLayout) => Layout = new RectangleF
        (
            _handle->GetComputedLeft() + ownerLayout?.Left ?? .0f,
            _handle->GetComputedTop() + ownerLayout?.Top ?? .0f,
            _handle->GetComputedWidth(),
            _handle->GetComputedHeight()
        );

    /// <summary>
    /// Calculates and applies layout to nodes upstream.
    /// </summary>
    /// <param name="ownerBounds"></param>
    /// <param name="ownerDirection"></param>
    public void CalculateLayout(Optional<RectangleF> ownerBounds, Optional<YGDirection> ownerDirection)
    {
        unsafe
        {
            _handle->CalculateLayout(ownerBounds.ToNullable()?.Width ?? float.NaN, ownerBounds.ToNullable()?.Height ?? float.NaN, ownerDirection.ToNullable() ?? YGDirection.Inherit);
        }

        ApplyLayout(new FlexCalculateLayoutArgs(ownerBounds.ToNullable(), ownerDirection.ToNullable()));
    }

    /// <summary>
    /// Node layout should be re-calculated. 
    /// </summary>
    public bool IsDirty
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
    public void MarkDirty()
    {
        unsafe
        {
            _handle->MarkDirty();
        }
    }

    /// <summary>
    /// Should apply new layout. 
    /// </summary>
    public unsafe bool HasNewLayout
    {
        get => _handle->GetHasNewLayout();
        set
        {
            _handle->SetHasNewLayout(value);
        }
    }
    #endregion

    #region Constructors
    /// <summary>
    /// Since delegates are static methods, we need to store pointers somewhere
    /// </summary>
    public FlexNodeBase()
    {
        Store.GetRegistry().RegisterNode(this);
    }
    #endregion

    #region MeasureFunc
    protected MeasureFunc? _measureFunc;
    protected nint _measureFuncPtr;

    /// <inheritdoc cref="Internals.Delegates.MeasureFunc"/>
    public MeasureFunc? MeasureFunc
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
    /// Object that going to be passed to measure func.
    /// </summary>
    protected object? _measureContext;
    public virtual object? MeasureContext
    {
        internal get => _measureContext;
        set => _measureContext = value;
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
    public BaselineFunc? BaselineFunc
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
    /// Object that going to be passed to baseline func.
    /// </summary>
    protected object? _baselineContext;
    public object? BaselineContext
    {
        internal get => _baselineContext;
        set => _baselineContext = value;
    }

    /// <summary>
    ///  Whether a baseline function is set.
    /// </summary>
    public bool HasBaselineFunc
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
    public bool? IsReferenceBaseline
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
    public YGNodeType? NodeType
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
    public bool? AlwaysFormsContainingBlock
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

    public abstract void Dispose();
}

/// <summary>
/// Base class for handling Flex Layouts
/// </summary>
public partial class FlexNode : FlexNodeBase<FlexNode, IFlexStyle>
{
    #region Children

    protected Spread<FlexNode?>? _children = null;

    /// <summary>
    /// FlexNode Children
    /// </summary>
    public override Spread<FlexNode?>? Children
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

    #endregion

    #region Layout

    public override event Action<FlexCalculateLayoutArgs>? OnLayoutChanged;
    public override void ApplyLayout(FlexCalculateLayoutArgs args, RectangleF? ownerLayout = null)
    {
        BuildLayout(ownerLayout ?? args.OwnerBounds);

        Children?.ForEach(child =>
        {
            child?.ApplyLayout(args, Layout);
        });

        OnLayoutChanged?.Invoke(args);

        HasNewLayout = false;
    }

    #endregion

    #region Constructors

    public FlexNode() : base() { }

    #endregion

    #region Dispose

    public override void Dispose()
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