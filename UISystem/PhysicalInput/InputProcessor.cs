using Godot;
using UISystem.Core.Constants;
using UISystem.Core.PhysicalInput;

namespace UISystem.PhysicalInput;
internal class InputProcessor : IInputProcessor<InputEvent>
{

    public void ProcessInput(InputEvent inputEvent, IInputReceiver<InputEvent> inputReceiver)
    {
        if (inputEvent.IsActionPressed(InputsData.ReturnButton))
            inputReceiver.OnReturnButtonDown();

        if (inputEvent.IsActionPressed(InputsData.PauseButton))
            inputReceiver.OnPauseButtonDown();

        if (inputEvent.IsPressed())
            inputReceiver.OnAnyButtonDown(inputEvent);
    }

}
