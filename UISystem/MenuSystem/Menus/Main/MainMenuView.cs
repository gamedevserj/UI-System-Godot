using Godot;
using UISystem.Core.Elements.Interfaces;
using UISystem.Core.Views;
using UISystem.Elements.ElementViews;
using UISystem.Transitions;

namespace UISystem.MenuSystem.Views;
public partial class MainMenuView : BaseInteractableWindow
{

    private const float MainElementAnimationDuration = 0.25f;
    private const float SecondaryElementAnimationDuration = 0.5f;

    [Export] private ButtonView playButton;
    [Export] private ButtonView optionsButton;
    [Export] private ButtonView quitButton;
    [Export] private Control fadeObjectsContainer;

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

}
