using VL.Lib.Collections;
using YogaSharp;

namespace VL.Flex
{
    public abstract class FlexBase : IDisposable
    {
        // Yoga internals
        protected unsafe YGNode* _handle = YGNode.New();
        public abstract unsafe YGNode* Handle { get; }

        // Children
        protected Spread<FlexBase?>? _children = null;
        public abstract Spread<FlexBase?>? Children { internal get; set; }

        // Style
        protected IFlexStyle? _style;
        public abstract IFlexStyle? Style { internal get; set; }

        // Calculated Layout
        protected FlexLayout _layout;
        public abstract FlexLayout Layout { get; set; }
        public abstract void ApplyLayout(FlexLayoutArgs args, FlexLayout? ownerLayout);

        public abstract event Action<FlexLayoutArgs>? OnLayoutChanged;
        public abstract bool IsDirty { get; }
        public abstract bool HasNewLayout { internal get; set; }
        public abstract void CalculateLayout(float? ownerWidth, float? ownerHeight, YGDirection? ownerDirection);

        // Dispose
        public abstract void Dispose();

        // Owner, so we can traverse to Root from breakpoint (subject to change)
        protected FlexBase? _owner = null;
        public abstract void SetOwner(FlexNode node);
        public abstract FlexBase GetRoot();
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
                        _handle->CopyStyle(FlexInternalsLocator.GetInternals().BulkNode);
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
        public override void Dispose()
        {
            unsafe
            {
                if (_handle != null)
                {
                    _handle->RemoveAllChildren();
                    _handle->Dispose();
                }

                GC.SuppressFinalize(this);
            }
        }
    }
}

