using Godot;
using UISystem.Core.Transitions;
using UISystem.Elements;
using UISystem.Elements.ElementViews;
using UISystem.MenuSystem.SettingsMenu;
using UISystem.Transitions;
using UISystem.Transitions.Interfaces;

namespace UISystem.MenuSystem.Views;
public partial class InterfaceSettingsMenuView : SettingsMenuView
{

    [Export] private DropdownView controllerIconsDropdown;
    [Export] private ButtonView saveSettingsButton;
    [Export] private Control panel;

    public ButtonView SaveSettingsButton => saveSettingsButton;
    public DropdownView ControllerIconsDropdown => controllerIconsDropdown;
    public Control Panel => panel;
    protected override IViewTransition CreateTransition()
    {
        return new PanelSizeTransition(this, FadeObjectsContainer, Panel,
        new ITweenableMenuElement[] { ReturnButton, ControllerIconsDropdown, SaveSettingsButton, ResetButton });
    }
    protected override void PopulateFocusableElements()
    {
        _focusableElements = new IFocusableControl[] { ReturnButton, ControllerIconsDropdown, SaveSettingsButton, ResetButton };
    }
}

