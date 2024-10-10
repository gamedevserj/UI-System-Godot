using Godot;
using UISystem.Constants;

namespace UISystem.Core.Extensions;
internal static class TweenExtensions
{
    public static void TweenControlPosition(this Tween tween, bool parallel, Control target, Vector2 position, float duration)
    {
        if (parallel)
            tween.Parallel();
        tween.TweenProperty(target, PropertyConstants.Position, position, duration);
    }

    public static void TweenSelfModulate(this Tween tween, bool parallel, CanvasItem target, Color color, float duration)
    {
        if (parallel)
            tween.Parallel();
        tween.TweenProperty(target, PropertyConstants.SelfModulate, color, duration);
    }

    public static void TweenModulate(this Tween tween, bool parallel, CanvasItem target, Color color, float duration)
    {
        if (parallel)
            tween.Parallel();
        tween.TweenProperty(target, PropertyConstants.Modulate, color, duration);
    }

    public static void TweenAlpha(this Tween tween, bool parallel, CanvasItem target, float alpha, float duration, bool self = false)
    {
        if (parallel)
            tween.Parallel();
        if (!self)
            tween.TweenProperty(target, PropertyConstants.Modulate, new Color(target.Modulate, alpha), duration);
        else
            tween.TweenProperty(target, PropertyConstants.SelfModulate, new Color(target.SelfModulate, alpha), duration);
    }

    public static void TweenControlSize(this Tween tween, bool parallel, Control target, Vector2 size, float duration)
    {
        if (parallel)
            tween.Parallel();
        tween.TweenProperty(target, PropertyConstants.Size, size, duration);
    }

}
