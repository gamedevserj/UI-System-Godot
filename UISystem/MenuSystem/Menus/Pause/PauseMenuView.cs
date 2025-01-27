using Godot;
using UISystem.Core.Transitions;
using UISystem.Core.Views;
using UISystem.Elements;
using UISystem.Elements.ElementViews;
using UISystem.Transitions;

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

    protected override IViewTransition CreateTransition()
    {
        return new MainElementDropTransition(this, FadeObjectsContainer, ResumeGameButton, new[] { OptionsButton, ReturnToMainMenuButton });
    }

    protected override void PopulateFocusableElements()
    {
        _focusableElements = new IFocusableControl[] { ResumeGameButton, OptionsButton, ReturnToMainMenuButton };
    }

}
