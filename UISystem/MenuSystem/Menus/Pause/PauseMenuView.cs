using Godot;
using UISystem.Common.ElementViews;
using UISystem.Common.Transitions;
using UISystem.Core.Elements.Interfaces;
using UISystem.Core.Views;

namespace UISystem.MenuSystem.Views;
public partial class PauseMenuView : BaseInteractableWindow
{

    private const float MainElementAnimationDuration = 0.25f;
    private const float SecondaryElementAnimationDuration = 0.5f;

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
        _transition = new MainElementDropTransition(this, fadeObjectsContainer, ResumeGameButton,
            new[] { OptionsButton, ReturnToMainMenuButton },
            MainElementAnimationDuration, SecondaryElementAnimationDuration);
    }

}
