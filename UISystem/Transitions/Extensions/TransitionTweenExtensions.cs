﻿using Godot;
using UISystem.Extensions;
using UISystem.Transitions.Enums;
using UISystem.Transitions.Extensions;
using UISystem.Transitions.Structs;

namespace UISystem.Transitions.Extensions;
public static class TransitionTweenExtensions
{

    // for transitions that scale object to center
    public static void TweenControlSize(this Tween tween, bool parallel, Control target, Vector2 size, float duration,
        ResizableControlSettings settings)
    {
        if (parallel)
            tween.Parallel().TweenControlSize(target, size, duration);
        else
            tween.TweenControlSize(target, size, duration);

        float multiplierX = GetHorizontalMultiplier(settings.HorizontalDirection);
        float multiplierY = GetVerticalMultiplier(settings.VerticalDirection);
        Vector2 sizeDifference = size - settings.OriginalSize;
        Vector2 position = settings.OriginalPosition - sizeDifference * new Vector2(multiplierX, multiplierY);

        // in order to change size properly when direction is set to center or bottom/right, it needs to be parallel
        tween.Parallel().TweenControlPosition(target, position, duration);
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
