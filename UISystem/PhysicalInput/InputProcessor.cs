using Godot;
using UISystem.Core.Constants;
using UISystem.Core.PhysicalInput;

namespace UISystem.PhysicalInput;
internal class InputProcessor : IInputProcessor<InputEvent>
{
    public bool IsPressingReturnToPreviousMenuButton(InputEvent inputEvent)
    {
        if (inputEvent.IsActionPressed(InputsData.ReturnToPreviousMenu))
            return true;
        return false;
    }

    public bool IsPressingPause(InputEvent inputEvent)
    {
        if(inputEvent.IsPressed() && inputEvent.IsAction(InputsData.PauseButton))
            return true;
        return false;
    }

    public bool IsPressingAnyKey(InputEvent inputEvent)
    {
        if(inputEvent.IsPressed()) return true;
        return false;
    }
}
