using Godot;
using UISystem.Core.PhysicalInput;

namespace UISystem.PhysicalInput;
public interface IRebindInputReceiver : IInputReceiver
{

    void OnAnyButtonDown(InputEvent inputEvent);

}
