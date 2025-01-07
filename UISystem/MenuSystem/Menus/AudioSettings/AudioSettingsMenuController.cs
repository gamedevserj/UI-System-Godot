using Godot;
using UISystem.Core.MenuSystem.Interfaces;
using UISystem.Core.PopupSystem.Interfaces;
using UISystem.MenuSystem.Constants;
using UISystem.MenuSystem.Models;
using UISystem.MenuSystem.SettingsMenu;
using UISystem.MenuSystem.Views;

namespace UISystem.MenuSystem.Controllers;
internal class AudioSettingsMenuController : SettingsMenuController<AudioSettingsMenuView, AudioSettingsMenuModel>
{

    public override int Type => MenuType.AudioSettings;

    public AudioSettingsMenuController(string prefab, AudioSettingsMenuModel model, IMenusManager menusManager, Node parent,
        IPopupsManager popupsManager)
        : base(prefab, model, menusManager, parent, popupsManager)
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
        _lastSelectedElement = _view.ReturnButton;
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
        _view.MusicSlider.SetValue(_model.MusicVolume);
        _view.SfxSlider.SetValue(_model.SfxVolume);
        _lastSelectedElement = _view.ResetButton;
    }
}
