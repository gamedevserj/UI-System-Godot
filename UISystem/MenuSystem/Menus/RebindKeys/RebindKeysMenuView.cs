using Godot;
using UISystem.Core.Elements;
using UISystem.Core.Transitions;
using UISystem.Elements.ElementViews;
using UISystem.MenuSystem.SettingsMenu;
using UISystem.Transitions;
using UISystem.Transitions.Interfaces;

namespace UISystem.MenuSystem.Views;

/// <summary>
/// Rebind keys menu view.
/// </summary>
public partial class RebindKeysMenuView : SettingsMenuView
{
    [Export] private RebindableKeyButtonView _moveLeft;
    [Export] private RebindableKeyButtonView _moveLeftJoystick;
    [Export] private RebindableKeyButtonView _moveRight;
    [Export] private RebindableKeyButtonView _moveRightJoystick;
    [Export] private RebindableKeyButtonView _jump;
    [Export] private RebindableKeyButtonView _jumpJoystick;
    [Export] private Control _panel;
    [Export] private ResizableControlView _moveLeftLabelResizableControl;
    [Export] private ResizableControlView _moveRightLabelResizableControl;
    [Export] private ResizableControlView _jumpLabelResizableControl;

    /// <summary>
    /// Gets move left button view.
    /// </summary>
    public RebindableKeyButtonView MoveLeft => _moveLeft;

    /// <summary>
    /// Gets joystick move left button.
    /// </summary>
    public RebindableKeyButtonView MoveLeftJoystick => _moveLeftJoystick;

    /// <summary>
    /// Gets move right button.
    /// </summary>
    public RebindableKeyButtonView MoveRight => _moveRight;

    /// <summary>
    /// Gets joystick move right button.
    /// </summary>
    public RebindableKeyButtonView MoveRightJoystick => _moveRightJoystick;

    /// <summary>
    /// Gets jump button.
    /// </summary>
    public RebindableKeyButtonView Jump => _jump;

    /// <summary>
    /// Gets joystick jump button.
    /// </summary>
    public RebindableKeyButtonView JumpJoystick => _jumpJoystick;

    /// <inheritdoc/>
    protected override IViewTransition CreateTransition()
    {
        return new PanelSizeTransition(
            this,
            FadeObjectsContainer,
            _panel,
            new ITweenableMenuElement[]
                {
                    ReturnButton, ResetButton, MoveLeft, MoveLeftJoystick, MoveRight, MoveRightJoystick,
                    Jump, JumpJoystick,
                    _moveLeftLabelResizableControl, _moveRightLabelResizableControl, _jumpLabelResizableControl,
                });
    }

    /// <inheritdoc/>
    protected override void PopulateFocusableElements()
    {
        FocusableElements = new IInteractableElement[]
        {
            MoveLeft, MoveLeftJoystick, MoveRight, MoveRightJoystick, Jump, JumpJoystick, ResetButton, ReturnButton,
        };
    }
}
