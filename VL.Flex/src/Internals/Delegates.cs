﻿using System.Runtime.CompilerServices;
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
        /// Returns the computed dimensions of the node, following the constraints of <paramref name="widthMode"/> and <paramref name="heightMode"/>:<br/>
        /// <br/>
        /// <list type="table">
        ///   <item>
        ///     <term><see cref="YGMeasureMode.Undefined"/></term>
        ///     <description>The parent has not imposed any constraint on the child. It can be whatever size it wants.</description>
        ///   </item>
        ///   <item>
        ///     <term><see cref="YGMeasureMode.AtMost"/></term>
        ///     <description>The child can be as large as it wants up to the specified size.</description>
        ///   </item>
        ///   <item>
        ///     <term><see cref="YGMeasureMode.Exactly"/></term>
        ///     <description>The parent has determined an exact size for the child. The child is going to be given those bounds regardless of how big it wants to be.</description>
        ///   </item>
        /// </list>
        /// </summary>
        /// <returns>
        /// The size of the leaf node, measured under the given constraints.
        /// </returns>
        public delegate YGSize MeasureFunc(object? context, float width, YGMeasureMode widthMode, float height, YGMeasureMode heightMode);

        /// <summary>
        /// Native delegate adapter, used internally.
        /// </summary>
        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvCdecl) })]
        public static unsafe float BaselineFuncAdapter(YGNode* node, float width, float height)
        {
            var item = Store.GetRegistry().GetNode(node);
            if (item != null)
            {
                var baseline = item.BaselineFunc?.Invoke(item.NodeContext, width, height);

                if (baseline != null)
                {
                    return baseline.Value;
                }
            }

            return float.NaN;
        }

        /// <summary>
        /// A defined offset to baseline (ascent).
        /// </summary>
        public delegate float BaselineFunc(object? context, float width, float height);

    }
}
