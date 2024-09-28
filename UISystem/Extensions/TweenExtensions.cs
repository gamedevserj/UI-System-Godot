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

        // in order to change size properly when direction is set to center, it needs to be parallel
        tween.TweenNode2DPosition(true, target, position, duration);
    }

    public static void TweenNode2DPosition(this Tween tween, bool parallel, Control target, Vector2 position, float duration)
    {
        if (parallel)
            tween.Parallel();
        tween.TweenProperty(target, PropertyConstants.Position, position, duration);
    }

    public static void TweenCanvasItemSelfModulate(this Tween tween, bool parallel, CanvasItem target, Color color, float duration)
    {
        if (parallel)
            tween.Parallel();
        tween.TweenProperty(target, PropertyConstants.SelfModulate, color, duration);
    }

    public static void TweenCanvasItemModulate(this Tween tween, bool parallel, CanvasItem target, Color color, float duration)
    {
        if (parallel)
            tween.Parallel();
        tween.TweenProperty(target, PropertyConstants.Modulate, color, duration);
    }

    public static void TweenCanvasItemAlpha(this Tween tween, bool parallel, CanvasItem target, float alpha, float duration)
    {
        if (parallel)
            tween.Parallel();
        tween.TweenProperty(target, PropertyConstants.Modulate, new Color(target.Modulate, alpha), duration);
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
