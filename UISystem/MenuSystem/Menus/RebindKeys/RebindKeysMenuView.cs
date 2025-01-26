using Godot;
using UISystem.Elements;
using UISystem.Elements.ElementViews;
using UISystem.MenuSystem.SettingsMenu;

namespace UISystem.MenuSystem.Views;
public partial class RebindKeysMenuView : SettingsMenuView
{

    [Export] private RebindableKeyButtonView moveLeft;
    [Export] private RebindableKeyButtonView moveLeftJoystick;
    [Export] private RebindableKeyButtonView moveRight;
    [Export] private RebindableKeyButtonView moveRightJoystick;
    [Export] private RebindableKeyButtonView jump;
    [Export] private RebindableKeyButtonView jumpJoystick;
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
    public Control Panel => panel;
    public ResizableControlView MoveLeftLabelResizableControl => moveLeftLabelResizableControl;
    public ResizableControlView MoveRightLabelResizableControl => moveRightLabelResizableControl;
    public ResizableControlView JumpLabelResizableControl => jumpLabelResizableControl;

    protected override void PopulateFocusableElements()
    {
        _focusableElements = new IFocusableControl[]
        { MoveLeft, MoveLeftJoystick, MoveRight, MoveRightJoystick, Jump, JumpJoystick, ResetButton, ReturnButton };
    }

}
