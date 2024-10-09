using System.Text.RegularExpressions;
using UISystem.Common.Extensions;
using UISystem.Common.Structs;
using UISystem.Core.MenuSystem;
using UISystem.Core.PopupSystem;
using UISystem.MenuSystem.Models;
using UISystem.MenuSystem.SettingsMenu;
using UISystem.MenuSystem.Views;

namespace UISystem.MenuSystem.Controllers;
public class VideoSettingsMenuController : SettingsMenuController<VideoSettingsMenuView, VideoSettingsMenuModel>
{
    public override int Menu => MenuType.VideoSettings;

    public VideoSettingsMenuController(string prefab, VideoSettingsMenuModel model, MenusManager menusManager, PopupsManager popupsManager)
        : base(prefab, model, menusManager, popupsManager)
    {

    }

    protected override void SetupElements()
    {
        SetupWindowModeDropdown();
        SetupResolutionDropdown();
        _view.SaveSettingsButton.ButtonDown += _model.SaveSettings;
        _view.ResetButton.ButtonDown += OnResetToDefaultButtonDown;
        _view.ReturnButton.ButtonDown += OnReturnButtonDown;
        DefaultSelectedElement = _view.ReturnButton;
    }

    private void OnReturnButtonDown()
    {
        OnReturnToPreviousMenuButtonDown();
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

    protected override void ResetViewToDefault()
    {
        _view.WindowModeDropdown.Select(_model.CurrenWindowModeIndex);
        _view.ResolutionDropdown.Select(_model.CurrentResolutionIndex);
    }
}
