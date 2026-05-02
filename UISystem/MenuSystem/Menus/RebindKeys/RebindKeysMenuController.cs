using Godot;
using UISystem.Constants;
using UISystem.Core.Constants;
using UISystem.Core.Elements;
using UISystem.Core.MenuSystem;
using UISystem.Core.PopupSystem;
using UISystem.Core.Views;
using UISystem.Elements.ElementViews;
using UISystem.MenuSystem.Models;
using UISystem.MenuSystem.SettingsMenu;
using UISystem.MenuSystem.Views;
using UISystem.PhysicalInput;

namespace UISystem.MenuSystem.Controllers;

/// <summary>
/// Rebind keys menu controller.
/// </summary>
internal class RebindKeysMenuController
    : SettingsMenuController<IViewCreator<RebindKeysMenuView>, RebindKeysMenuView, RebindKeysMenuModel>, IRebindInputReceiver
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RebindKeysMenuController"/> class.
    /// </summary>
    /// <param name="viewCreator">View creator.</param>
    /// <param name="menusManager">Menus manager.</param>
    /// <param name="model">Rebind keys menu model.</param>
    /// <param name="popupsManager">Popups manager.</param>
    public RebindKeysMenuController(
        IViewCreator<RebindKeysMenuView> viewCreator,
        IMenusManager menusManager,
        RebindKeysMenuModel model,
        IPopupsManager popupsManager)
        : base(viewCreator, menusManager, model, popupsManager)
    {
    }

    /// <summary>
    /// If rebinding is in progress - rebinds key, otherwise does nothing.
    /// </summary>
    /// <param name="inputEvent">Button/key that is being pressed.</param>
    public void OnAnyButtonDown(InputEvent inputEvent)
    {
        if (Model.IsRebinding)
            Model.RebindKey(inputEvent);
    }

    /// <inheritdoc/>
    public override void OnReturnButtonDown()
    {
        if (!Model.IsRebinding)
            base.OnReturnButtonDown();
    }

    /// <inheritdoc/>
    protected override void UpdateFullView()
    {
        UpdateAllButtonViews();
    }

    /// <inheritdoc/>
    protected override void SetupElements()
    {
        base.SetupElements();

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

    private void UpdateButtonView(RebindableKeyButtonView button, string action, int index)
    {
        var actionEvent = InputMap.ActionGetEvents(action)[index];
        button.TextureRect.Texture = (Texture2D)GD.Load(Icons.GetIcon(actionEvent, Model.IconsType));
    }

    private void OnButtonDown(RebindableKeyButtonView button, string action, int index)
    {
        button.TextureRect.Texture = (Texture2D)GD.Load(Icons.EllipsisImage);
        View.SetLastSelectedElement(button);
        SwitchInteractability(false);

        Model.StartRebinding(action, index, () =>
        {
            SwitchRebindingButtonFocusability(button, true);
            UpdateButtonView(button, action, index);
            SwitchInteractability(true);
        });
    }

    private void SwitchRebindingButtonFocusability(IInteractableElement button, bool allowFocus)
    {
        SwitchInteractability(allowFocus);
        if (allowFocus)
        {
            View.GetViewport().SetInputAsHandled();
            View.SetLastSelectedElement(button);
        }
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
}
