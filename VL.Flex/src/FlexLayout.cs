namespace VL.Flex
{
    public record struct FlexLayout
    {
        public float Left { get; set; }
        public float Top { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }

        public unsafe FlexLayout(FlexBase node, FlexLayout? ownerLayout)
        {
            Left = node.Handle->GetComputedLeft() + ownerLayout?.Left ?? .0f;
            Top = node.Handle->GetComputedTop() + ownerLayout?.Top ?? .0f;
            Width = node.Handle->GetComputedWidth();
            Height = node.Handle->GetComputedHeight();
        }
    }

}
