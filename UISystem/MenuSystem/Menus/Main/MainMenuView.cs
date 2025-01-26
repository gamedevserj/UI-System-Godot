using Godot;
using UISystem.Core.Views;
using UISystem.Elements;
using UISystem.Elements.ElementViews;

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

    protected override void PopulateFocusableElements()
    {
        _focusableElements = new IFocusableControl[] { PlayButton, OptionsButton, QuitButton };
    }

}
