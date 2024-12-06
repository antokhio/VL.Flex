using YogaSharp;

namespace VL.Flex
{
    public interface IFlexStyle
    {
        public void ApplyStyle(FlexBase node);
    }
}

namespace VL.Flex.Style
{
    public static partial class FlexStyle
    {
        public static FlexStyleDirection SetDirection(IFlexStyle? style, YGDirection direction) => new(style, direction);
        public static FlexStyleFlexDirection SetFlexDirection(IFlexStyle? style, YGFlexDirection flexDirection) => new(style, flexDirection);
        public static FlexStyleAlignContent SetAlignContent(IFlexStyle? style, YGAlign alignContent) => new(style, alignContent);
        public static FlexStyleAlignItems SetAlignItems(IFlexStyle? style, YGAlign alignItems) => new(style, alignItems);
        public static FlexStyleAlignSelf SetAlignSelf(IFlexStyle? style, YGAlign alignSelf) => new(style, alignSelf);
        public static FlexStylePositionType SetPositionType(IFlexStyle? style, YGPositionType positionType) => new(style, positionType);
        public static FlexStyleFlexWrap SetFlexWrap(IFlexStyle? style, YGWrap flexWrap) => new(style, flexWrap);
        public static FlexStyleOverflow SetOverflow(IFlexStyle? style, YGOverflow overflow) => new(style, overflow);
        public static FlexStyleDisplay SetDisplay(IFlexStyle? style, YGDisplay display) => new(style, display);
        public static FlexStyleFlex SetFlex(IFlexStyle? style, float flex) => new(style, flex);
        public static FlexStyleFlexGrow SetFlexGrow(IFlexStyle? style, float flexGrow) => new(style, flexGrow);
        public static FlexStyleFlexShrink SetFlexShrink(IFlexStyle? style, float flexShrink) => new(style, flexShrink);
        public static FlexStyleFlexBasis SetFlexBasis(IFlexStyle? style, float flexBasis) => new(style, flexBasis);
        public static FlexStyleFlexBasisPercent SetFlexBasisPercent(IFlexStyle? style, float flexBasisPercent) => new(style, flexBasisPercent);
        public static FlexStyleFlexBasisAuto SetFlexBasisAuto(IFlexStyle? style) => new(style);
        public static FlexStyleFlexBasisMaxContent SetFlexBasisMaxContent(IFlexStyle? style) => new(style);
        public static FlexStyleFlexBasisFitContent SetFlexBasisFitContent(IFlexStyle? style) => new(style);
        public static FlexStyleFlexBasisStretch SetFlexBasisStretch(IFlexStyle? style) => new(style);

        public static FlexStylePosition SetPosition(IFlexStyle? style, YGEdge edge, float value) => new(style, edge, value);
        public static FlexStylePositionPercent SetPositionPercent(IFlexStyle? style, YGEdge edge, float percent) => new(style, edge, percent);
        public static FlexStyleMargin SetMargin(IFlexStyle? style, YGEdge edge, float value) => new(style, edge, value);
        public static FlexStyleMarginPercent SetMarginPercent(IFlexStyle? style, YGEdge edge, float percent) => new(style, edge, percent);
        public static FlexStyleMarginAuto SetMarginAuto(IFlexStyle? style, YGEdge edge) => new(style, edge);
        public static FlexStylePadding SetPadding(IFlexStyle? style, YGEdge edge, float value) => new(style, edge, value);
        public static FlexStylePaddingPercent SetPaddingPercent(IFlexStyle? style, YGEdge edge, float percent) => new(style, edge, percent);
        public static FlexStyleBorder SetBorder(IFlexStyle? style, YGEdge edge, float value) => new(style, edge, value);
        public static FlexStyleGap SetGap(IFlexStyle? style, YGGutter gutter, float gapLength) => new(style, gutter, gapLength);
        public static FlexStyleAspectRatio SetAspectRatio(IFlexStyle? style, float aspectRatio) => new(style, aspectRatio);
        public static FlexStyleWidth SetWidth(IFlexStyle? style, float width) => new(style, width);
        public static FlexStyleWidthPercent SetWidthPercent(IFlexStyle? style, float widthPercent) => new(style, widthPercent);
        public static FlexStyleWidthAuto SetWidthAuto(IFlexStyle? style) => new(style);
        public static FlexStyleWidthMaxContent SetWidthMaxContent(IFlexStyle? style) => new(style);
        public static FlexStyleWidthFitContent SetWidthFitContent(IFlexStyle? style) => new(style);
        public static FlexStyleWidthStretch SetWidthStretch(IFlexStyle? style) => new(style);
        public static FlexStyleHeight SetHeight(IFlexStyle? style, float height) => new(style, height);
        public static FlexStyleHeightPercent SetHeightPercent(IFlexStyle? style, float heightPercent) => new(style, heightPercent);
        public static FlexStyleHeightAuto SetHeightAuto(IFlexStyle? style) => new(style);
        public static FlexStyleHeightMaxContent SetHeightMaxContent(IFlexStyle? style) => new(style);
        public static FlexStyleHeightFitContent SetHeightFitContent(IFlexStyle? style) => new(style);
        public static FlexStyleHeightStretch SetHeightStretch(IFlexStyle? style) => new(style);
        public static FlexStyleMinWidth SetMinWidth(IFlexStyle? style, float minWidth) => new(style, minWidth);
        public static FlexStyleMinWidthPercent SetMinWidthPercent(IFlexStyle? style, float minWidthPercent) => new(style, minWidthPercent);
        public static FlexStyleMinWidthMaxContent SetMinWidthMaxContent(IFlexStyle? style) => new(style);
        public static FlexStyleMinWidthFitContent SetMinWidthFitContent(IFlexStyle? style) => new(style);
        public static FlexStyleMinWidthStretch SetMinWidthStretch(IFlexStyle? style) => new(style);
        public static FlexStyleMinHeight SetMinHeight(IFlexStyle? style, float minHeight) => new(style, minHeight);
        public static FlexStyleMinHeightPercent SetMinHeightPercent(IFlexStyle? style, float minHeightPercent) => new(style, minHeightPercent);
        public static FlexStyleMinHeightMaxContent SetMinHeightMaxContent(IFlexStyle? style) => new(style);
        public static FlexStyleMinHeightFitContent SetMinHeightFitContent(IFlexStyle? style) => new(style);
        public static FlexStyleMinHeightStretch SetMinHeightStretch(IFlexStyle? style) => new(style);
        public static FlexStyleMaxWidth SetMaxWidth(IFlexStyle? style, float maxWidth) => new(style, maxWidth);
        public static FlexStyleMaxWidthPercent SetMaxWidthPercent(IFlexStyle? style, float maxWidthPercent) => new(style, maxWidthPercent);
        public static FlexStyleMaxWidthMaxContent SetMaxWidthMaxContent(IFlexStyle? style) => new(style);
        public static FlexStyleMaxWidthFitContent SetMaxWidthFitContent(IFlexStyle? style) => new(style);
        public static FlexStyleMaxWidthStretch SetMaxWidthStretch(IFlexStyle? style) => new(style);
        public static FlexStyleMaxHeight SetMaxHeight(IFlexStyle? style, float maxHeight) => new(style, maxHeight);
        public static FlexStyleMaxHeightPercent SetMaxHeightPercent(IFlexStyle? style, float maxHeightPercent) => new(style, maxHeightPercent);
        public static FlexStyleMaxHeightMaxContent SetMaxHeightMaxContent(IFlexStyle? style) => new(style);
        public static FlexStyleMaxHeightFitContent SetMaxHeightFitContent(IFlexStyle? style) => new(style);
        public static FlexStyleMaxHeightStretch SetMaxHeightStretch(IFlexStyle? style) => new(style);
    }

    public unsafe record struct FlexStyleDirection(IFlexStyle? Style, YGDirection Direction) : IFlexStyle
    {
        public void ApplyStyle(FlexBase node)
        {
            node.Handle->SetDirection(Direction);
            Style?.ApplyStyle(node);
        }
    }

    public unsafe record struct FlexStyleFlexDirection(IFlexStyle? Style, YGFlexDirection FlexDirection) : IFlexStyle
    {
        public void ApplyStyle(FlexBase node)
        {
            node.Handle->SetFlexDirection(FlexDirection);
            Style?.ApplyStyle(node);
        }
    }

    public unsafe record struct FlexStyleAlignContent(IFlexStyle? Style, YGAlign AlignContent) : IFlexStyle
    {
        public void ApplyStyle(FlexBase node)
        {
            node.Handle->SetAlignContent(AlignContent);
            Style?.ApplyStyle(node);
        }
    }

    public unsafe record struct FlexStyleAlignItems(IFlexStyle? Style, YGAlign AlignItems) : IFlexStyle
    {
        public void ApplyStyle(FlexBase node)
        {
            node.Handle->SetAlignItems(AlignItems);
            Style?.ApplyStyle(node);
        }
    }

    public unsafe record struct FlexStyleAlignSelf(IFlexStyle? Style, YGAlign AlignSelf) : IFlexStyle
    {
        public void ApplyStyle(FlexBase node)
        {
            node.Handle->SetAlignSelf(AlignSelf);
            Style?.ApplyStyle(node);
        }
    }

    public unsafe record struct FlexStylePositionType(IFlexStyle? Style, YGPositionType PositionType) : IFlexStyle
    {
        public void ApplyStyle(FlexBase node)
        {
            node.Handle->SetPositionType(PositionType);
            Style?.ApplyStyle(node);
        }
    }

    public unsafe record struct FlexStyleFlexWrap(IFlexStyle? Style, YGWrap FlexWrap) : IFlexStyle
    {
        public void ApplyStyle(FlexBase node)
        {
            node.Handle->SetFlexWrap(FlexWrap);
            Style?.ApplyStyle(node);
        }
    }

    public unsafe record struct FlexStyleOverflow(IFlexStyle? Style, YGOverflow Overflow) : IFlexStyle
    {
        public void ApplyStyle(FlexBase node)
        {
            node.Handle->SetOverflow(Overflow);
            Style?.ApplyStyle(node);
        }
    }

    public unsafe record struct FlexStyleDisplay(IFlexStyle? Style, YGDisplay Display) : IFlexStyle
    {
        public void ApplyStyle(FlexBase node)
        {
            node.Handle->SetDisplay(Display);
            Style?.ApplyStyle(node);
        }
    }

    public unsafe record struct FlexStyleFlex(IFlexStyle? Style, float Flex) : IFlexStyle
    {
        public void ApplyStyle(FlexBase node)
        {
            node.Handle->SetFlex(Flex);
            Style?.ApplyStyle(node);
        }
    }

    public unsafe record struct FlexStyleFlexGrow(IFlexStyle? Style, float FlexGrow) : IFlexStyle
    {
        public void ApplyStyle(FlexBase node)
        {
            node.Handle->SetFlexGrow(FlexGrow);
            Style?.ApplyStyle(node);
        }
    }

    public unsafe record struct FlexStyleFlexShrink(IFlexStyle? Style, float FlexShrink) : IFlexStyle
    {
        public void ApplyStyle(FlexBase node)
        {
            node.Handle->SetFlexShrink(FlexShrink);
            Style?.ApplyStyle(node);
        }
    }

    public unsafe record struct FlexStyleFlexBasis(IFlexStyle? Style, float FlexBasis) : IFlexStyle
    {
        public void ApplyStyle(FlexBase node)
        {
            node.Handle->SetFlexBasis(FlexBasis);
            Style?.ApplyStyle(node);
        }
    }

    public unsafe record struct FlexStyleFlexBasisPercent(IFlexStyle? Style, float FlexBasisPercent) : IFlexStyle
    {
        public void ApplyStyle(FlexBase node)
        {
            node.Handle->SetFlexBasisPercent(FlexBasisPercent);
            Style?.ApplyStyle(node);
        }
    }

    public unsafe record struct FlexStyleFlexBasisAuto(IFlexStyle? Style) : IFlexStyle
    {
        public void ApplyStyle(FlexBase node)
        {
            node.Handle->SetFlexBasisAuto();
            Style?.ApplyStyle(node);
        }
    }

    public unsafe record struct FlexStyleFlexBasisMaxContent(IFlexStyle? Style) : IFlexStyle
    {
        public void ApplyStyle(FlexBase node)
        {
            node.Handle->SetFlexBasisMaxContent();
            Style?.ApplyStyle(node);
        }
    }

    public unsafe record struct FlexStyleFlexBasisFitContent(IFlexStyle? Style) : IFlexStyle
    {
        public void ApplyStyle(FlexBase node)
        {
            node.Handle->SetFlexBasisFitContent();
            Style?.ApplyStyle(node);
        }
    }

    public unsafe record struct FlexStyleFlexBasisStretch(IFlexStyle? Style) : IFlexStyle
    {
        public void ApplyStyle(FlexBase node)
        {
            node.Handle->SetFlexBasisStretch();
            Style?.ApplyStyle(node);
        }
    }


    public unsafe record struct FlexStylePosition(IFlexStyle? Style, YGEdge Edge, float Value) : IFlexStyle
    {
        public void ApplyStyle(FlexBase node)
        {
            node.Handle->SetPosition(Edge, Value);
            Style?.ApplyStyle(node);
        }
    }

    public unsafe record struct FlexStylePositionPercent(IFlexStyle? Style, YGEdge Edge, float Percent) : IFlexStyle
    {
        public void ApplyStyle(FlexBase node)
        {
            node.Handle->SetPositionPercent(Edge, Percent);
            Style?.ApplyStyle(node);
        }
    }


    public unsafe record struct FlexStyleMargin(IFlexStyle? Style, YGEdge Edge, float Value) : IFlexStyle
    {
        public void ApplyStyle(FlexBase node)
        {
            node.Handle->SetMargin(Edge, Value);
            Style?.ApplyStyle(node);
        }
    }

    public unsafe record struct FlexStyleMarginPercent(IFlexStyle? Style, YGEdge Edge, float Percent) : IFlexStyle
    {
        public void ApplyStyle(FlexBase node)
        {
            node.Handle->SetMarginPercent(Edge, Percent);
            Style?.ApplyStyle(node);
        }
    }

    public unsafe record struct FlexStyleMarginAuto(IFlexStyle? Style, YGEdge Edge) : IFlexStyle
    {
        public void ApplyStyle(FlexBase node)
        {
            node.Handle->SetMarginAuto(Edge);
            Style?.ApplyStyle(node);
        }
    }

    public unsafe record struct FlexStylePadding(IFlexStyle? Style, YGEdge Edge, float Value) : IFlexStyle
    {
        public void ApplyStyle(FlexBase node)
        {
            node.Handle->SetPadding(Edge, Value);
            Style?.ApplyStyle(node);
        }
    }

    public unsafe record struct FlexStylePaddingPercent(IFlexStyle? Style, YGEdge Edge, float Percent) : IFlexStyle
    {
        public void ApplyStyle(FlexBase node)
        {
            node.Handle->SetPaddingPercent(Edge, Percent);
            Style?.ApplyStyle(node);
        }
    }

    public unsafe record struct FlexStyleBorder(IFlexStyle? Style, YGEdge Edge, float Value) : IFlexStyle
    {
        public void ApplyStyle(FlexBase node)
        {
            node.Handle->SetBorder(Edge, Value);
            Style?.ApplyStyle(node);
        }
    }

    public unsafe record struct FlexStyleGap(IFlexStyle? Style, YGGutter Gutter, float GapLength) : IFlexStyle
    {
        public void ApplyStyle(FlexBase node)
        {
            node.Handle->SetGap(Gutter, GapLength);
            Style?.ApplyStyle(node);
        }
    }

    public unsafe record struct FlexStyleAspectRatio(IFlexStyle? Style, float AspectRatio) : IFlexStyle
    {
        public void ApplyStyle(FlexBase node)
        {
            node.Handle->SetAspectRatio(AspectRatio);
            Style?.ApplyStyle(node);
        }
    }

    public unsafe record struct FlexStyleWidth(IFlexStyle? Style, float Width) : IFlexStyle
    {
        public void ApplyStyle(FlexBase node)
        {
            node.Handle->SetWidth(Width);
            Style?.ApplyStyle(node);
        }
    }

    public unsafe record struct FlexStyleWidthPercent(IFlexStyle? Style, float WidthPercent) : IFlexStyle
    {
        public void ApplyStyle(FlexBase node)
        {
            node.Handle->SetWidthPercent(WidthPercent);
            Style?.ApplyStyle(node);
        }
    }

    public unsafe record struct FlexStyleWidthAuto(IFlexStyle? Style) : IFlexStyle
    {
        public void ApplyStyle(FlexBase node)
        {
            node.Handle->SetWidthAuto();
            Style?.ApplyStyle(node);
        }
    }

    public unsafe record struct FlexStyleWidthMaxContent(IFlexStyle? Style) : IFlexStyle
    {
        public void ApplyStyle(FlexBase node)
        {
            node.Handle->SetWidthMaxContent();
            Style?.ApplyStyle(node);
        }
    }

    public unsafe record struct FlexStyleWidthFitContent(IFlexStyle? Style) : IFlexStyle
    {
        public void ApplyStyle(FlexBase node)
        {
            node.Handle->SetWidthFitContent();
            Style?.ApplyStyle(node);
        }
    }

    public unsafe record struct FlexStyleWidthStretch(IFlexStyle? Style) : IFlexStyle
    {
        public void ApplyStyle(FlexBase node)
        {
            node.Handle->SetWidthStretch();
            Style?.ApplyStyle(node);
        }
    }

    public unsafe record struct FlexStyleHeight(IFlexStyle? Style, float Height) : IFlexStyle
    {
        public void ApplyStyle(FlexBase node)
        {
            node.Handle->SetHeight(Height);
            Style?.ApplyStyle(node);
        }
    }

    public unsafe record struct FlexStyleHeightPercent(IFlexStyle? Style, float HeightPercent) : IFlexStyle
    {
        public void ApplyStyle(FlexBase node)
        {
            node.Handle->SetHeightPercent(HeightPercent);
            Style?.ApplyStyle(node);
        }
    }

    public unsafe record struct FlexStyleHeightAuto(IFlexStyle? Style) : IFlexStyle
    {
        public void ApplyStyle(FlexBase node)
        {
            node.Handle->SetHeightAuto();
            Style?.ApplyStyle(node);
        }
    }

    public unsafe record struct FlexStyleHeightMaxContent(IFlexStyle? Style) : IFlexStyle
    {
        public void ApplyStyle(FlexBase node)
        {
            node.Handle->SetHeightMaxContent();
            Style?.ApplyStyle(node);
        }
    }

    public unsafe record struct FlexStyleHeightFitContent(IFlexStyle? Style) : IFlexStyle
    {
        public void ApplyStyle(FlexBase node)
        {
            node.Handle->SetHeightFitContent();
            Style?.ApplyStyle(node);
        }
    }

    public unsafe record struct FlexStyleHeightStretch(IFlexStyle? Style) : IFlexStyle
    {
        public void ApplyStyle(FlexBase node)
        {
            node.Handle->SetHeightStretch();
            Style?.ApplyStyle(node);
        }
    }


    public unsafe record struct FlexStyleMinWidth(IFlexStyle? Style, float MinWidth) : IFlexStyle
    {
        public void ApplyStyle(FlexBase node)
        {
            node.Handle->SetMinWidth(MinWidth);
            Style?.ApplyStyle(node);
        }
    }

    public unsafe record struct FlexStyleMinWidthPercent(IFlexStyle? Style, float MinWidthPercent) : IFlexStyle
    {
        public void ApplyStyle(FlexBase node)
        {
            node.Handle->SetMinWidthPercent(MinWidthPercent);
            Style?.ApplyStyle(node);
        }
    }

    public unsafe record struct FlexStyleMinWidthMaxContent(IFlexStyle? Style) : IFlexStyle
    {
        public void ApplyStyle(FlexBase node)
        {
            node.Handle->SetMinWidthMaxContent();
            Style?.ApplyStyle(node);
        }
    }
    public unsafe record struct FlexStyleMinWidthFitContent(IFlexStyle? Style) : IFlexStyle
    {
        public void ApplyStyle(FlexBase node)
        {
            node.Handle->SetMinWidthFitContent();
            Style?.ApplyStyle(node);
        }
    }
    public unsafe record struct FlexStyleMinWidthStretch(IFlexStyle? Style) : IFlexStyle
    {
        public void ApplyStyle(FlexBase node)
        {
            node.Handle->SetMinWidthStretch();
            Style?.ApplyStyle(node);
        }
    }

    public unsafe record struct FlexStyleMinHeight(IFlexStyle? Style, float MinHeight) : IFlexStyle
    {
        public void ApplyStyle(FlexBase node)
        {
            node.Handle->SetMinHeight(MinHeight);
            Style?.ApplyStyle(node);
        }
    }

    public unsafe record struct FlexStyleMinHeightPercent(IFlexStyle? Style, float MinHeightPercent) : IFlexStyle
    {
        public void ApplyStyle(FlexBase node)
        {
            node.Handle->SetMinHeightPercent(MinHeightPercent);
            Style?.ApplyStyle(node);
        }
    }

    public unsafe record struct FlexStyleMinHeightMaxContent(IFlexStyle? Style) : IFlexStyle
    {
        public void ApplyStyle(FlexBase node)
        {
            node.Handle->SetMinHeightMaxContent();
            Style?.ApplyStyle(node);
        }
    }
    public unsafe record struct FlexStyleMinHeightFitContent(IFlexStyle? Style) : IFlexStyle
    {
        public void ApplyStyle(FlexBase node)
        {
            node.Handle->SetMinHeightFitContent();
            Style?.ApplyStyle(node);
        }
    }
    public unsafe record struct FlexStyleMinHeightStretch(IFlexStyle? Style) : IFlexStyle
    {
        public void ApplyStyle(FlexBase node)
        {
            node.Handle->SetMinHeightStretch();
            Style?.ApplyStyle(node);
        }
    }

    public unsafe record struct FlexStyleMaxWidth(IFlexStyle? Style, float MaxWidth) : IFlexStyle
    {
        public void ApplyStyle(FlexBase node)
        {
            node.Handle->SetMaxWidth(MaxWidth);
            Style?.ApplyStyle(node);
        }
    }

    public unsafe record struct FlexStyleMaxWidthPercent(IFlexStyle? Style, float MaxWidthPercent) : IFlexStyle
    {
        public void ApplyStyle(FlexBase node)
        {
            node.Handle->SetMaxWidthPercent(MaxWidthPercent);
            Style?.ApplyStyle(node);
        }
    }

    public unsafe record struct FlexStyleMaxWidthMaxContent(IFlexStyle? Style) : IFlexStyle
    {
        public void ApplyStyle(FlexBase node)
        {
            node.Handle->SetMaxWidthMaxContent();
            Style?.ApplyStyle(node);
        }
    }
    public unsafe record struct FlexStyleMaxWidthFitContent(IFlexStyle? Style) : IFlexStyle
    {
        public void ApplyStyle(FlexBase node)
        {
            node.Handle->SetMaxWidthFitContent();
            Style?.ApplyStyle(node);
        }
    }
    public unsafe record struct FlexStyleMaxWidthStretch(IFlexStyle? Style) : IFlexStyle
    {
        public void ApplyStyle(FlexBase node)
        {
            node.Handle->SetMaxWidthStretch();
            Style?.ApplyStyle(node);
        }
    }

    public unsafe record struct FlexStyleMaxHeight(IFlexStyle? Style, float MaxHeight) : IFlexStyle
    {
        public void ApplyStyle(FlexBase node)
        {
            node.Handle->SetMaxHeight(MaxHeight);
            Style?.ApplyStyle(node);
        }
    }

    public unsafe record struct FlexStyleMaxHeightPercent(IFlexStyle? Style, float MaxHeightPercent) : IFlexStyle
    {
        public void ApplyStyle(FlexBase node)
        {
            node.Handle->SetMaxHeightPercent(MaxHeightPercent);
            Style?.ApplyStyle(node);
        }
    }

    public unsafe record struct FlexStyleMaxHeightMaxContent(IFlexStyle? Style) : IFlexStyle
    {
        public void ApplyStyle(FlexBase node)
        {
            node.Handle->SetMaxHeightMaxContent();
            Style?.ApplyStyle(node);
        }
    }
    public unsafe record struct FlexStyleMaxHeightFitContent(IFlexStyle? Style) : IFlexStyle
    {
        public void ApplyStyle(FlexBase node)
        {
            node.Handle->SetMaxHeightFitContent();
            Style?.ApplyStyle(node);
        }
    }
    public unsafe record struct FlexStyleMaxHeightStretch(IFlexStyle? Style) : IFlexStyle
    {
        public void ApplyStyle(FlexBase node)
        {
            node.Handle->SetMaxHeightStretch();
            Style?.ApplyStyle(node);
        }
    }

}
