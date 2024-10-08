using Godot;
using System;
using UISystem.Common.ElementViews;
using UISystem.Common.Transitions;
using UISystem.Common.Transitions.Interfaces;
using UISystem.Core.Common.Interfaces;
using UISystem.Core.MenuSystem.Views;

namespace UISystem.MenuSystem.Views;
public partial class OptionsMenuView : MenuView
{

    private const float MainElementAnimationDuration = 0.25f;
    private const float SecondaryElementAnimationDuration = 0.5f;

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
        _transition = new MainElementDropTransition(this, fadeObjectsContainer, InterfaceSettingsButton,
            new[] { ReturnButton, AudioSettingsButton, VideoSettingsButton, RebindKeysButton },
            MainElementAnimationDuration,
            SecondaryElementAnimationDuration);
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
