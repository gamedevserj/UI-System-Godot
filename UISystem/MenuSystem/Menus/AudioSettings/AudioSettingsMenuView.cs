using Godot;
using System;
using UISystem.Common.Elements;
using UISystem.Common.Interfaces;
using UISystem.Common.Transitions;
using UISystem.Common.Transitions.Interfaces;

namespace UISystem.MenuSystem.Views;
public partial class AudioSettingsMenuView : SettingsMenuView
{

    private const float PanelDuration = 0.5f;
    private const float ElementsDuration = 0.25f;

    [Export] private Control resizableControlMusic;
    [Export] private HSliderView musicSlider;
    [Export] private Control resizableControlSfx;
    [Export] private HSliderView sfxSlider;
    [Export] private ButtonView saveSettingsButton;
    [Export] private ButtonView returnButton;
    [Export] private Control panel;

    private IViewTransition _transition;

    public HSliderView MusicSlider => musicSlider;
    public HSliderView SfxSlider => sfxSlider;
    public ButtonView SaveSettingsButton => saveSettingsButton;
    public ButtonView ReturnButton => returnButton;

    protected override void PopulateFocusableElements()
    {
        _focusableElements = new IFocusableControl[] { MusicSlider, SfxSlider, SaveSettingsButton, ResetButton, ReturnButton };
    }

    public override void Init()
    {
        base.Init();
        _transition = new PanelSizeTransition(this, fadeObjectsContainer, panel,
            new Control[] { ReturnButton.ResizableControl, SaveSettingsButton.ResizableControl, ResetButton.ResizableControl,
            MusicSlider.ResizableControl, SfxSlider.ResizableControl, resizableControlMusic, resizableControlSfx },
            PanelDuration, ElementsDuration);
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
