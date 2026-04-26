using Godot;
using System;
using System.Threading.Tasks;
using UISystem.Constants;

namespace UISystem.Helpers;
public static class Fader
{

    private const float TransitionDuration = 0.25f;

    public static void Init(Control target)
    {
        target.Modulate = new Color(target.Modulate, 0);
    }

    public static async Task Show(SceneTree tree, Control target, bool instant = false)
    {
        var targetColor = new Color(target.Modulate, 1);
        if (instant)
        {
            InstantChange(target, targetColor);
            return;
        }

        await TweenColor(tree, target, targetColor);
    }

    public static async Task Hide(SceneTree tree, Control target, bool instant = false)
    {
        var targetColor = new Color(target.Modulate, 0);
        if (instant)
        {
            InstantChange(target, targetColor);
            return;
        }

        await TweenColor(tree, target, targetColor);
    }

    private static async Task TweenColor(SceneTree tree, Control target, Color targetColor)
    {
        Tween tween = tree.CreateTween();
        tween.SetPauseMode(Tween.TweenPauseMode.Process);
        tween.TweenProperty(target, PropertyConstants.Modulate, targetColor, TransitionDuration);
        await tree.ToSignal(tween, Tween.SignalName.Finished);
    }

    private static void InstantChange(Control target, Color targetColor, Action onComplete = null)
    {
        target.Modulate = targetColor;
        onComplete?.Invoke();
    }

}
