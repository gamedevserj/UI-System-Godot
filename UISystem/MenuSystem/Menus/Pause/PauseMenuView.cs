using Godot;
using System;
using UISystem.Common.ElementViews;
using UISystem.Common.Transitions;
using UISystem.Common.Transitions.Interfaces;
using UISystem.Core.Common.Interfaces;
using UISystem.Core.MenuSystem.Views;

namespace UISystem.MenuSystem.Views;
public partial class PauseMenuView : MenuView
{

    private const float MainElementAnimationDuration = 0.25f;
    private const float SecondaryElementAnimationDuration = 0.5f;

    [Export] private ButtonView resumeGameButton;
    [Export] private ButtonView optionsButton;
    [Export] private ButtonView returnToMainMenuButton;
    [Export] private Control fadeObjectsContainer;

    private IViewTransition _transition;

    public ButtonView ResumeGameButton => resumeGameButton;
    public ButtonView OptionsButton => optionsButton;
    public ButtonView ReturnToMainMenuButton => returnToMainMenuButton;

    protected override void PopulateFocusableElements()
    {
        _focusableElements = new IFocusableControl[] { ResumeGameButton, OptionsButton, ReturnToMainMenuButton };
    }

    public override void Init()
    {
        base.Init();
        _transition = new MainElementDropTransition(this, fadeObjectsContainer, ResumeGameButton,
            new[] { OptionsButton, ReturnToMainMenuButton },
            MainElementAnimationDuration, SecondaryElementAnimationDuration);
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
