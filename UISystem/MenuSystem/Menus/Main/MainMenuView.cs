using Godot;
using System;
using UISystem.Common.Elements;
using UISystem.Common.Interfaces;
using UISystem.Common.Transitions;
using UISystem.Common.Transitions.Interfaces;

namespace UISystem.MenuSystem.Views;
public partial class MainMenuView : MenuView
{

    private const float AnimationDuration = 0.5f;

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
        _transition = new MainElementDropTransition(this, fadeObjectsContainer, playButton, new[] { optionsButton, quitButton },
            AnimationDuration);
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
