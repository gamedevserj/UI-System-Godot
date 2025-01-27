using Godot;
using UISystem.Core.Transitions;
using UISystem.Elements;
using UISystem.Elements.ElementViews;
using UISystem.Transitions;

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

    protected override IViewTransition CreateTransition()
    {
        return new MainElementDropTransition(this, FadeObjectsContainer, InterfaceSettingsButton,
        new[] { ReturnButton, AudioSettingsButton, VideoSettingsButton, RebindKeysButton });
    }
    protected override void PopulateFocusableElements()
    {
        _focusableElements = new IFocusableControl[] { ReturnButton, AudioSettingsButton, VideoSettingsButton,
            RebindKeysButton, InterfaceSettingsButton };
    }

}
