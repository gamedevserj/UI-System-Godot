using Godot;
using UISystem.Core.Elements.Interfaces;
using UISystem.Elements.ElementViews;
using UISystem.MenuSystem.SettingsMenu;

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

    protected override void PopulateFocusableElements()
    {
        _focusableElements = new IFocusableControl[] { MusicSlider, SfxSlider, SaveSettingsButton, ResetButton, ReturnButton };
    }

}
