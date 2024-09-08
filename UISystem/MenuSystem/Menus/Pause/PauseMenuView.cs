using Godot;
using System;
using UISystem.Common.Elements;
using UISystem.Common.Helpers;
using UISystem.Common.Interfaces;

namespace UISystem.MenuSystem.Views;
public partial class PauseMenuView : MenuView
{

    [Export] private ButtonView resumeGameButton;
    [Export] private ButtonView optionsButton;
    [Export] private ButtonView returnToMainMenuButton;
    [Export] private Control fadeObjectsContainer;

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
