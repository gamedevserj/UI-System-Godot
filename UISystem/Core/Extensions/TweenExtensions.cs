using Godot;
using UISystem.Constants;

namespace UISystem.Core.Extensions;
internal static class TweenExtensions
{
    public static Tween TweenControlPosition(this Tween tween, Control target, Vector2 position, float duration)
    {
        tween.TweenProperty(target, PropertyConstants.Position, position, duration);
        return tween;
    }

    public static Tween TweenControlGlobalPosition(this Tween tween, Control target, Vector2 position, float duration)
    {
        tween.TweenProperty(target, PropertyConstants.GlobalPosition, position, duration);
        return tween;
    }

    public static void TweenModulate(this Tween tween, CanvasItem target, Color color, float duration, bool self = false)
    {
        if (!self)
            tween.TweenProperty(target, PropertyConstants.Modulate, color, duration);
        else
            tween.TweenProperty(target, PropertyConstants.SelfModulate, color, duration);
    }

    public static Tween TweenAlpha(this Tween tween, CanvasItem target, float alpha, float duration, bool self = false)
    {
        if (!self)
            tween.TweenProperty(target, PropertyConstants.Modulate, new Color(target.Modulate, alpha), duration);
        else
            tween.TweenProperty(target, PropertyConstants.SelfModulate, new Color(target.SelfModulate, alpha), duration);
        return tween;
    }

    public static Tween TweenControlSize(this Tween tween, Control target, Vector2 size, float duration)
    {
        tween.TweenProperty(target, PropertyConstants.Size, size, duration);
        return tween;
    }

}
