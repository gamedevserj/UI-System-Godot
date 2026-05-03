using System.Threading.Tasks;
using Godot;
using UISystem.Constants;

namespace UISystem.Helpers;

/// <summary>
/// Helper class to fade controls.
/// </summary>
public static class Fader
{
    private const float TransitionDuration = 0.25f;

    /// <summary>
    /// Initializes control by setting alpha of the target control's modulate to 0.
    /// </summary>
    /// <param name="target">Target control.</param>
    public static void Init(Control target)
    {
        target.Modulate = new Color(target.Modulate, 0);
    }

    /// <summary>
    /// Tweens the alpha value of the target control to 1.
    /// </summary>
    /// <param name="tree">Scene tree.</param>
    /// <param name="target">Target control.</param>
    /// <param name="instant">Whether transition should happen instantly.</param>
    /// <param name="duration">Tween duration.</param>
    public static async Task Show(SceneTree tree, Control target, bool instant = false, float duration = TransitionDuration)
    {
        var targetColor = new Color(target.Modulate, 1);
        if (instant)
        {
            InstantChange(target, targetColor);
            return;
        }

        await TweenColor(tree, target, targetColor, duration);
    }

    /// <summary>
    /// Tweens the alpha value of the target control to 0.
    /// </summary>
    /// <param name="tree">Scene tree.</param>
    /// <param name="target">Target control.</param>
    /// <param name="instant">Whether transition should happen instantly.</param>
    /// <param name="duration">Tween duration.</param>
    public static async Task Hide(SceneTree tree, Control target, bool instant = false, float duration = TransitionDuration)
    {
        var targetColor = new Color(target.Modulate, 0);
        if (instant)
        {
            InstantChange(target, targetColor);
            return;
        }

        await TweenColor(tree, target, targetColor, duration);
    }

    private static async Task TweenColor(SceneTree tree, Control target, Color targetColor, float duration)
    {
        Tween tween = tree.CreateTween();
        tween.SetPauseMode(Tween.TweenPauseMode.Process);
        tween.TweenProperty(target, PropertyConstants.Modulate, targetColor, duration);
        await tree.ToSignal(tween, Tween.SignalName.Finished);
    }

    private static void InstantChange(Control target, Color targetColor)
    {
        target.Modulate = targetColor;
    }
}
