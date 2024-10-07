using Godot;
using System;
using UISystem.Common.Elements;
using UISystem.Common.Interfaces;
using UISystem.Common.Transitions;
using UISystem.Common.Transitions.Interfaces;

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

    private IViewTransition _transition;

    public RebindableKeyButtonView MoveLeft => moveLeft;
    public RebindableKeyButtonView MoveLeftJoystick => moveLeftJoystick;
    public RebindableKeyButtonView MoveRight => moveRight;
    public RebindableKeyButtonView MoveRightJoystick=> moveRightJoystick;
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

    public override void Hide(Action onHidden, bool instant)
    {
        _transition.Hide(onHidden, instant);
    }

    public override void Show(Action onShown, bool instant)
    {
        _transition.Show(onShown, instant);
    }

}
