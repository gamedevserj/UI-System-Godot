using Godot;
using System;
using UISystem.Common.Enums;
using UISystem.Common.Structs;
using UISystem.Constants;

namespace UISystem.Extensions;
public static class TweenExtensions
{

    public static void TweenControlSize(this Tween tween, bool parallel, Control target, Vector2 size, float duration,
        TweenSizeSettings sizeSettings = default)
    {
        tween.TweenControlSize(parallel, target, size, duration);

        if (!sizeSettings.IsInitialized)
            return;

        float multiplierX = GetHorizontalMultiplier(sizeSettings.HorizontalDirection);
        float multiplierY = GetVerticalMultiplier(sizeSettings.VerticalDirection);
        Vector2 sizeDifference = size - sizeSettings.OriginalSize;
        Vector2 position = sizeSettings.OriginalPosition - sizeDifference * new Vector2(multiplierX, multiplierY);

        tween.TweenControlPosition(parallel, target, position, duration);
    }

    public static void TweenControlPosition(this Tween tween, bool parallel, Control target, Vector2 position, float duration)
    {
        if (parallel)
            tween.Parallel();
        tween.Parallel().TweenProperty(target, PropertyConstants.Position, position, duration);
    }

    private static void TweenControlSize(this Tween tween, bool parallel, Control target, Vector2 size, float duration)
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
