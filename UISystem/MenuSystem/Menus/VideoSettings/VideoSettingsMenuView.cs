using Godot;
using UISystem.Core.Elements;
using UISystem.Core.Transitions;
using UISystem.Elements.ElementViews;
using UISystem.MenuSystem.SettingsMenu;
using UISystem.Transitions;
using UISystem.Transitions.Interfaces;

namespace UISystem.MenuSystem.Views;

/// <summary>
/// Video settings menu view.
/// </summary>
public partial class VideoSettingsMenuView : SettingsMenuView
{
    [Export] private DropdownView _windowModeDropdown;
    [Export] private DropdownView _resolutionDropdown;
    [Export] private ButtonView _saveSettingsButton;
    [Export] private Control _panel;

    /// <summary>
    /// Gets window mode dropdown.
    /// </summary>
    public DropdownView WindowModeDropdown => _windowModeDropdown;

    /// <summary>
    /// Gets resolution dropdown.
    /// </summary>
    public DropdownView ResolutionDropdown => _resolutionDropdown;

    /// <summary>
    /// Gets save settings button.
    /// </summary>
    public ButtonView SaveSettingsButton => _saveSettingsButton;

    /// <inheritdoc/>
    protected override IViewTransition CreateTransition()
    {
        return new PanelSizeTransition(
            this,
            FadeObjectsContainer,
            _panel,
            new ITweenableMenuElement[] { ReturnButton, ResolutionDropdown, WindowModeDropdown, SaveSettingsButton, ResetButton });
    }

    /// <inheritdoc/>
    protected override void PopulateFocusableElements()
    {
        FocusableElements = new IInteractableElement[]
        {
            WindowModeDropdown, ResolutionDropdown, SaveSettingsButton, ResetButton, ReturnButton,
        };
    }
}
