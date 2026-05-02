using Godot;
using UISystem.Core.Elements;
using UISystem.Core.Transitions;
using UISystem.Elements.ElementViews;
using UISystem.Transitions;

namespace UISystem.MenuSystem.Views;

/// <summary>
/// Options menu view.
/// </summary>
public partial class OptionsMenuView : MenuView
{
    [Export] private ButtonView _interfaceSettingsButton;
    [Export] private ButtonView _audioSettingsButton;
    [Export] private ButtonView _videoSettingsButton;
    [Export] private ButtonView _rebindKeysButton;
    [Export] private ButtonView _returnButton;
    [Export] private Control _fadeObjectsContainer;

    /// <summary>
    /// Gets return button.
    /// </summary>
    public ButtonView ReturnButton => _returnButton;

    /// <summary>
    /// Gets interface settings button.
    /// </summary>
    public ButtonView InterfaceSettingsButton => _interfaceSettingsButton;

    /// <summary>
    /// Gets audio settings button.
    /// </summary>
    public ButtonView AudioSettingsButton => _audioSettingsButton;

    /// <summary>
    /// Gets video settings button.
    /// </summary>
    public ButtonView VideoSettingsButton => _videoSettingsButton;

    /// <summary>
    /// Gets rebind keys button.
    /// </summary>
    public ButtonView RebindKeysButton => _rebindKeysButton;

    /// <inheritdoc/>
    protected override IInteractableElement DefaultSelectedElement => InterfaceSettingsButton;

    /// <inheritdoc/>
    protected override IViewTransition CreateTransition()
    {
        return new MainElementDropTransition(
            this,
            _fadeObjectsContainer,
            InterfaceSettingsButton,
            new[] { ReturnButton, AudioSettingsButton, VideoSettingsButton, RebindKeysButton });
    }

    /// <inheritdoc/>
    protected override void PopulateFocusableElements()
    {
        FocusableElements = new IInteractableElement[]
        {
            ReturnButton,
            AudioSettingsButton,
            VideoSettingsButton,
            RebindKeysButton,
            InterfaceSettingsButton,
        };
    }
}
