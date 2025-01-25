using Godot;
using UISystem.Core.Elements.Interfaces;
using UISystem.Elements.ElementViews;

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
    public Control FadeObjectsContainer => fadeObjectsContainer;

    protected override IFocusableControl DefaultSelectedElement => ResumeGameButton;

    protected override void PopulateFocusableElements()
    {
        _focusableElements = new IFocusableControl[] { ResumeGameButton, OptionsButton, ReturnToMainMenuButton };
    }

}
