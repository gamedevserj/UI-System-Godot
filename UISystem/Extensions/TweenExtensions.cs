using Godot;
using UISystem.Constants;

namespace UISystem.Extensions;

/// <summary>
/// Class containing extension methods for tweening.
/// </summary>
internal static class TweenExtensions
{
    /// <summary>
    /// Tweens control's local position.
    /// </summary>
    /// <param name="tween">Tween.</param>
    /// <param name="target">Target control.</param>
    /// <param name="position">Target position.</param>
    /// <param name="duration">Tween duration.</param>
    /// <returns>Instance of tween.</returns>
    public static Tween TweenControlPosition(this Tween tween, Control target, Vector2 position, float duration)
    {
        tween.TweenProperty(target, PropertyConstants.Position, position, duration);
        return tween;
    }

    /// <summary>
    /// Tweens control's global position.
    /// </summary>
    /// <param name="tween">Tween.</param>
    /// <param name="target">Target control.</param>
    /// <param name="position">Target position.</param>
    /// <param name="duration">Tween duration.</param>
    /// <returns>Instance of tween.</returns>
    public static Tween TweenControlGlobalPosition(this Tween tween, Control target, Vector2 position, float duration)
    {
        tween.TweenProperty(target, PropertyConstants.GlobalPosition, position, duration);
        return tween;
    }

    /// <summary>
    /// Tweens canvas item's color.
    /// </summary>
    /// <param name="tween">Tween.</param>
    /// <param name="target">Target canvas item.</param>
    /// <param name="color">Target color.</param>
    /// <param name="duration">Tween duration.</param>
    /// <param name="self">Whether color change is applied to children too.</param>
    /// <returns>Instance of tween.</returns>
    public static Tween TweenModulate(this Tween tween, CanvasItem target, Color color, float duration, bool self = false)
    {
        if (!self)
            tween.TweenProperty(target, PropertyConstants.Modulate, color, duration);
        else
            tween.TweenProperty(target, PropertyConstants.SelfModulate, color, duration);
        return tween;
    }

    /// <summary>
    /// Tweens canvas item's alpha value.
    /// </summary>
    /// <param name="tween">Tween.</param>
    /// <param name="target">Target canvas item.</param>
    /// <param name="value">Target alpha value.</param>
    /// <param name="duration">Tween duration.</param>
    /// <param name="self">Whether color change is applied to children too.</param>
    /// <returns>Instance of tween.</returns>
    public static Tween TweenAlpha(this Tween tween, CanvasItem target, float value, float duration, bool self = false)
    {
        if (!self)
            tween.TweenProperty(target, PropertyConstants.Modulate, new Color(target.Modulate, value), duration);
        else
            tween.TweenProperty(target, PropertyConstants.SelfModulate, new Color(target.SelfModulate, value), duration);
        return tween;
    }

    /// <summary>
    /// Tweens control's size.
    /// </summary>
    /// <param name="tween">Tween.</param>
    /// <param name="target">Target control.</param>
    /// <param name="size">Target size.</param>
    /// <param name="duration">Tween duration.</param>
    /// <returns>Instance of tween.</returns>
    public static Tween TweenControlSize(this Tween tween, Control target, Vector2 size, float duration)
    {
        tween.TweenProperty(target, PropertyConstants.Size, size, duration);
        return tween;
    }
}
