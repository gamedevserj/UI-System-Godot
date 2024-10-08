using Godot;
using UISystem.Common.Enums;
using UISystem.Common.Structs;
using UISystem.Constants;

namespace UISystem.Common.Extensions;
public static class TweenExtensions
{

    public static void TweenControlSize(this Tween tween, bool parallel, Control target, Vector2 size, float duration,
        ResizableControlSettings settings = default)
    {
        tween.ControlSize(parallel, target, size, duration);

        if (!settings.IsInitialized)
            return;

        float multiplierX = GetHorizontalMultiplier(settings.HorizontalDirection);
        float multiplierY = GetVerticalMultiplier(settings.VerticalDirection);
        Vector2 sizeDifference = size - settings.OriginalSize;
        Vector2 position = settings.OriginalPosition - sizeDifference * new Vector2(multiplierX, multiplierY);

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

    public static void TweenCanvasItemAlpha(this Tween tween, bool parallel, CanvasItem target, float alpha, float duration, bool self = false)
    {
        if (parallel)
            tween.Parallel();
        if (!self)
            tween.TweenProperty(target, PropertyConstants.Modulate, new Color(target.Modulate, alpha), duration);
        else
            tween.TweenProperty(target, PropertyConstants.SelfModulate, new Color(target.SelfModulate, alpha), duration);
    }

    private static void ControlSize(this Tween tween, bool parallel, Control target, Vector2 size, float duration)
    {
        if (parallel)
            tween.Parallel();
        tween.TweenProperty(target, PropertyConstants.Size, size, duration);
    }

    private static float GetHorizontalMultiplier(HorizontalDirection direction) => direction switch
    {
        HorizontalDirection.FromLeft => 0,
        HorizontalDirection.FromCenter => 0.5f,
        HorizontalDirection.FromRight => 1,
        _ => 0,
    };

    private static float GetVerticalMultiplier(VerticalDirection direction) => direction switch
    {
        VerticalDirection.FromTop => 0,
        VerticalDirection.FromCenter => 0.5f,
        VerticalDirection.FromBottom => 1,
        _ => 0,
    };

}
