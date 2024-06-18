using Godot;
using UISystem.Common.Elements;
using UISystem.Common.Interfaces;

namespace UISystem.MenuSystem.Views;
public partial class InterfaceSettingsMenuView : MenuView
{

    [Export] private OptionButtonView controllerIconsDropdown;
    [Export] private ButtonView saveSettingsButton;
    [Export] private ButtonView resetToDefaultButton;
    [Export] private ButtonView returnButton;

    public ButtonView SaveSettingsButton => saveSettingsButton;
    public ButtonView ResetToDefaultButton => resetToDefaultButton;
    public ButtonView ReturnButton => returnButton;
    public OptionButtonView ControllerIconsDropdown => controllerIconsDropdown;

    protected override void PopulateFocusableElements()
    {
        _focusableElements = new IFocusableControl[] { ReturnButton, ControllerIconsDropdown, SaveSettingsButton, ResetToDefaultButton };
    }

}

