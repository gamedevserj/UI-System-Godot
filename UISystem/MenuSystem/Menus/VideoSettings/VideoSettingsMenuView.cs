using Godot;
using UISystem.Core.Elements.Interfaces;
using UISystem.Elements.ElementViews;
using UISystem.MenuSystem.SettingsMenu;

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

    protected override void PopulateFocusableElements()
    {
        _focusableElements = new IFocusableControl[]
        { WindowModeDropdown, ResolutionDropdown, SaveSettingsButton, ResetButton, ReturnButton };
    }

}
