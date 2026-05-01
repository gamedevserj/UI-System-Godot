using Godot;
using UISystem.Core.Constants;
using UISystem.Core.MenuSystem;
using UISystem.Core.PhysicalInput;
using UISystem.Core.PopupSystem;

namespace UISystem.PhysicalInput;

/// <summary>
/// Input processor. Controls who receives OnReturnButtonDown, OnPauseButtonDown events.
/// Also handles OnAnyButtonDown for rebinding.
/// </summary>
internal class InputProcessor : IInputProcessor<InputEvent>
{
    private readonly IMenusManager _menusManager;
    private readonly IPopupsManager _popupsManager;

    private IInputReceiver _menuInputReceiver;
    private IInputReceiver _activeReceiver;
    private IRebindInputReceiver _rebindInputReceiver;

    /// <summary>
    /// Initializes a new instance of the <see cref="InputProcessor"/> class.
    /// </summary>
    /// <param name="menusManager">Menus manager.</param>
    /// <param name="popupsManager">Popups manager.</param>
    public InputProcessor(IMenusManager menusManager, IPopupsManager popupsManager)
    {
        _menusManager = menusManager;
        _menusManager.OnControllerSwitch += OnMenuControllerSwitch;
        _popupsManager = popupsManager;
        _popupsManager.OnControllerSwitch += OnPopupControllerSwitch;
    }

    /// <summary>
    /// Finalizes an instance of the <see cref="InputProcessor"/> class.
    /// </summary>
    ~InputProcessor()
    {
        _menusManager.OnControllerSwitch += OnMenuControllerSwitch;
        _popupsManager.OnControllerSwitch += OnPopupControllerSwitch;
    }

    /// <inheritdoc/>
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
