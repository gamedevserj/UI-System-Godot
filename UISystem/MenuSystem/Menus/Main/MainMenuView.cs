using Godot;
using UISystem.Core.Elements;
using UISystem.Core.Transitions;
using UISystem.Elements.ElementViews;
using UISystem.Transitions;

namespace UISystem.MenuSystem.Views;

/// <summary>
/// Main menu view.
/// </summary>
public partial class MainMenuView : MenuView
{
    [Export] private ButtonView _playButton;
    [Export] private ButtonView _optionsButton;
    [Export] private ButtonView _quitButton;
    [Export] private Control _fadeObjectsContainer;

    /// <summary>
    /// Gets play button.
    /// </summary>
    public ButtonView PlayButton => _playButton;

    /// <summary>
    /// Gets options button.
    /// </summary>
    public ButtonView OptionsButton => _optionsButton;

    /// <summary>
    /// Gets quit button.
    /// </summary>
    public ButtonView QuitButton => _quitButton;

    /// <summary>
    /// Gets fade objects container.
    /// </summary>
    public Control FadeObjectsContainer => _fadeObjectsContainer;

    /// <inheritdoc/>
    protected override IInteractableElement DefaultSelectedElement => PlayButton;

    /// <inheritdoc/>
    protected override IViewTransition CreateTransition()
    {
        return new MainElementDropTransition(this, FadeObjectsContainer, PlayButton, new[] { OptionsButton, QuitButton });
    }

    /// <inheritdoc/>
    protected override void PopulateFocusableElements()
    {
        FocusableElements = new IInteractableElement[] { PlayButton, OptionsButton, QuitButton };
    }
}
