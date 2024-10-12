using Godot;
using UISystem.Transitions.Enums;

namespace UISystem.Transitions.Structs;
public readonly struct ResizableControlSettings
{

    public readonly Vector2 OriginalPosition = Vector2.Zero;
    public readonly Vector2 OriginalSize = Vector2.Zero;
    public readonly Vector2 CenterPosition = Vector2.Zero;
    public readonly HorizontalDirection HorizontalDirection = HorizontalDirection.FromLeft;
    public readonly VerticalDirection VerticalDirection = VerticalDirection.FromTop;
    public readonly bool IsInitialized = false; // using it to not tween position when settings are default

    public ResizableControlSettings(Vector2 originalPosition, Vector2 originalSize,
        HorizontalDirection horizontalDirection,
        VerticalDirection verticalDirection)
    {
        OriginalPosition = originalPosition;
        OriginalSize = originalSize;
        CenterPosition = originalPosition + originalSize * 0.5f;
        HorizontalDirection = horizontalDirection;
        VerticalDirection = verticalDirection;
        IsInitialized = true;
    }

}
