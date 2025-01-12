using Godot;
using UISystem.Core.Elements.Interfaces;
using UISystem.Core.Views;
using UISystem.Elements.ElementViews;

namespace UISystem.MenuSystem.Views;
public partial class MainMenuView : BaseInteractableWindow
{

    [Export] private ButtonView playButton;
    [Export] private ButtonView optionsButton;
    [Export] private ButtonView quitButton;
    [Export] private Control fadeObjectsContainer;

    public ButtonView PlayButton => playButton;
    public ButtonView OptionsButton => optionsButton;
    public ButtonView QuitButton => quitButton;
    public Control FadeObjectsContainer => fadeObjectsContainer;

    protected override void PopulateFocusableElements()
    {
        _focusableElements = new IFocusableControl[] { PlayButton, OptionsButton, QuitButton };
    }

}
