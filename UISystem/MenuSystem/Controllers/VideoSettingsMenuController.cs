using Godot;
using GodotExtensions;
using MenuSystem.Enums;
using MenuSystem.Models;
using MenuSystem.Views;
using PopupSystem;
using PopupSystem.Enums;
using System.Text.RegularExpressions;
using UISystem.Common;
using UISystem.Constants;

namespace MenuSystem.Controllers;
public class VideoSettingsMenuController : MenuControllerFade<VideoSettingsMenuView, VideoSettingsMenuModel>
{
    public override MenuType MenuType => MenuType.VideoSettings;

    private readonly PopupsManager _popupsManager;

    public VideoSettingsMenuController(string prefab, VideoSettingsMenuModel model, MenusManager menusManager, SceneTree sceneTree,
        PopupsManager popupsManager) 
        : base(prefab, model, menusManager, sceneTree)
    {
        _popupsManager = popupsManager;
    }

    protected override void CreateView(Node menuParent)
    {
        base.CreateView(menuParent);
        SetupElements();
        _defaultSelectedElement = _view.ReturnButton;
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
        SetupWindowModeDropdown();
        SetupResolutionDropdown();
        _view.SaveSettingsButton.ButtonDown += _model.SaveSettings;
        _view.ReturnButton.ButtonDown += OnReturnToPreviousMenuButtonDown;
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
