using Godot;
using UISystem.Common.Elements;
using UISystem.Common.Interfaces;

namespace UISystem.MenuSystem.Views;
public partial class VideoSettingsMenuView : SettingsMenuView
{

    [Export] private DropdownView windowModeDropdown;
    [Export] private DropdownView resolutionDropdown;
    [Export] private ButtonView saveSettingsButton;
    [Export] private ButtonView returnButton;

    public DropdownView WindowModeDropdown => windowModeDropdown;
    public DropdownView ResolutionDropdown => resolutionDropdown;
    public ButtonView SaveSettingsButton => saveSettingsButton;
    public ButtonView ReturnButton => returnButton;

    protected override void PopulateFocusableElements()
    {
        _focusableElements = new IFocusableControl[] 
        { WindowModeDropdown, ResolutionDropdown, SaveSettingsButton, ResetButton, ReturnButton };
    }

}
