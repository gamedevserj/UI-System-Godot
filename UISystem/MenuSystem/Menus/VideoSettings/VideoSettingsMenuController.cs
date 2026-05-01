using System.Text.RegularExpressions;
using UISystem.Core.MenuSystem;
using UISystem.Core.PopupSystem;
using UISystem.Core.Views;
using UISystem.Elements;
using UISystem.Extensions;
using UISystem.MenuSystem.Models;
using UISystem.MenuSystem.SettingsMenu;
using UISystem.MenuSystem.Views;

namespace UISystem.MenuSystem.Controllers;

/// <summary>
/// Video settings menu controller.
/// </summary>
internal class VideoSettingsMenuController : SettingsMenuController<IViewCreator<VideoSettingsMenuView>, VideoSettingsMenuView, VideoSettingsMenuModel>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="VideoSettingsMenuController"/> class.
    /// </summary>
    /// <param name="viewCreator">View creator.</param>
    /// <param name="menusManager">Menus manager.</param>
    /// <param name="model">Video settings menu model.</param>
    /// <param name="popupsManager">Popups manager.</param>
    public VideoSettingsMenuController(
        IViewCreator<VideoSettingsMenuView> viewCreator,
        IMenusManager menusManager,
        VideoSettingsMenuModel model,
        IPopupsManager popupsManager)
        : base(viewCreator, menusManager, model, popupsManager)
    {
    }

    /// <inheritdoc/>
    protected override void SetupElements()
    {
        base.SetupElements();
        SetupWindowModeDropdown();
        SetupResolutionDropdown();
        View.SaveSettingsButton.ButtonDown += Model.SaveSettings;
    }

    /// <inheritdoc/>
    protected override void UpdateFullView()
    {
        View.WindowModeDropdown.SelectItem(Model.CurrenWindowModeIndex);
        View.ResolutionDropdown.SelectItem(Model.CurrentResolutionIndex);
    }

    private void SetupWindowModeDropdown()
    {
        var windowModeNames = VideoSettingsMenuModel.WindowModeOptionNames;
        OptionButtonItem[] items = new OptionButtonItem[windowModeNames.Length];
        for (int i = 0; i < items.Length; i++)
        {
            var name = Regex.Replace(windowModeNames[i].ToString(), "([A-Z])", " $1").Trim(); // to have space in ExclusiveFullscreen
            items[i] = new OptionButtonItem(name, i);
        }

        View.WindowModeDropdown.AddMultipleItems(items);
        View.WindowModeDropdown.SelectItem(Model.CurrenWindowModeIndex);
        View.WindowModeDropdown.ItemSelected += OnWindowModeDropdownSelect;
    }

    private void SetupResolutionDropdown()
    {
        var resolutionNames = VideoSettingsMenuModel.AvailableResolutionNames;
        OptionButtonItem[] items = new OptionButtonItem[resolutionNames.Length];
        for (int i = 0; i < items.Length; i++)
        {
            items[i] = new OptionButtonItem(resolutionNames[i], i);
        }

        View.ResolutionDropdown.AddMultipleItems(items);

        // if player resizes window, there won't be any matching resolutions
        // this is to prevent dropdown being empty and show some value
        int index = Model.CurrentResolutionIndex > 0 ? Model.CurrentResolutionIndex : 0;
        View.ResolutionDropdown.SelectItem(index);
        View.ResolutionDropdown.ItemSelected += OnResolutionDropdownSelect;
    }

    private void OnResolutionDropdownSelect(long index)
    {
        Model.SelectResolution((int)index);
    }

    private void OnWindowModeDropdownSelect(long index)
    {
        Model.SelectWindowMode((int)index);
    }
}
