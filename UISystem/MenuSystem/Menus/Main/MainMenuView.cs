using Godot;
using System;
using UISystem.Common.Elements;
using UISystem.Common.Helpers;
using UISystem.Common.Interfaces;

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


    protected override void PopulateFocusableElements()
    {
        _focusableElements = new IFocusableControl[] { PlayButton, OptionsButton, QuitButton };
    }

    public override void Init()
    {
        base.Init();
        Fader.Init(fadeObjectsContainer);
    }

    public override void Hide(Action onHidden, bool instant)
    {
        Fader.Hide(GetTree(), fadeObjectsContainer, onHidden, instant);
    }

    public override void Show(Action onShown, bool instant)
    {
        Fader.Show(GetTree(), fadeObjectsContainer, onShown, instant);
    }

}
