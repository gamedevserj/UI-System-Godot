using Godot;
using UISystem.Core.PhysicalInput;

namespace UISystem.PhysicalInput;

/// <summary>
/// Defines contract for input receiver that handles input rebinding.
/// </summary>
public interface IRebindInputReceiver : IInputReceiver
{
    /// <summary>
    /// Action performed when any key is pressed.
    /// </summary>
    /// <param name="inputEvent">Key or button that was pressed.</param>
    void OnAnyButtonDown(InputEvent inputEvent);
}
