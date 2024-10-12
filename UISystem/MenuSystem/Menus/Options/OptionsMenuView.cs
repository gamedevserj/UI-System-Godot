using Godot;
using UISystem.Core.Elements.Interfaces;
using UISystem.Core.Views;
using UISystem.Elements.ElementViews;
using UISystem.Transitions;

namespace UISystem.MenuSystem.Views;
public partial class OptionsMenuView : BaseInteractableWindow
{

    private const float MainElementAnimationDuration = 0.25f;
    private const float SecondaryElementAnimationDuration = 0.5f;

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

    protected override void PopulateFocusableElements()
    {
        _focusableElements = new IFocusableControl[] { ReturnButton, AudioSettingsButton, VideoSettingsButton,
            RebindKeysButton, InterfaceSettingsButton };
    }

    public override void Init()
    {
        base.Init();
        _transition = new MainElementDropTransition(this, fadeObjectsContainer, InterfaceSettingsButton,
            new[] { ReturnButton, AudioSettingsButton, VideoSettingsButton, RebindKeysButton },
            MainElementAnimationDuration,
            SecondaryElementAnimationDuration);
    }

}
