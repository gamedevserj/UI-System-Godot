using Godot;
using UISystem.Core.Transitions;
using UISystem.Elements;
using UISystem.Elements.ElementViews;
using UISystem.MenuSystem.SettingsMenu;
using UISystem.Transitions;
using UISystem.Transitions.Interfaces;

namespace UISystem.MenuSystem.Views;
public partial class AudioSettingsMenuView : SettingsMenuView
{

    [Export] private ResizableControlView resizableControlMusic; // label container
    [Export] private HSliderView musicSlider;
    [Export] private ResizableControlView resizableControlSfx; // label container
    [Export] private HSliderView sfxSlider;
    [Export] private ButtonView saveSettingsButton;
    [Export] private Control panel;

    public HSliderView MusicSlider => musicSlider;
    public HSliderView SfxSlider => sfxSlider;
    public ButtonView SaveSettingsButton => saveSettingsButton;
    public Control Panel => panel;
    public ResizableControlView ResizableControlMusic => resizableControlMusic;
    public ResizableControlView ResizableControlSfx => resizableControlSfx;
    protected override IViewTransition CreateTransition()
    {
        return new PanelSizeTransition(this, FadeObjectsContainer, Panel,
        new ITweenableMenuElement[] { ReturnButton, SaveSettingsButton, ResetButton,
            MusicSlider, SfxSlider, ResizableControlMusic, ResizableControlSfx });
    }
    protected override void PopulateFocusableElements()
    {
        _focusableElements = new IFocusableControl[] { MusicSlider, SfxSlider, SaveSettingsButton, ResetButton, ReturnButton };
    }

}
