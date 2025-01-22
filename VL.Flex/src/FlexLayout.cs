using YogaSharp;

namespace VL.Flex
{
    public record struct FlexLayout
    {
        public float Left { get; set; }
        public float Top { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }

        public unsafe FlexLayout(FlexNode node, FlexLayout? ownerLayout)
        {
            Left = node.Handle->GetComputedLeft() + ownerLayout?.Left ?? .0f;
            Top = node.Handle->GetComputedTop() + ownerLayout?.Top ?? .0f;
            Width = node.Handle->GetComputedWidth();
            Height = node.Handle->GetComputedHeight();
        }
    }

    public record struct FlexLayoutArgs
    {
        public float? OwnerWidth { get; }
        public float? OwnerHeight { get; }
        public YGDirection? OwnerDirection { get; }

        public FlexLayoutArgs(float? ownerWidth, float? ownerHeight, YGDirection? ownerDirection)
        {
            OwnerWidth = ownerWidth;
            OwnerHeight = ownerHeight;
            OwnerDirection = ownerDirection;
        }
    }
}