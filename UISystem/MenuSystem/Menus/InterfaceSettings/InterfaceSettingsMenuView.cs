using Godot;
using UISystem.Elements;
using UISystem.Elements.ElementViews;
using UISystem.MenuSystem.SettingsMenu;

namespace UISystem.MenuSystem.Views;
public partial class InterfaceSettingsMenuView : SettingsMenuView
{

    [Export] private DropdownView controllerIconsDropdown;
    [Export] private ButtonView saveSettingsButton;
    [Export] private Control panel;

    public ButtonView SaveSettingsButton => saveSettingsButton;
    public DropdownView ControllerIconsDropdown => controllerIconsDropdown;
    public Control Panel => panel;

    protected override void PopulateFocusableElements()
    {
        _focusableElements = new IFocusableControl[] { ReturnButton, ControllerIconsDropdown, SaveSettingsButton, ResetButton };
    }

}

