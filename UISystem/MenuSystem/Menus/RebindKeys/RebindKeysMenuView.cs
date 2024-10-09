using Godot;
using UISystem.Common.ElementViews;
using UISystem.Common.Transitions;
using UISystem.Core.Elements.Interfaces;
using UISystem.MenuSystem.SettingsMenu;

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
    [Export] private Control moveLeftLabelResizableControl;
    [Export] private Control moveRightLabelResizableControl;
    [Export] private Control jumpLabelResizableControl;

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
            new Control[] { returnButton.ResizableControl,
                moveLeft.ResizableControl, moveLeftJoystick.ResizableControl,
                moveRight.ResizableControl, moveRightJoystick.ResizableControl,
                jump.ResizableControl, jumpJoystick.ResizableControl,
                moveLeftLabelResizableControl, moveRightLabelResizableControl, jumpLabelResizableControl,
                ResetButton.ResizableControl },
            PanelDuration, ElementsDuration);
    }

}
