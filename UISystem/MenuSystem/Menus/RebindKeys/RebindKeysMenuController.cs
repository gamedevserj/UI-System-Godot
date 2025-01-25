﻿using Godot;
using UISystem.Constants;
using UISystem.Core.Constants;
using UISystem.Core.Elements.Interfaces;
using UISystem.Core.MenuSystem.Interfaces;
using UISystem.Core.PopupSystem.Interfaces;
using UISystem.Elements.ElementViews;
using UISystem.MenuSystem.Constants;
using UISystem.MenuSystem.Models;
using UISystem.MenuSystem.SettingsMenu;
using UISystem.MenuSystem.ViewHandlers;
using UISystem.MenuSystem.Views;

namespace UISystem.MenuSystem.Controllers;
internal class RebindKeysMenuController<TViewHandler, TInputEvent>
    : SettingsMenuController<RebindKeysMenuViewHandler<RebindKeysMenuView>, InputEvent, RebindKeysMenuView, RebindKeysMenuModel>
{

    public override int Type => MenuType.RebindKeys;

    public RebindKeysMenuController(RebindKeysMenuViewHandler<RebindKeysMenuView> viewHandler, RebindKeysMenuModel model, IMenusManager<InputEvent> menusManager, IPopupsManager<InputEvent> popupsManager) : base(viewHandler, model, menusManager, popupsManager)
    { }

    public override void OnAnyButtonDown(InputEvent inputEvent)
    {
        if (_model.IsRebinding)
            _model.RebindKey(inputEvent);
    }
    public override void OnCancelButtonDown()
    {
        if (!_model.IsRebinding)
            base.OnCancelButtonDown();
    }

    private static void UpdateButtonView(RebindableKeyButtonView button, string action, int index)
    {
        var e = InputMap.ActionGetEvents(action)[index];
        button.TextureRect.Texture = (Texture2D)GD.Load(Icons.GetIcon(e));
    }

    private void OnButtonDown(RebindableKeyButtonView button, string action, int index)
    {
        button.TextureRect.Texture = (Texture2D)GD.Load(Icons.EllipsisImage);
        _view.SetLastSelectedElement(button);
        SwitchFocusAvailability(false);

        _model.StartRebinding(action, index, () =>
        {
            SwitchRebindingButtonFocusability(button, true);
            UpdateButtonView(button, action, index);
            SwitchFocusAvailability(true);
        });
    }

    private void SwitchRebindingButtonFocusability(IFocusableControl button, bool allowFocus)
    {
        SwitchFocusAvailability(allowFocus);
        if (allowFocus)
        {
            _view.GetViewport().SetInputAsHandled();
            _view.SetLastSelectedElement(button);
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
        _view.SetLastSelectedElement(_view.ReturnButton);
        OnCancelButtonDown();
    }

}
