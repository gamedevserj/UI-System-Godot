using UISystem.Core.MenuSystem;
using UISystem.Core.MenuSystem.Controllers;
using UISystem.Core.MenuSystem.Enums;
using UISystem.Core.PopupSystem;
using UISystem.MenuSystem.Models;
using UISystem.MenuSystem.Views;

namespace UISystem.MenuSystem.Controllers;
public class AudioSettingsMenuController : SettingsMenuController<AudioSettingsMenuView, AudioSettingsMenuModel>
{

    public override MenuType MenuType => MenuType.AudioSettings;

    public AudioSettingsMenuController(string prefab, AudioSettingsMenuModel model, MenusManager menusManager,
        PopupsManager popupsManager) 
        : base(prefab, model, menusManager, popupsManager)
    { 
        
    }

    protected override void SetupElements()
    {
        SetupMusicSlider();
        SetupSfxSlider();
        _view.SaveSettingsButton.ButtonDown += OnSaveSettingsButtonDown;
        _view.ResetButton.ButtonDown += OnResetToDefaultButtonDown;
        _view.ReturnButton.ButtonDown += OnReturnButtonDown;
        DefaultSelectedElement = _view.MusicSlider;
    }

    private void OnReturnButtonDown()
    {
        OnReturnToPreviousMenuButtonDown();
    }

    private void OnSaveSettingsButtonDown()
    {
        _model.SaveSettings();
        _lastSelectedElement = _view.SaveSettingsButton;
    }

    private void SetupMusicSlider()
    {
        _view.MusicSlider.SetValueNoSignal(_model.MusicVolume);
        _view.MusicSlider.DragEnded += OnMusicSliderDragEnded;
        _view.MusicSlider.DragStarted += OnMusicSliderDragStarted;
    }

    private void OnMusicSliderDragEnded(bool dragEnded)
    {
        if (dragEnded)
            _model.MusicVolume = (float)_view.MusicSlider.Value;
    }

    private void OnMusicSliderDragStarted()
    {
        _model.MusicVolume = (float)_view.MusicSlider.Value;
        _lastSelectedElement = _view.MusicSlider;
    }

    private void SetupSfxSlider()
    {
        _view.SfxSlider.SetValueNoSignal(_model.SfxVolume);
        _view.SfxSlider.DragEnded += OnSfxSliderDragEnded;
        _view.SfxSlider.DragStarted += OnSfxSliderDragStarted;
    }

    private void OnSfxSliderDragEnded(bool dragEnded)
    {
        if (dragEnded)
            _model.SfxVolume = (float)_view.SfxSlider.Value;
    }

    private void OnSfxSliderDragStarted()
    {
        _model.SfxVolume = (float)_view.SfxSlider.Value;
        _lastSelectedElement = _view.SfxSlider;
    }

    protected override void ResetViewToDefault()
    {
        _view.MusicSlider.SetValueNoSignal(_model.MusicVolume);
        _view.SfxSlider.SetValueNoSignal(_model.SfxVolume);
        _lastSelectedElement = _view.ResetButton;
    }
}
