using Godot;
using UISystem.Common.Elements;
using UISystem.Common.Interfaces;

namespace UISystem.MenuSystem.Views;
public partial class RebindKeysMenuView : MenuView
{

    [Export] private RebindableKeyButtonView moveLeft;
    [Export] private RebindableKeyButtonView moveLeftJoystick;
    [Export] private RebindableKeyButtonView moveRight;
    [Export] private RebindableKeyButtonView moveRightJoystick;
    [Export] private RebindableKeyButtonView jump;
    [Export] private RebindableKeyButtonView jumpJoystick;
    [Export] private ButtonView resetToDefaultButton;
    [Export] private ButtonView returnButton;

    public RebindableKeyButtonView MoveLeft => moveLeft;
    public RebindableKeyButtonView MoveLeftJoystick => moveLeftJoystick;
    public RebindableKeyButtonView MoveRight => moveRight;
    public RebindableKeyButtonView MoveRightJoystick=> moveRightJoystick;
    public RebindableKeyButtonView Jump => jump;
    public RebindableKeyButtonView JumpJoystick => jumpJoystick;
    public ButtonView ResetToDefaultButton => resetToDefaultButton;
    public ButtonView ReturnButton => returnButton;

    protected override void PopulateFocusableElements()
    {
        _focusableElements = new IFocusableControl[] 
        { MoveLeft, MoveLeftJoystick, MoveRight, MoveRightJoystick, Jump, JumpJoystick, ResetToDefaultButton, ReturnButton };
    }

}
