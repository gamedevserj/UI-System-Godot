using Godot;
using MenuSystem.Enums;
using MenuSystem.Models;
using MenuSystem.Views;
using PopupSystem;
using PopupSystem.Enums;
using UISystem.Constants;

namespace MenuSystem.Controllers;
public class AudioSettingsMenuController : MenuControllerFade<AudioSettingsMenuView, AudioSettingsMenuModel>
{

    public override MenuType MenuType => MenuType.AudioSettings;

    private readonly PopupsManager _popupsManager;

    public AudioSettingsMenuController(string prefab, AudioSettingsMenuModel model, MenusManager menusManager, SceneTree sceneTree,
        PopupsManager popupsManager) 
        : base(prefab, model, menusManager, sceneTree)
    {
        _popupsManager = popupsManager;
    }

    protected override void CreateView(Node menuParent)
    {
        base.CreateView(menuParent);
        SetupElements();
        _defaultSelectedElement = _view.MusicSlider;
    }

    protected override void OnReturnToPreviousMenuButtonDown()
    {
        if (_model.HasUnappliedSettings)
        {
            _popupsManager.ShowPopup(PopupType.ConfirmationPopup, PopupMessages.SaveChanges, (result) =>
            {
                if (result)
                    _model.SaveSettings();

                base.OnReturnToPreviousMenuButtonDown();
            });
        }
        else
        {
            base.OnReturnToPreviousMenuButtonDown();
        }
    }

    private void SetupElements()
    {
        SetupMusicSlider();
        SetupSfxSlider();
        _view.SaveSettingsButton.ButtonDown += _model.SaveSettings;
        _view.ResetToDefaultButton.ButtonDown += OnResetToDefaultButtonDown;
        _view.ReturnButton.ButtonDown += OnReturnToPreviousMenuButtonDown;
    }

    private void OnResetToDefaultButtonDown()
    {
        _lastSelectedElement = _view.ResetToDefaultButton;
        _popupsManager.ShowPopup(PopupType.ConfirmationPopup, "Reset to default?", (result) =>
        {
            if (result)
            {
                _model.ResetSettingsToDefault();
                _view.MusicSlider.SetValueNoSignal(_model.MusicVolume);
                _view.SfxSlider.SetValueNoSignal(_model.SfxVolume);
            }
            SwitchFocusAvailability(true);
        });
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
        {
            _model.MusicVolume = (float)_view.MusicSlider.Value;
        }
    }

    private void OnMusicSliderDragStarted()
    {
        _model.MusicVolume = (float)_view.MusicSlider.Value;
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
        {
            _model.SfxVolume = (float)_view.SfxSlider.Value;
        }
    }

    private void OnSfxSliderDragStarted()
    {
        _model.SfxVolume = (float)_view.SfxSlider.Value;
    }

}
