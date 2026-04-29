using UISystem.Core.MenuSystem;
using UISystem.Core.PopupSystem;
using UISystem.Core.Views;
using UISystem.MenuSystem.Models;
using UISystem.MenuSystem.SettingsMenu;
using UISystem.MenuSystem.Views;
using UISystem.PopupSystem;

namespace UISystem.MenuSystem.Controllers;

/// <summary>
/// Audio settings menu controller.
/// </summary>
internal class AudioSettingsMenuController : SettingsMenuController<IViewCreator<AudioSettingsMenuView>, AudioSettingsMenuView, AudioSettingsMenuModel>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AudioSettingsMenuController"/> class.
    /// </summary>
    /// <param name="viewCreator">View creator.</param>
    /// <param name="menusManager">Menus manager.</param>
    /// <param name="model">Audio settings menu model.</param>
    /// <param name="popupsManager">Popups manager.</param>
    public AudioSettingsMenuController(
        IViewCreator<AudioSettingsMenuView> viewCreator,
        IMenusManager menusManager,
        AudioSettingsMenuModel model,
        IPopupsManager<PopupResult> popupsManager)
        : base(viewCreator, menusManager, model, popupsManager)
    {
    }

    /// <inheritdoc/>
    protected override void SetupElements()
    {
        base.SetupElements();
        SetupMusicSlider();
        SetupSfxSlider();
        View.SaveSettingsButton.ButtonDown += OnSaveSettingsButtonDown;
    }

    /// <inheritdoc/>
    protected override void ResetViewToDefault()
    {
        View.MusicSlider.SetValue(_model.MusicVolume);
        View.SfxSlider.SetValue(_model.SfxVolume);
        View.SetLastSelectedElement(View.ResetButton);
    }

    private void OnSaveSettingsButtonDown()
    {
        _model.SaveSettings();
        View.SetLastSelectedElement(View.SaveSettingsButton);
    }

    private void SetupMusicSlider()
    {
        View.MusicSlider.SetValueNoSignal(_model.MusicVolume);
        View.MusicSlider.DragEnded += OnMusicSliderDragEnded;
        View.MusicSlider.DragStarted += OnMusicSliderDragStarted;
    }

    private void OnMusicSliderDragEnded(bool dragEnded)
    {
        if (dragEnded)
            _model.MusicVolume = (float)View.MusicSlider.Value;
    }

    private void OnMusicSliderDragStarted()
    {
        _model.MusicVolume = (float)View.MusicSlider.Value;
        View.SetLastSelectedElement(View.MusicSlider);
    }

    private void SetupSfxSlider()
    {
        View.SfxSlider.SetValueNoSignal(_model.SfxVolume);
        View.SfxSlider.DragEnded += OnSfxSliderDragEnded;
        View.SfxSlider.DragStarted += OnSfxSliderDragStarted;
    }

    private void OnSfxSliderDragEnded(bool dragEnded)
    {
        if (dragEnded)
            _model.SfxVolume = (float)View.SfxSlider.Value;
    }

    private void OnSfxSliderDragStarted()
    {
        _model.SfxVolume = (float)View.SfxSlider.Value;
        View.SetLastSelectedElement(View.SfxSlider);
    }
}
