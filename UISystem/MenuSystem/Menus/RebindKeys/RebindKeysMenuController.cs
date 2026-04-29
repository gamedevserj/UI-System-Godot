using Godot;
using UISystem.Constants;
using UISystem.Core.Constants;
using UISystem.Core.MenuSystem;
using UISystem.Core.PopupSystem;
using UISystem.Core.Views;
using UISystem.Elements;
using UISystem.Elements.ElementViews;
using UISystem.MenuSystem.Models;
using UISystem.MenuSystem.SettingsMenu;
using UISystem.MenuSystem.Views;
using UISystem.PhysicalInput;
using UISystem.PopupSystem;

namespace UISystem.MenuSystem.Controllers;
internal class RebindKeysMenuController 
    : SettingsMenuController<IViewCreator<RebindKeysMenuView>, RebindKeysMenuView, RebindKeysMenuModel>, IRebindInputReceiver
{

    public RebindKeysMenuController(
        IViewCreator<RebindKeysMenuView> viewCreator, 
        IMenusManager menusManager, 
        RebindKeysMenuModel model, 
        IPopupsManager<PopupResult> popupsManager) 
        : base(viewCreator, menusManager, model, popupsManager)
    { }

    public void OnAnyButtonDown(InputEvent inputEvent)
    {
        if (_model.IsRebinding)
            _model.RebindKey(inputEvent);
    }

    public override void OnReturnButtonDown()
    {
        if (!_model.IsRebinding)
            base.OnReturnButtonDown();
    }

    private void UpdateButtonView(RebindableKeyButtonView button, string action, int index)
    {
        var actionEvent = InputMap.ActionGetEvents(action)[index];
        button.TextureRect.Texture = (Texture2D)GD.Load(Icons.GetIcon(actionEvent, _model.IconsType));
    }

    private void OnButtonDown(RebindableKeyButtonView button, string action, int index)
    {
        button.TextureRect.Texture = (Texture2D)GD.Load(Icons.EllipsisImage);
        View.SetLastSelectedElement(button);
        SwitchInteractability(false);

        _model.StartRebinding(action, index, () =>
        {
            SwitchRebindingButtonFocusability(button, true);
            UpdateButtonView(button, action, index);
            SwitchInteractability(true);
        });
    }

    private void SwitchRebindingButtonFocusability(IFocusableUiElement button, bool allowFocus)
    {
        SwitchInteractability(allowFocus);
        if (allowFocus)
        {
            View.GetViewport().SetInputAsHandled();
            View.SetLastSelectedElement(button);
        }
    }

    protected override void SetupElements()
    {
        View.ReturnButton.ButtonDown += OnReturnButtonDown;
        View.ResetButton.ButtonDown += OnResetToDefaultButtonDown;

        View.MoveLeft.ButtonDown += () =>
        OnButtonDown(View.MoveLeft, InputsData.MoveLeft, InputsData.KeyboardEventIndex);
        View.MoveLeftJoystick.ButtonDown += () =>
        OnButtonDown(View.MoveLeftJoystick, InputsData.MoveLeft, InputsData.JoystickEventIndex);

        View.MoveRight.ButtonDown += () =>
        OnButtonDown(View.MoveRight, InputsData.MoveRight, InputsData.KeyboardEventIndex);
        View.MoveRightJoystick.ButtonDown += () =>
        OnButtonDown(View.MoveRightJoystick, InputsData.MoveRight, InputsData.JoystickEventIndex);

        View.Jump.ButtonDown += () =>
        OnButtonDown(View.Jump, InputsData.Jump, InputsData.KeyboardEventIndex);
        View.JumpJoystick.ButtonDown += () =>
        OnButtonDown(View.JumpJoystick, InputsData.Jump, InputsData.JoystickEventIndex);

        UpdateAllButtonViews();
    }

    private void UpdateAllButtonViews()
    {
        string action = InputsData.MoveLeft;
        UpdateButtonView(View.MoveLeft, action, InputsData.KeyboardEventIndex);
        UpdateButtonView(View.MoveLeftJoystick, action, InputsData.JoystickEventIndex);

        action = InputsData.MoveRight;
        UpdateButtonView(View.MoveRight, action, InputsData.KeyboardEventIndex);
        UpdateButtonView(View.MoveRightJoystick, action, InputsData.JoystickEventIndex);

        action = InputsData.Jump;
        UpdateButtonView(View.Jump, action, InputsData.KeyboardEventIndex);
        UpdateButtonView(View.JumpJoystick, action, InputsData.JoystickEventIndex);

    }

    protected override void ResetViewToDefault()
    {
        UpdateAllButtonViews();
    }
}
