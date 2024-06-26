using Godot;
using UISystem.Common.Elements;
using UISystem.Common.Interfaces;

namespace UISystem.MenuSystem.Views;
public partial class MainMenuView : MenuViewFade
{

    [Export] private ButtonView playButton;
    [Export] private ButtonView optionsButton;
    [Export] private ButtonView quitButton;

    public ButtonView PlayButton => playButton;
    public ButtonView OptionsButton => optionsButton;
    public ButtonView QuitButton => quitButton;


    protected override void PopulateFocusableElements()
    {
        _focusableElements = new IFocusableControl[] { PlayButton, OptionsButton, QuitButton };
    }

}
