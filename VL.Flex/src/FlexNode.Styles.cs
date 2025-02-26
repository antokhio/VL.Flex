using YogaSharp;

namespace VL.Flex;

public partial class FlexNode
{
    public YGDirection StyleGetDirection()
    {
        unsafe
        {
            return Interop.YGNodeStyleGetDirection(_handle);
        }
    }
    public YGFlexDirection StyleGetFlexDirection()
    {
        unsafe
        {
            return Interop.YGNodeStyleGetFlexDirection(_handle);
        }
    }
    public YGJustify StyleGetJustifyContent()
    {
        unsafe
        {
            return Interop.YGNodeStyleGetJustifyContent(_handle);
        }
    }
    public YGAlign StyleGetAlignContent()
    {
        unsafe
        {
            return Interop.YGNodeStyleGetAlignContent(_handle);
        }
    }
    public YGAlign StyleGetAlignItems()
    {
        unsafe
        {
            return Interop.YGNodeStyleGetAlignItems(_handle);
        }
    }
    public YGAlign StyleGetAlignSelf()
    {
        unsafe
        {
            return Interop.YGNodeStyleGetAlignSelf(_handle);
        }
    }
    public YGPositionType StyleGetPositionType()
    {
        unsafe
        {
            return Interop.YGNodeStyleGetPositionType(_handle);
        }
    }
    public YGWrap StyleGetFlexWrap()
    {
        unsafe
        {
            return Interop.YGNodeStyleGetFlexWrap(_handle);
        }
    }
    public YGOverflow StyleGetOverflow()
    {
        unsafe
        {
            return Interop.YGNodeStyleGetOverflow(_handle);
        }
    }
    public YGDisplay StyleGetDisplay()
    {
        unsafe
        {
            return Interop.YGNodeStyleGetDisplay(_handle);
        }
    }
    public float StyleGetFlex()
    {
        unsafe
        {
            return Interop.YGNodeStyleGetFlex(_handle);
        }
    }
    public float StyleGetFlexGrow()
    {
        unsafe
        {
            return Interop.YGNodeStyleGetFlexGrow(_handle);
        }
    }
    public float StyleGetFlexShrink()
    {
        unsafe
        {
            return Interop.YGNodeStyleGetFlexShrink(_handle);
        }
    }
    public YGValue StyleGetFlexBasis()
    {
        unsafe
        {
            return Interop.YGNodeStyleGetFlexBasis(_handle);
        }
    }
    public YGValue StyleGetPosition(YGEdge edge)
    {
        unsafe
        {
            return Interop.YGNodeStyleGetPosition(_handle, edge);
        }
    }
    public YGValue StyleGetMargin(YGEdge edge)
    {
        unsafe
        {
            return Interop.YGNodeStyleGetMargin(_handle, edge);
        }
    }
    public YGValue StyleGetPadding(YGEdge edge)
    {
        unsafe
        {
            return Interop.YGNodeStyleGetPadding(_handle, edge);
        }
    }
    public float StyleGetBorder(YGEdge edge)
    {
        unsafe
        {
            return Interop.YGNodeStyleGetBorder(_handle, edge);
        }
    }
    public float StyleGetGap(YGGutter gutter)
    {
        unsafe
        {
            return Interop.YGNodeStyleGetGap(_handle, gutter);
        }
    }
    public YGBoxSizing StyleGetBoxSizing()
    {
        unsafe
        {
            return Interop.YGNodeStyleGetBoxSizing(_handle);
        }
    }
    public float StyleGetAspectRatio()
    {
        unsafe
        {
            return Interop.YGNodeStyleGetAspectRatio(_handle);
        }
    }
    public YGValue StyleGetWidth()
    {
        unsafe
        {
            return Interop.YGNodeStyleGetWidth(_handle);
        }
    }
    public YGValue StyleGetHeight()
    {
        unsafe
        {
            return Interop.YGNodeStyleGetHeight(_handle);
        }
    }
    public YGValue StyleGetMinWidth()
    {
        unsafe
        {
            return Interop.YGNodeStyleGetMinWidth(_handle);
        }
    }
    public YGValue StyleGetMinHeight()
    {
        unsafe
        {
            return Interop.YGNodeStyleGetMinHeight(_handle);
        }
    }
    public YGValue StyleGetMaxWidth()
    {
        unsafe
        {
            return Interop.YGNodeStyleGetMaxWidth(_handle);
        }
    }
    public YGValue StyleGetMaxHeight()
    {
        unsafe
        {
            return Interop.YGNodeStyleGetMaxHeight(_handle);
        }
    }

}
