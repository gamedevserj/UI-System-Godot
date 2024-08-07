﻿using UISystem.Constants;
using UISystem.MenuSystem.Enums;
using UISystem.MenuSystem.Models;
using UISystem.MenuSystem.Views;
using UISystem.PopupSystem;
using UISystem.PopupSystem.Enums;

namespace UISystem.MenuSystem.Controllers;
public class AudioSettingsMenuController : SettingsMenuController<AudioSettingsMenuView, AudioSettingsMenuModel>
{

    public override MenuType MenuType => MenuType.AudioSettings;

    private readonly PopupsManager _popupsManager;

    public AudioSettingsMenuController(string prefab, AudioSettingsMenuModel model, MenusManager menusManager,
        PopupsManager popupsManager) 
        : base(prefab, model, menusManager)
    {
        _popupsManager = popupsManager;
    }

    protected override void OnReturnToPreviousMenuButtonDown()
    {
        if (_model.HasUnappliedSettings)
        {
            _popupsManager.ShowPopup(PopupType.YesNoCancel, this, PopupMessages.SaveChanges, (result) =>
            {
                if (result == PopupResult.Yes)
                {
                    _model.SaveSettings();
                    base.OnReturnToPreviousMenuButtonDown();
                }
                else if (result == PopupResult.No)
                {
                    _model.DiscardChanges();
                    base.OnReturnToPreviousMenuButtonDown(); 
                }
            });
        }
        else
        {
            base.OnReturnToPreviousMenuButtonDown();
        }
    }

    protected override void SetupElements()
    {
        SetupMusicSlider();
        SetupSfxSlider();
        _view.SaveSettingsButton.ButtonDown += _model.SaveSettings;
        _view.ResetToDefaultButton.ButtonDown += OnResetToDefaultButtonDown;
        _view.ReturnButton.ButtonDown += OnReturnToPreviousMenuButtonDown;
        _defaultSelectedElement = _view.MusicSlider;
    }

    private void OnResetToDefaultButtonDown()
    {
        _lastSelectedElement = _view.ResetToDefaultButton;
        _popupsManager.ShowPopup(PopupType.YesNo, this, PopupMessages.ResetToDefault, (result) =>
        {
            if (result == PopupResult.Yes)
            {
                _model.ResetToDefault();
                ResetViewToDefault();
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

    protected override void ResetViewToDefault()
    {
        _view.MusicSlider.SetValueNoSignal(_model.MusicVolume);
        _view.SfxSlider.SetValueNoSignal(_model.SfxVolume);
    }
}
