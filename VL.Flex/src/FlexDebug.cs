using System.Text.Json;
using VL.Flex.Internals;
using YogaSharp;

namespace VL.Flex
{
    public static class FlexDebug
    {
        public static string? DebugFlexNodeStyle(FlexNode? node)
        {
            unsafe
            {
                if (node != null)
                {
                    var handle = node.Handle;

                    return JsonSerializer.Serialize(new
                    {
                        direction = handle->GetDirection(),
                        flexDirection = handle->GetFlexDirection(),
                        justifyContent = handle->GetJustifyContent(),
                        alignContent = handle->GetAlignContent(),
                        alignItems = handle->GetAlignItems(),
                        alignSelf = handle->GetAlignSelf(),
                        positionType = handle->GetPositionType(),
                        flexWrap = handle->GetFlexWrap(),
                        overflow = handle->GetOverflow(),
                        display = handle->GetDisplay(),
                        flex = handle->GetFlex(),
                        flexGrow = handle->GetFlexGrow(),
                        flexShrink = handle->GetFlexShrink(),
                        flexBasis = handle->GetFlexBasis(),

                        position = new
                        {
                            left = handle->GetPosition(YGEdge.Left),
                            right = handle->GetPosition(YGEdge.Right),
                            top = handle->GetPosition(YGEdge.Top),
                            bottom = handle->GetPosition(YGEdge.Bottom),
                            horizontal = handle->GetPosition(YGEdge.Horizontal),
                            vertical = handle->GetPosition(YGEdge.Vertical),
                            all = handle->GetPosition(YGEdge.All),
                        },

                        margin = new
                        {
                            left = handle->GetMargin(YGEdge.Left),
                            right = handle->GetMargin(YGEdge.Right),
                            top = handle->GetMargin(YGEdge.Top),
                            bottom = handle->GetMargin(YGEdge.Bottom),
                            horizontal = handle->GetMargin(YGEdge.Horizontal),
                            vertical = handle->GetMargin(YGEdge.Vertical),
                            all = handle->GetMargin(YGEdge.All),
                        },

                        padding = new
                        {
                            left = handle->GetPadding(YGEdge.Left),
                            right = handle->GetPadding(YGEdge.Right),
                            top = handle->GetPadding(YGEdge.Top),
                            bottom = handle->GetPadding(YGEdge.Bottom),
                            horizontal = handle->GetPadding(YGEdge.Horizontal),
                            vertical = handle->GetPadding(YGEdge.Vertical),
                            all = handle->GetPadding(YGEdge.All),
                        },

                        border = new
                        {
                            left = handle->GetBorder(YGEdge.Left),
                            right = handle->GetBorder(YGEdge.Right),
                            top = handle->GetBorder(YGEdge.Top),
                            bottom = handle->GetBorder(YGEdge.Bottom),
                            horizontal = handle->GetBorder(YGEdge.Horizontal),
                            vertical = handle->GetBorder(YGEdge.Vertical),
                            all = handle->GetBorder(YGEdge.All),
                        },

                        gap = new
                        {
                            column = handle->GetGap(YGGutter.Column),
                            row = handle->GetGap(YGGutter.Row),
                            all = handle->GetGap(YGGutter.All),
                        },

                        aspectRatio = handle->GetAspectRatio(),
                        width = handle->GetWidth(),
                        height = handle->GetHeight(),
                        minWidth = handle->GetMinWidth(),
                        minHeight = handle->GetMinHeight(),
                        maxWidth = handle->GetMaxWidth(),
                        maxHeight = handle->GetMaxHeight(),
                    }, Store.GetInternals().SerializerOptions);
                }

                return null;
            }
        }

        public static string? DebugFlexNodeConfig(FlexNode? node)
        {
            unsafe
            {
                if (node != null)
                {
                    var handle = node.Handle;
                    if (handle != null)
                    {
                        var config = handle->GetConfig();
                        if (config != null)
                        {
                            return JsonSerializer.Serialize(new
                            {
                                useWebDefaults = config->GetUseWebDefaults(),
                                pointScaleFactor = config->GetPointScaleFactor(),
                                errata = config->GetErrata(),
                            }, Store.GetInternals().SerializerOptions);
                        }
                    }
                }

                return null;
            }
        }
    }
}

