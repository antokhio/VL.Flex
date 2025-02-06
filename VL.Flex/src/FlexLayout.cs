using Stride.Core.Mathematics;

namespace VL.Flex
{
    public record struct FlexLayout
    {
        public float Left { get; set; }
        public float Top { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }

        public FlexLayout(Vector2? ownerPosition)
        {
            Left = ownerPosition?.X ?? .0f;
            Top = ownerPosition?.Y ?? .0f;
        }
        public unsafe FlexLayout(FlexNode node, FlexLayout? ownerLayout)
        {
            Left = node.Handle->GetComputedLeft() + ownerLayout?.Left ?? .0f;
            Top = node.Handle->GetComputedTop() + ownerLayout?.Top ?? .0f;
            Width = node.Handle->GetComputedWidth();
            Height = node.Handle->GetComputedHeight();
        }
    }
}