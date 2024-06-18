using Godot;
using UISystem.Common.Constants;
using UISystem.Common.Elements;
using UISystem.Common.Interfaces;
using UISystem.Constants;
using UISystem.MenuSystem.Enums;
using UISystem.MenuSystem.Models;
using UISystem.MenuSystem.Views;
using UISystem.PopupSystem;
using UISystem.PopupSystem.Enums;

namespace UISystem.MenuSystem.Controllers;
public class RebindKeysMenuController : MenuControllerFade<RebindKeysMenuView, RebindKeysMenuModel>
{

    private readonly PopupsManager _popupsManager;

    public override MenuType MenuType => MenuType.RebindKeys;

    public RebindKeysMenuController(string prefab, RebindKeysMenuModel model, MenusManager menusManager, SceneTree sceneTree,
        PopupsManager popupsManager) : base(prefab, model, menusManager, sceneTree)
    {
        _popupsManager = popupsManager;
    }

    public override void HandleInputPressedWhenActive(InputEvent key)
    {
        if (key.IsPressed() && _model.IsRebinding)
            _model.RebindKey(key);
        else
            base.HandleInputPressedWhenActive(key);
    }

    protected override void CreateView(Node menuParent)
    {
        base.CreateView(menuParent);
        SetupElements();
        _defaultSelectedElement = _view.ReturnButton;
    }

    private static void UpdateButtonView(RebindableKeyButtonView button, string action, int index)
    {
        var e = InputMap.ActionGetEvents(action)[index];
        string icon = "";
        
        if (e is InputEventKey key)
        {
            icon = Icons.GetIcon(key.PhysicalKeycode);
        }
        else if (e is InputEventMouseButton mouseButton)
        {
            icon = Icons.GetIcon(mouseButton.ButtonIndex);
        }
        if (e is InputEventJoypadButton inputButton)
        {
            icon = Icons.GetIcon(inputButton.ButtonIndex);
        }
        else if (e is InputEventJoypadMotion motion)
        {
            icon = Icons.GetIcon(motion.Axis, motion.AxisValue);
        }

        button.Image.Texture = (Texture2D)GD.Load(icon);
    }

    private void ResetToDefault()
    {
        _lastSelectedElement = _view.ResetToDefaultButton;
        SwitchFocusAvailability(false);
        _popupsManager.ShowPopup(PopupType.YesNo, PopupMessages.ResetToDefault, (result) =>
        {
            _model.ResetToDefault(result);
            UpdateAllButtonViews();
            SwitchFocusAvailability(true);
        });
    }

    private void OnButtonDown(RebindableKeyButtonView button, string action, int index)
    {
        button.Image.Texture = (Texture2D)GD.Load(Icons.EllipsisImage);
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

    private void SetupElements()
    {
        _view.ReturnButton.ButtonDown += OnReturnToPreviousMenuButtonDown;
        _view.ResetToDefaultButton.ButtonDown += ResetToDefault;

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

}
