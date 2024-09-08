using Godot;
using System;
using UISystem.Constants;

namespace UISystem.Common.Helpers;
public static class Fader
{

    private const float TransitionDuration = 0.25f;

    public static void Init(Control target)
    {
        target.Modulate = new Color(target.Modulate.R, target.Modulate.G, target.Modulate.B, 0);
    }

    public static void Show(SceneTree tree, Control target, Action onComplete = null, bool instant = false)
    {
        var targetColor = new Color(target.Modulate, 1);
        if (instant)
        {
            InstantChange(target, targetColor, onComplete);
            return;
        }

        TweenColor(tree, target, targetColor, onComplete);
    }

    public static void Hide(SceneTree tree, Control target, Action onComplete = null, bool instant = false)
    {
        var targetColor = new Color(target.Modulate, 0);
        if (instant)
        {
            InstantChange(target, targetColor, onComplete);
            return;
        }

        TweenColor(tree, target, targetColor, onComplete);
    }

    private static void TweenColor(SceneTree tree, Control target, Color targetColor, Action onComplete = null)
    {
        Tween tween = tree.CreateTween();
        tween.SetPauseMode(Tween.TweenPauseMode.Process);
        tween.TweenProperty(target, PropertyConstants.Modulate, targetColor, TransitionDuration);
        tween.TweenCallback(Callable.From(() => onComplete?.Invoke()));
    }

    private static void InstantChange(Control target, Color targetColor, Action onComplete = null)
    {
        target.Modulate = targetColor;
        onComplete?.Invoke();
    }

}
