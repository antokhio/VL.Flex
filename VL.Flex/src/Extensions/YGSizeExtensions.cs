using Stride.Core.Mathematics;
using YogaSharp;

namespace VL.Flex.Extensions
{
    public static class YGSizeExtensions
    {
        public static YGSize YGSize(float width, float height) => new() { Width = width, Height = height };
        public static YGSize YGSize(Vector2 size) => new() { Width = size.X, Height = size.Y };
        public static Vector2 YGSize(YGSize size) => new Vector2 { X = size.Width, Y = size.Height };
    }
}
