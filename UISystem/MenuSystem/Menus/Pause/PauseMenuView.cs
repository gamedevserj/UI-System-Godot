using Godot;
using UISystem.Core.Elements;
using UISystem.Core.Transitions;
using UISystem.Elements.ElementViews;
using UISystem.Transitions;

namespace UISystem.MenuSystem.Views;

/// <summary>
/// Pause menu view.
/// </summary>
public partial class PauseMenuView : MenuView
{
    [Export] private ButtonView _resumeGameButton;
    [Export] private ButtonView _optionsButton;
    [Export] private ButtonView _returnToMainMenuButton;
    [Export] private Control _fadeObjectsContainer;

    /// <summary>
    /// Gets resume game button.
    /// </summary>
    public ButtonView ResumeGameButton => _resumeGameButton;

    /// <summary>
    /// Gets options button.
    /// </summary>
    public ButtonView OptionsButton => _optionsButton;

    /// <summary>
    /// Gets return to main menu button.
    /// </summary>
    public ButtonView ReturnToMainMenuButton => _returnToMainMenuButton;

    /// <summary>
    /// Gets fade objects container.
    /// </summary>
    public Control FadeObjectsContainer => _fadeObjectsContainer;

    /// <inheritdoc/>
    protected override IInteractableElement DefaultSelectedElement => ResumeGameButton;

    /// <inheritdoc/>
    protected override IViewTransition CreateTransition()
    {
        return new MainElementDropTransition(this, FadeObjectsContainer, ResumeGameButton, new[] { OptionsButton, ReturnToMainMenuButton });
    }

    /// <inheritdoc/>
    protected override void PopulateFocusableElements()
    {
        FocusableElements = new IInteractableElement[] { ResumeGameButton, OptionsButton, ReturnToMainMenuButton };
    }
}
