using UISystem.Core.MenuSystem;
using UISystem.Core.PopupSystem;
using UISystem.Core.Views;
using UISystem.MenuSystem.Models;
using UISystem.MenuSystem.SettingsMenu;
using UISystem.MenuSystem.Views;

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
        IPopupsManager popupsManager)
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
    protected override void UpdateFullView()
    {
        View.MusicSlider.SetValue(Model.MusicVolume);
        View.SfxSlider.SetValue(Model.SfxVolume);
        View.SetLastSelectedElement(View.ResetButton);
    }

    private void OnSaveSettingsButtonDown()
    {
        Model.SaveSettings();
        View.SetLastSelectedElement(View.SaveSettingsButton);
    }

    private void SetupMusicSlider()
    {
        View.MusicSlider.SetValueNoSignal(Model.MusicVolume);
        View.MusicSlider.DragEnded += OnMusicSliderDragEnded;
        View.MusicSlider.DragStarted += OnMusicSliderDragStarted;
    }

    private void OnMusicSliderDragEnded(bool dragEnded)
    {
        if (dragEnded)
            Model.MusicVolume = (float)View.MusicSlider.Value;
    }

    private void OnMusicSliderDragStarted()
    {
        Model.MusicVolume = (float)View.MusicSlider.Value;
        View.SetLastSelectedElement(View.MusicSlider);
    }

    private void SetupSfxSlider()
    {
        View.SfxSlider.SetValueNoSignal(Model.SfxVolume);
        View.SfxSlider.DragEnded += OnSfxSliderDragEnded;
        View.SfxSlider.DragStarted += OnSfxSliderDragStarted;
    }

    private void OnSfxSliderDragEnded(bool dragEnded)
    {
        if (dragEnded)
            Model.SfxVolume = (float)View.SfxSlider.Value;
    }

    private void OnSfxSliderDragStarted()
    {
        Model.SfxVolume = (float)View.SfxSlider.Value;
        View.SetLastSelectedElement(View.SfxSlider);
    }
}
