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

    public override MenuType Type => MenuType.VideoSettings;

    public VideoSettingsMenuController(IViewCreator<VideoSettingsMenuView> viewCreator, VideoSettingsMenuModel model, 
        IMenusManager<MenuType> menusManager, IPopupsManager<PopupType, PopupResult> popupsManager) : base(viewCreator, model, menusManager, popupsManager)
    { }

    protected override void SetupElements()
    {
        base.SetupElements();
        SetupWindowModeDropdown();
        SetupResolutionDropdown();
        _view.SaveSettingsButton.ButtonDown += _model.SaveSettings;
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
        _view.WindowModeDropdown.SelectItem(_model.CurrenWindowModeIndex);
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
        // if player resizes window, there won't be any matching resolutions
        // this is to prevent dropdown being empty and show some value
        int index = _model.CurrentResolutionIndex > 0 ? _model.CurrentResolutionIndex : 0;
        _view.ResolutionDropdown.SelectItem(index);
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
        _view.WindowModeDropdown.SelectItem(_model.CurrenWindowModeIndex);
        _view.ResolutionDropdown.SelectItem(_model.CurrentResolutionIndex);
    }
}
