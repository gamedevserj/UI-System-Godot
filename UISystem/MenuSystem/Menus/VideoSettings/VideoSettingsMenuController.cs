using System.Text.RegularExpressions;
using UISystem.Core.MenuSystem;
using UISystem.Core.PopupSystem;
using UISystem.Core.Views;
using UISystem.Elements;
using UISystem.Extensions;
using UISystem.MenuSystem.Models;
using UISystem.MenuSystem.SettingsMenu;
using UISystem.MenuSystem.Views;
using UISystem.PopupSystem;

namespace UISystem.MenuSystem.Controllers;
internal class VideoSettingsMenuController : SettingsMenuController<IViewCreator<VideoSettingsMenuView>, VideoSettingsMenuView, VideoSettingsMenuModel>
{

    public VideoSettingsMenuController(
        IViewCreator<VideoSettingsMenuView> viewCreator, 
        IMenusManager menusManager, 
        VideoSettingsMenuModel model, 
        IPopupsManager<PopupResult> popupsManager) 
        : base(viewCreator, menusManager, model, popupsManager)
    { }

    protected override void SetupElements()
    {
        base.SetupElements();
        SetupWindowModeDropdown();
        SetupResolutionDropdown();
        View.SaveSettingsButton.ButtonDown += _model.SaveSettings;
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
        View.WindowModeDropdown.AddMultipleItems(items);
        View.WindowModeDropdown.SelectItem(_model.CurrenWindowModeIndex);
        View.WindowModeDropdown.ItemSelected += OnWindowModeDropdownSelect;
    }

    private void SetupResolutionDropdown()
    {
        var resolutionNames = _model.GetAvailableResolutionNames();
        OptionButtonItem[] items = new OptionButtonItem[resolutionNames.Length];
        for (int i = 0; i < items.Length; i++)
        {
            items[i] = new OptionButtonItem(resolutionNames[i], i);
        }

        View.ResolutionDropdown.AddMultipleItems(items);
        // if player resizes window, there won't be any matching resolutions
        // this is to prevent dropdown being empty and show some value
        int index = _model.CurrentResolutionIndex > 0 ? _model.CurrentResolutionIndex : 0;
        View.ResolutionDropdown.SelectItem(index);
        View.ResolutionDropdown.ItemSelected += OnResolutionDropdownSelect;
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
        View.WindowModeDropdown.SelectItem(_model.CurrenWindowModeIndex);
        View.ResolutionDropdown.SelectItem(_model.CurrentResolutionIndex);
    }
}
