using Godot;
using UISystem.Common.Elements;
using UISystem.Common.Interfaces;

namespace MenuSystem.Views;
public partial class OptionsMenuView : MenuView
{

    [Export] private ButtonView audioSettingsButton;
    [Export] private ButtonView videoSettingsButton;
    [Export] private ButtonView returnButton;

    public ButtonView ReturnButton => returnButton;
    public ButtonView AudioSettingsButton => audioSettingsButton;
    public ButtonView VideoSettingsButton => videoSettingsButton;

    protected override void PopulateFocusableElements()
    {
        _focusableElements = new IFocusableControl[] { ReturnButton, AudioSettingsButton, VideoSettingsButton };
    }

}
