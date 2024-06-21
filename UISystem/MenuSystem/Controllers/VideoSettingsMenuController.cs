using GodotExtensions;
using System.Text.RegularExpressions;
using UISystem.Common;
using UISystem.Constants;
using UISystem.MenuSystem.Enums;
using UISystem.MenuSystem.Models;
using UISystem.MenuSystem.Views;
using UISystem.PopupSystem;
using UISystem.PopupSystem.Enums;

namespace UISystem.MenuSystem.Controllers;
public class VideoSettingsMenuController : MenuController<VideoSettingsMenuView, VideoSettingsMenuModel>
{
    public override MenuType MenuType => MenuType.VideoSettings;

    private readonly PopupsManager _popupsManager;

    public VideoSettingsMenuController(string prefab, VideoSettingsMenuModel model, MenusManager menusManager, PopupsManager popupsManager) 
        : base(prefab, model, menusManager)
    {
        _popupsManager = popupsManager;
    }

    protected override void OnReturnToPreviousMenuButtonDown()
    {
        if (_model.HasUnappliedSettings)
        {
            _popupsManager.ShowPopup(PopupType.YesNoCancel, PopupMessages.SaveChanges, (result) =>
            {
                if (result == PopupResult.Yes)
                {
                    _model.SaveSettings();
                    base.OnReturnToPreviousMenuButtonDown();
                }
                else if (result == PopupResult.No)
                    base.OnReturnToPreviousMenuButtonDown();
            });
        }
        else
        {
            base.OnReturnToPreviousMenuButtonDown();
        }
    }

    protected override void SetupElements()
    {
        SetupWindowModeDropdown();
        SetupResolutionDropdown();
        _view.SaveSettingsButton.ButtonDown += _model.SaveSettings;
        _view.ResetToDefaultButton.ButtonDown += OnResetToDefaultButtonDown;
        _view.ReturnButton.ButtonDown += OnReturnToPreviousMenuButtonDown;
        _defaultSelectedElement = _view.ReturnButton;
    }

    private void OnResetToDefaultButtonDown()
    {
        _lastSelectedElement = _view.ResetToDefaultButton;
        _popupsManager.ShowPopup(PopupType.YesNo, PopupMessages.ResetToDefault, (result) =>
        {
            if (result == PopupResult.Yes)
            {
                _model.ResetToDefault();
                _view.WindowModeDropdown.Select(_model.CurrenWindowModeIndex);
                _view.ResolutionDropdown.Select(_model.CurrentResolutionIndex);
            }
            SwitchFocusAvailability(true);
        });
    }

    private void SetupWindowModeDropdown()
    {
        var windowModeNames = _model.GetWindowModeOptionNames();
        OptionButtonItem[] items = new OptionButtonItem[windowModeNames.Length];
        for (int i = 0; i < items.Length; i++)
        {
            var name = Regex.Replace(windowModeNames[i].ToString(), "([A-Z])", " $1").Trim(); // to have space in ExclusiveFullscreen
            items[i] = new OptionButtonItem(name, i);
        }
        _view.WindowModeDropdown.AddMultipleItems(items);
        _view.WindowModeDropdown.Select(_model.CurrenWindowModeIndex);
        _view.WindowModeDropdown.ItemSelected += OnWindowModeDropdownSelect;
    }

    private void SetupResolutionDropdown()
    {
        var resolutionNames = _model.GetAvailableResolutionNames();
        OptionButtonItem[] items = new OptionButtonItem[resolutionNames.Length];
        for (int i = 0; i < items.Length; i++)
        {
            items[i] = new OptionButtonItem(resolutionNames[i], i);
        }
        
        _view.ResolutionDropdown.AddMultipleItems(items);
        _view.ResolutionDropdown.Select(_model.CurrentResolutionIndex);
        _view.ResolutionDropdown.ItemSelected += OnResolutionDropdownSelect;
    }

    private void OnResolutionDropdownSelect(long index)
    {
        _model.SelectResolution((int)index);
    }

    private void OnWindowModeDropdownSelect(long index)
    {
        _model.SelectWindowMode((int)index);
    }

}
