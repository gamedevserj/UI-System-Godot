using Godot;
using UISystem.Common.Enums;
using UISystem.Common.Structs;
using UISystem.Constants;

namespace UISystem.Extensions;
public static class TweenExtensions
{

    public static void TweenControlSize(this Tween tween, bool parallel, Control target, Vector2 size, float duration,
        SizeSettings sizeSettings = default)
    {
        tween.ControlSize(parallel, target, size, duration);

        if (!sizeSettings.IsInitialized)
            return;

        float multiplierX = GetHorizontalMultiplier(sizeSettings.HorizontalDirection);
        float multiplierY = GetVerticalMultiplier(sizeSettings.VerticalDirection);
        Vector2 sizeDifference = size - sizeSettings.OriginalSize;
        Vector2 position = sizeSettings.OriginalPosition - sizeDifference * new Vector2(multiplierX, multiplierY);

        tween.TweenNode2DPosition(parallel, target, position, duration);
    }

    public static void TweenNode2DPosition(this Tween tween, bool parallel, Control target, Vector2 position, float duration)
    {
        if (parallel)
            tween.Parallel();
        tween.Parallel().TweenProperty(target, PropertyConstants.Position, position, duration);
    }

    public static void TweenCanvasItemSelfModulate(this Tween tween, bool parallel, CanvasItem target, Color color, float duration)
    {
        if (parallel)
            tween.Parallel();
        tween.Parallel().TweenProperty(target, PropertyConstants.SelfModulate, color, duration);
    }

    private static void ControlSize(this Tween tween, bool parallel, Control target, Vector2 size, float duration)
    {
        if (parallel)
            tween.Parallel();
        tween.TweenProperty(target, PropertyConstants.Size, size, duration);
    }

    private static float GetHorizontalMultiplier(HorizontalControlSizeChangeDirection direction) => direction switch
    {
        HorizontalControlSizeChangeDirection.FromLeft => 0,
        HorizontalControlSizeChangeDirection.FromCenter => 0.5f,
        HorizontalControlSizeChangeDirection.FromRight => 1,
        _ => 0,
    };

    private static float GetVerticalMultiplier(VerticalControlSizeChangeDirection direction) => direction switch
    {
        VerticalControlSizeChangeDirection.FromTop => 0,
        VerticalControlSizeChangeDirection.FromCenter => 0.5f,
        VerticalControlSizeChangeDirection.FromBottom => 1,
        _ => 0,
    };

}
