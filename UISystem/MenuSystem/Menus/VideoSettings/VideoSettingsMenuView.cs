using Godot;
using UISystem.Core.Transitions;
using UISystem.Elements;
using UISystem.Elements.ElementViews;
using UISystem.MenuSystem.SettingsMenu;
using UISystem.Transitions;
using UISystem.Transitions.Interfaces;

namespace UISystem.MenuSystem.Views;
public partial class VideoSettingsMenuView : SettingsMenuView
{

    [Export] private DropdownView windowModeDropdown;
    [Export] private DropdownView resolutionDropdown;
    [Export] private ButtonView saveSettingsButton;
    [Export] private Control panel;

    public DropdownView WindowModeDropdown => windowModeDropdown;
    public DropdownView ResolutionDropdown => resolutionDropdown;
    public ButtonView SaveSettingsButton => saveSettingsButton;
    public Control Panel => panel;
    protected override IViewTransition CreateTransition()
    {
        return new PanelSizeTransition(this, FadeObjectsContainer, Panel,
        new ITweenableMenuElement[] { ReturnButton, ResolutionDropdown, WindowModeDropdown, SaveSettingsButton, ResetButton });
    }

    protected override void PopulateFocusableElements()
    {
        _focusableElements = new IFocusableControl[]
        { WindowModeDropdown, ResolutionDropdown, SaveSettingsButton, ResetButton, ReturnButton };
    }

}
