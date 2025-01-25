using Godot;
using UISystem.Core.Elements.Interfaces;
using UISystem.Core.Views;
using UISystem.Elements.ElementViews;

namespace UISystem.MenuSystem.Views;
public partial class PauseMenuView : BaseInteractableWindow
{

    [Export] private ButtonView resumeGameButton;
    [Export] private ButtonView optionsButton;
    [Export] private ButtonView returnToMainMenuButton;
    [Export] private Control fadeObjectsContainer;

    public ButtonView ResumeGameButton => resumeGameButton;
    public ButtonView OptionsButton => optionsButton;
    public ButtonView ReturnToMainMenuButton => returnToMainMenuButton;
    public Control FadeObjectsContainer => fadeObjectsContainer;

    public override void FocusElement()
    {
        throw new System.NotImplementedException();
    }

    protected override void PopulateFocusableElements()
    {
        _focusableElements = new IFocusableControl[] { ResumeGameButton, OptionsButton, ReturnToMainMenuButton };
    }

}
