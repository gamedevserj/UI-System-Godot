using Godot;
using System;
using UISystem.Common.Transitions;
using UISystem.Common.Transitions.Interfaces;
using UISystem.Core.Common.ElementViews;
using UISystem.Core.Common.Interfaces;
using UISystem.Core.MenuSystem.Views;

namespace UISystem.MenuSystem.Views;
public partial class MainMenuView : MenuView
{

    private const float MainElementAnimationDuration = 0.25f;
    private const float SecondaryElementAnimationDuration = 0.5f;

    [Export] private ButtonView playButton;
    [Export] private ButtonView optionsButton;
    [Export] private ButtonView quitButton;
    [Export] private Control fadeObjectsContainer;

    private IViewTransition _transition;

    public ButtonView PlayButton => playButton;
    public ButtonView OptionsButton => optionsButton;
    public ButtonView QuitButton => quitButton;

    protected override void PopulateFocusableElements()
    {
        _focusableElements = new IFocusableControl[] { PlayButton, OptionsButton, QuitButton };
    }

    public override void Init()
    {
        base.Init();
        _transition = new MainElementDropTransition(this, fadeObjectsContainer, playButton,
            new[] { optionsButton, quitButton },
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
