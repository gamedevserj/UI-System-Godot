using Godot;
using UISystem.Constants;
using UISystem.Core.Constants;
using UISystem.Core.Elements.Interfaces;
using UISystem.Core.MenuSystem.Interfaces;
using UISystem.Core.PopupSystem.Interfaces;
using UISystem.Core.Transitions.Interfaces;
using UISystem.Elements.ElementViews;
using UISystem.MenuSystem.Constants;
using UISystem.MenuSystem.Models;
using UISystem.MenuSystem.SettingsMenu;
using UISystem.MenuSystem.Views;
using UISystem.Transitions.Interfaces;
using UISystem.Transitions;

namespace UISystem.MenuSystem.Controllers;
internal class RebindKeysMenuController : SettingsMenuController<RebindKeysMenuView, RebindKeysMenuModel, Node, IFocusableControl>
{

    private const float PanelDuration = 0.5f;
    private const float ElementsDuration = 0.25f;

    public override int Type => MenuType.RebindKeys;

    public RebindKeysMenuController(string prefab, RebindKeysMenuModel model, IMenusManager menusManager, Node parent,
        IPopupsManager popupsManager) : base(prefab, model, menusManager, parent, popupsManager)
    { }

    protected override void ProcessInput(InputEvent key)
    {
        if (key.IsPressed() && _model.IsRebinding)
            _model.RebindKey(key);
        else
            base.ProcessInput(key);
    }

    private static void UpdateButtonView(RebindableKeyButtonView button, string action, int index)
    {
        var e = InputMap.ActionGetEvents(action)[index];
        button.TextureRect.Texture = (Texture2D)GD.Load(Icons.GetIcon(e));
    }

    private void OnButtonDown(RebindableKeyButtonView button, string action, int index)
    {
        button.TextureRect.Texture = (Texture2D)GD.Load(Icons.EllipsisImage);
        SwitchRebindingButtonFocusability(button, false);

        _model.StartRebinding(action, index, () =>
        {
            SwitchRebindingButtonFocusability(button, true);
            UpdateButtonView(button, action, index);
        });
    }

    private void SwitchRebindingButtonFocusability(RebindableKeyButtonView button, bool allowFocus)
    {
        (button as IFocusableControl).SwitchFocusAvailability(allowFocus);
        (button as IFocusableControl).SwitchFocus(allowFocus);
        if (!allowFocus)
        {
            button.ReleaseFocus();
        }
        if (allowFocus)
        {
            button.GetViewport().SetInputAsHandled();
            button.GrabFocus();
            _lastSelectedElement = button;
        }
    }

    protected override void SetupElements()
    {
        _view.ReturnButton.ButtonDown += OnReturnButtonDown;
        _view.ResetButton.ButtonDown += OnResetToDefaultButtonDown;

        _view.MoveLeft.ButtonDown += () =>
        OnButtonDown(_view.MoveLeft, InputsData.MoveLeft, InputsData.KeyboardEventIndex);
        _view.MoveLeftJoystick.ButtonDown += () =>
        OnButtonDown(_view.MoveLeftJoystick, InputsData.MoveLeft, InputsData.JoystickEventIndex);

        _view.MoveRight.ButtonDown += () =>
        OnButtonDown(_view.MoveRight, InputsData.MoveRight, InputsData.KeyboardEventIndex);
        _view.MoveRightJoystick.ButtonDown += () =>
        OnButtonDown(_view.MoveRightJoystick, InputsData.MoveRight, InputsData.JoystickEventIndex);

        _view.Jump.ButtonDown += () =>
        OnButtonDown(_view.Jump, InputsData.Jump, InputsData.KeyboardEventIndex);
        _view.JumpJoystick.ButtonDown += () =>
        OnButtonDown(_view.JumpJoystick, InputsData.Jump, InputsData.JoystickEventIndex);

        UpdateAllButtonViews();
        DefaultSelectedElement = _view.ReturnButton;
    }

    private void UpdateAllButtonViews()
    {
        string action = InputsData.MoveLeft;
        UpdateButtonView(_view.MoveLeft, action, InputsData.KeyboardEventIndex);
        UpdateButtonView(_view.MoveLeftJoystick, action, InputsData.JoystickEventIndex);

        action = InputsData.MoveRight;
        UpdateButtonView(_view.MoveRight, action, InputsData.KeyboardEventIndex);
        UpdateButtonView(_view.MoveRightJoystick, action, InputsData.JoystickEventIndex);

        action = InputsData.Jump;
        UpdateButtonView(_view.Jump, action, InputsData.KeyboardEventIndex);
        UpdateButtonView(_view.JumpJoystick, action, InputsData.JoystickEventIndex);

    }

    protected override void ResetViewToDefault()
    {
        UpdateAllButtonViews();
    }

    private void OnReturnButtonDown()
    {
        _lastSelectedElement = _view.ReturnButton;
        OnReturnToPreviousMenuButtonDown();
    }

    protected override IViewTransition CreateTransition()
    {
        return new PanelSizeTransition(_view, _view.FadeObjectsContainer, _view.Panel,
            new ITweenableMenuElement[] { _view.ReturnButton, _view.ResetButton,
                _view.MoveLeft, _view.MoveLeftJoystick, _view.MoveRight, _view.MoveRightJoystick, _view.Jump, _view.JumpJoystick,
                _view.MoveLeftLabelResizableControl, _view.MoveRightLabelResizableControl, _view.JumpLabelResizableControl},
            PanelDuration, ElementsDuration);
    }
}
