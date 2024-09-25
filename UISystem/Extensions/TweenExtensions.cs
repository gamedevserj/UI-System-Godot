using Godot;
using UISystem.Common.Enums;
using UISystem.Constants;

namespace UISystem.Extensions;
public static class TweenExtensions
{

    public static void TweenControlSize(this Tween tween, bool parallel, Control target, Vector2 sizeIncrease, float duration,
        Vector2 originalPosition,
        Vector2 originalSize,
        HorizontalControlSizeChangeDirection horizontalDirection = HorizontalControlSizeChangeDirection.FromLeft,
        VerticalControlSizeChangeDirection verticalDirection = VerticalControlSizeChangeDirection.FromTop)
    {
        tween.TweenControlSize(parallel, target, originalSize + sizeIncrease, duration);
        float multiplierX = GetHorizontalMultiplier(horizontalDirection);
        float multiplierY = GetVerticalMultiplier(verticalDirection);
        Vector2 position = originalPosition - sizeIncrease * new Vector2(multiplierX, multiplierY);

        tween.Parallel().TweenProperty(target, PropertyConstants.Position, position, duration);
    }   

    public static void TweenControlSize(this Tween tween, bool parallel, Control target, Vector2 size, float duration)
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
