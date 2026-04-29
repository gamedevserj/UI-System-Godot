using Godot;

namespace UISystem.Hovering;

/// <summary>
/// Defines contract for tweening states.
/// </summary>
public interface IHoverTweener
{
    /// <summary>
    /// Performs tween based on the control drawing mode.
    /// </summary>
    /// <param name="tween">Tween to perform.</param>
    /// <param name="mode">Control drawing mode.</param>
    void Tween(Tween tween, ControlDrawMode mode);

    /// <summary>
    /// Resets the tween.
    /// </summary>
    /// <param name="tween">Tween to reset.</param>
    void Reset(Tween tween);
}
