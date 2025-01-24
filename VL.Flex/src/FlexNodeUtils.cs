using Stride.Core.Mathematics;
using YogaSharp;

namespace VL.Flex
{
    public static partial class FlexNodeUtils
    {
        public static Vector2 StyleGetPaddingSize(FlexNode? flexNode)
        {
            var size = Vector2.Zero;

            if (flexNode != null)
            {
                var paddingLeft = flexNode.StyleGetPadding(YGEdge.Left);
                var paddingRight = flexNode.StyleGetPadding(YGEdge.Right);
                var paddingTop = flexNode.StyleGetPadding(YGEdge.Top);
                var paddingBottom = flexNode.StyleGetPadding(YGEdge.Bottom);

                var paddingHorizontal = flexNode.StyleGetPadding(YGEdge.Horizontal);
                var paddingVertical = flexNode.StyleGetPadding(YGEdge.Vertical);

                var paddingAll = flexNode.StyleGetPadding(YGEdge.All);

                size.X += paddingLeft.IsDefined
                    ? paddingLeft.Value : paddingRight.IsDefined
                    ? paddingRight.Value : paddingHorizontal.IsDefined
                    ? paddingHorizontal.Value * 2 : paddingAll.IsDefined
                    ? paddingAll.Value * 2 : 0.0f;

                size.Y += paddingTop.IsDefined
                    ? paddingTop.Value : paddingBottom.IsDefined
                    ? paddingBottom.Value : paddingVertical.IsDefined
                    ? paddingVertical.Value * 2 : paddingAll.IsDefined
                    ? paddingAll.Value * 2 : 0.0f;
            }

            return size;
        }
    }
}
