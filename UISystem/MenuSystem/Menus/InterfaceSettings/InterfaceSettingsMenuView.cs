using Godot;
using UISystem.Core.Elements;
using UISystem.Core.Transitions;
using UISystem.Elements.ElementViews;
using UISystem.MenuSystem.SettingsMenu;
using UISystem.Transitions;
using UISystem.Transitions.Interfaces;

namespace UISystem.MenuSystem.Views;

/// <summary>
/// Interface settings menu model.
/// </summary>
public partial class InterfaceSettingsMenuView : SettingsMenuView
{
    [Export] private DropdownView _controllerIconsDropdown;
    [Export] private ButtonView _saveSettingsButton;
    [Export] private Control _panel;

    /// <summary>
    /// Gets the save settings button.
    /// </summary>
    public ButtonView SaveSettingsButton => _saveSettingsButton;

    /// <summary>
    /// Gets the controller icons dropdown.
    /// </summary>
    public DropdownView ControllerIconsDropdown => _controllerIconsDropdown;

    /// <inheritdoc/>
    protected override IViewTransition CreateTransition()
    {
        return new PanelSizeTransition(
            this,
            FadeObjectsContainer,
            _panel,
            new ITweenableMenuElement[] { ReturnButton, ControllerIconsDropdown, SaveSettingsButton, ResetButton });
    }

    /// <inheritdoc/>
    protected override void PopulateFocusableElements()
    {
        FocusableElements = new IInteractableElement[] { ReturnButton, ControllerIconsDropdown, SaveSettingsButton, ResetButton };
    }
}
