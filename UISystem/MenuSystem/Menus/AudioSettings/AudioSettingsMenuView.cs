using Godot;
using UISystem.Core.Elements;
using UISystem.Core.Transitions;
using UISystem.Elements.ElementViews;
using UISystem.MenuSystem.SettingsMenu;
using UISystem.Transitions;
using UISystem.Transitions.Interfaces;

namespace UISystem.MenuSystem.Views;

/// <summary>
/// Audio settings menu view.
/// </summary>
public partial class AudioSettingsMenuView : SettingsMenuView
{
    [Export] private ResizableControlView _resizableControlMusic; // label container
    [Export] private HSliderView _musicSlider;
    [Export] private ResizableControlView _resizableControlSfx; // label container
    [Export] private HSliderView _sfxSlider;
    [Export] private ButtonView _saveSettingsButton;
    [Export] private Control _panel;

    /// <summary>
    /// Gets music slider.
    /// </summary>
    public HSliderView MusicSlider => _musicSlider;

    /// <summary>
    /// Gets SFX slider.
    /// </summary>
    public HSliderView SfxSlider => _sfxSlider;

    /// <summary>
    /// Gets save settings button.
    /// </summary>
    public ButtonView SaveSettingsButton => _saveSettingsButton;

    /// <summary>
    /// Gets menu panel.
    /// </summary>
    public Control Panel => _panel;

    /// <summary>
    /// Gets resizable control for music label.
    /// </summary>
    public ResizableControlView ResizableControlMusic => _resizableControlMusic;

    /// <summary>
    /// Gets resizable control for SFX label.
    /// </summary>
    public ResizableControlView ResizableControlSfx => _resizableControlSfx;

    /// <inheritdoc/>
    protected override IViewTransition CreateTransition()
    {
        return new PanelSizeTransition(
            this,
            FadeObjectsContainer,
            Panel,
            new ITweenableMenuElement[]
            {
                ReturnButton,
                SaveSettingsButton,
                ResetButton,
                MusicSlider,
                SfxSlider,
                ResizableControlMusic,
                ResizableControlSfx,
            });
    }

    /// <inheritdoc/>
    protected override void PopulateFocusableElements()
    {
        FocusableElements = new IInteractableElement[] { MusicSlider, SfxSlider, SaveSettingsButton, ResetButton, ReturnButton };
    }
}
