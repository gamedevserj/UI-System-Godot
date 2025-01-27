using Godot;
using UISystem.Core.Transitions;
using UISystem.Elements;
using UISystem.Elements.ElementViews;
using UISystem.Transitions;

namespace UISystem.MenuSystem.Views;
public partial class MainMenuView : MenuView
{

    [Export] private ButtonView playButton;
    [Export] private ButtonView optionsButton;
    [Export] private ButtonView quitButton;
    [Export] private Control fadeObjectsContainer;

    public ButtonView PlayButton => playButton;
    public ButtonView OptionsButton => optionsButton;
    public ButtonView QuitButton => quitButton;
    public Control FadeObjectsContainer => fadeObjectsContainer;

    protected override IFocusableControl DefaultSelectedElement => PlayButton;

    protected override IViewTransition CreateTransition()
    {
        return new MainElementDropTransition(this, FadeObjectsContainer, PlayButton, new[] { OptionsButton, QuitButton });
    }

    protected override void PopulateFocusableElements()
    {
        _focusableElements = new IFocusableControl[] { PlayButton, OptionsButton, QuitButton };
    }

}
