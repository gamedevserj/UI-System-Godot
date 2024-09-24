using Godot;
using System;
using UISystem.Common.Elements;
using UISystem.Common.Interfaces;
using UISystem.MenuSystem.Interfaces;
using UISystem.MenuSystem.ViewTransitions;

namespace UISystem.MenuSystem.Views;
public partial class OptionsMenuView : MenuView
{

    private const float AnimationDuration = 0.5f;

    [Export] private ButtonView interfaceSettingsButton;
    [Export] private ButtonView audioSettingsButton;
    [Export] private ButtonView videoSettingsButton;
    [Export] private ButtonView rebindKeysButton;
    [Export] private ButtonView returnButton;
    [Export] private Control fadeObjectsContainer;

    private IViewTransition _transition;

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
        _transition = new ElementsDropTransition(this, fadeObjectsContainer, InterfaceSettingsButton,
            new[] { ReturnButton, AudioSettingsButton, VideoSettingsButton, RebindKeysButton }, AnimationDuration);
    }

    public override void Hide(Action onHidden, bool instant)
    {
        _transition.Hide(onHidden, instant);
    }

    public override void Show(Action onShown, bool instant)
    {
        _transition.Show(onShown, instant);
    }

}
