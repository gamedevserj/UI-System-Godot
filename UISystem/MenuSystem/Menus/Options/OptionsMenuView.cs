using Godot;
using UISystem.Core.Elements.Interfaces;
using UISystem.Elements.ElementViews;

namespace UISystem.MenuSystem.Views;
public partial class OptionsMenuView : MenuView
{

    [Export] private ButtonView interfaceSettingsButton;
    [Export] private ButtonView audioSettingsButton;
    [Export] private ButtonView videoSettingsButton;
    [Export] private ButtonView rebindKeysButton;
    [Export] private ButtonView returnButton;
    [Export] private Control fadeObjectsContainer;

    public ButtonView ReturnButton => returnButton;
    public ButtonView InterfaceSettingsButton => interfaceSettingsButton;
    public ButtonView AudioSettingsButton => audioSettingsButton;
    public ButtonView VideoSettingsButton => videoSettingsButton;
    public ButtonView RebindKeysButton => rebindKeysButton;
    public Control FadeObjectsContainer => fadeObjectsContainer;

    protected override IFocusableControl DefaultSelectedElement => InterfaceSettingsButton;

    protected override void PopulateFocusableElements()
    {
        _focusableElements = new IFocusableControl[] { ReturnButton, AudioSettingsButton, VideoSettingsButton,
            RebindKeysButton, InterfaceSettingsButton };
    }

}
