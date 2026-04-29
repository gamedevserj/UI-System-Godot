using Godot;
using UISystem.Transitions.Enums;

namespace UISystem.Transitions.Structs;

/// <summary>
/// Stores original element size and position, and directions in which element will grow.
/// </summary>
public readonly struct ResizableControlSettings
{
    /// <summary>
    /// Original position.
    /// </summary>
    public readonly Vector2 OriginalPosition = Vector2.Zero;

    /// <summary>
    /// Original size.
    /// </summary>
    public readonly Vector2 OriginalSize = Vector2.Zero;

    /// <summary>
    /// Element's center.
    /// </summary>
    public readonly Vector2 CenterPosition = Vector2.Zero;

    /// <summary>
    /// Horizontal growth direction.
    /// </summary>
    public readonly HorizontalDirection HorizontalDirection = HorizontalDirection.FromLeft;

    /// <summary>
    /// Vertical growth direction.
    /// </summary>
    public readonly VerticalDirection VerticalDirection = VerticalDirection.FromTop;

    /// <summary>
    /// Initializes a new instance of the <see cref="ResizableControlSettings"/> struct.
    /// </summary>
    /// <param name="originalPosition">Original position.</param>
    /// <param name="originalSize">Original size.</param>
    /// <param name="horizontalDirection">Horizontal growth direction.</param>
    /// <param name="verticalDirection">Vertical growth direction.</param>
    public ResizableControlSettings(
        Vector2 originalPosition,
        Vector2 originalSize,
        HorizontalDirection horizontalDirection,
        VerticalDirection verticalDirection)
    {
        OriginalPosition = originalPosition;
        OriginalSize = originalSize;
        CenterPosition = originalPosition + (originalSize * 0.5f);
        HorizontalDirection = horizontalDirection;
        VerticalDirection = verticalDirection;
    }
}
