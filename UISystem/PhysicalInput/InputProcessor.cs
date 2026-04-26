using Godot;
using UISystem.Core.Constants;
using UISystem.Core.MenuSystem;
using UISystem.Core.PhysicalInput;
using UISystem.Core.PopupSystem;
using UISystem.PopupSystem;

namespace UISystem.PhysicalInput;
internal class InputProcessor : IInputProcessor<InputEvent>
{
    private readonly IMenusManager _menusManager;
    private readonly IPopupsManager<PopupResult> _popupsManager;

    private IInputReceiver _menuInputReceiver;
    private IInputReceiver _activeReceiver;
    private IRebindInputReceiver _rebindInputReceiver;

    public InputProcessor(IMenusManager menusManager, IPopupsManager<PopupResult> popupsManager)
    {
        _menusManager = menusManager;
        _menusManager.OnControllerSwitch += OnMenuControllerSwitch;
        _popupsManager = popupsManager;
        _popupsManager.OnControllerSwitch += OnPopupControllerSwitch;

    }
    ~InputProcessor()
    {
        _menusManager.OnControllerSwitch += OnMenuControllerSwitch;
        _popupsManager.OnControllerSwitch += OnPopupControllerSwitch;
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
            _rebindInputReceiver?.OnAnyButtonDown(inputEvent);
    }

    private void OnPopupControllerSwitch(IInputReceiver inputReceiver)
    {
        _activeReceiver = inputReceiver ?? _menuInputReceiver;
    }

    private void OnMenuControllerSwitch(IInputReceiver inputReceiver)
    {
        _activeReceiver = _menuInputReceiver = inputReceiver;
        if (_activeReceiver is IRebindInputReceiver rebindInputReceiver)
            _rebindInputReceiver = rebindInputReceiver;
        else
            _rebindInputReceiver = null;
    }

}
