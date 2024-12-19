using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using YogaSharp;

namespace VL.Flex.Internals
{
    public class Delegates
    {
        /// <summary>
        /// Native delegate adapter, used internally.
        /// </summary>
        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvCdecl) })]
        public static unsafe YGSize MeasureFuncAdapter(YGNode* node, float width, YGMeasureMode widthMode, float height, YGMeasureMode heightMode)
        {
            var item = Store.GetRegistry().GetNode(node);

            if (item != null)
            {
                var size = item.MeasureFunc?.Invoke(item.NodeContext, width, widthMode, height, heightMode);

                if (size != null)
                {
                    return size.Value;
                }
            }

            return new YGSize { Width = float.NaN, Height = float.NaN };
        }

        /// <summary>
        /// External delegate signature
        /// </summary>
        public delegate YGSize MeasureFunc(object? context, float width, YGMeasureMode widthMode, float height, YGMeasureMode heightMode);

        /// <summary>
        /// External delegate utility method to be called by VL.
        /// </summary>
        public static MeasureFunc SetMeasureFunc(MeasureFunc measureFunc) => measureFunc;
    }
}
