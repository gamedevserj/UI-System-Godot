using Godot;
using UISystem.Common.ElementViews;
using UISystem.Common.Interfaces;
using UISystem.Common.Transitions;
using UISystem.Core.Elements.Interfaces;
using UISystem.MenuSystem.SettingsMenu;
using UISystem.UISystem.Common.ElementViews;

namespace UISystem.MenuSystem.Views;
public partial class RebindKeysMenuView : SettingsMenuView
{

    private const float PanelDuration = 0.5f;
    private const float ElementsDuration = 0.25f;

    [Export] private RebindableKeyButtonView moveLeft;
    [Export] private RebindableKeyButtonView moveLeftJoystick;
    [Export] private RebindableKeyButtonView moveRight;
    [Export] private RebindableKeyButtonView moveRightJoystick;
    [Export] private RebindableKeyButtonView jump;
    [Export] private RebindableKeyButtonView jumpJoystick;
    [Export] private ButtonView returnButton;
    [Export] private Control panel;
    [Export] private ResizableControlView moveLeftLabelResizableControl;
    [Export] private ResizableControlView moveRightLabelResizableControl;
    [Export] private ResizableControlView jumpLabelResizableControl;

    public RebindableKeyButtonView MoveLeft => moveLeft;
    public RebindableKeyButtonView MoveLeftJoystick => moveLeftJoystick;
    public RebindableKeyButtonView MoveRight => moveRight;
    public RebindableKeyButtonView MoveRightJoystick => moveRightJoystick;
    public RebindableKeyButtonView Jump => jump;
    public RebindableKeyButtonView JumpJoystick => jumpJoystick;
    public ButtonView ReturnButton => returnButton;

    protected override void PopulateFocusableElements()
    {
        _focusableElements = new IFocusableControl[]
        { MoveLeft, MoveLeftJoystick, MoveRight, MoveRightJoystick, Jump, JumpJoystick, ResetButton, ReturnButton };
    }

    public override void Init()
    {
        base.Init();
        _transition = new PanelSizeTransition(this, fadeObjectsContainer, panel,
            new ITweenableMenuElement[] { returnButton, ResetButton,
                moveLeft, moveLeftJoystick, moveRight, moveRightJoystick, jump, jumpJoystick,
                moveLeftLabelResizableControl, moveRightLabelResizableControl, jumpLabelResizableControl},
            PanelDuration, ElementsDuration);
    }

}
