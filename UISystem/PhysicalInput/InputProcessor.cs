using Godot;
using UISystem.Core.Constants;
using UISystem.Core.MenuSystem;
using UISystem.Core.PhysicalInput;
using UISystem.Core.PopupSystem;
using UISystem.MenuSystem;
using UISystem.PopupSystem;

namespace UISystem.PhysicalInput;
internal class InputProcessor : IInputProcessor<InputEvent>
{

    private IInputReceiver<InputEvent> _menuInputReceiver;
    private IInputReceiver<InputEvent> _popupInputReceiver;
    private IInputReceiver<InputEvent> _activeReceiver;

    public InputProcessor()
    {
        MenusManager<InputEvent, MenuType>.OnControllerSwitch += OnMenuControllerSwitch;
        PopupsManager<InputEvent, PopupType, PopupResult>.OnControllerSwitch += OnPopupControllerSwitch;
    }
    ~InputProcessor()
    {
        MenusManager<InputEvent, MenuType>.OnControllerSwitch -= OnMenuControllerSwitch;
        PopupsManager<InputEvent, PopupType, PopupResult>.OnControllerSwitch -= OnPopupControllerSwitch;
    }

    public void ProcessInput(InputEvent inputEvent)
    {
        if (_activeReceiver == null || !_activeReceiver.CanReceivePhysicalInput)
            return;

        if (inputEvent.IsActionPressed(InputsData.ReturnButton))
            _activeReceiver.OnReturnButtonDown();

        if (inputEvent.IsActionPressed(InputsData.PauseButton))
            _activeReceiver.OnPauseButtonDown();

        if (inputEvent.IsPressed())
            _activeReceiver.OnAnyButtonDown(inputEvent);
    }

    private void OnPopupControllerSwitch(IInputReceiver<InputEvent> inputReceiver)
    {
        _popupInputReceiver = inputReceiver;
        _activeReceiver = _popupInputReceiver ?? _menuInputReceiver;
    }

    private void OnMenuControllerSwitch(IInputReceiver<InputEvent> inputReceiver)
    {
        _activeReceiver = _menuInputReceiver = inputReceiver;
    }

}
