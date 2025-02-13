using System;
using UISystem.Common.Enums;
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
internal class InterfaceSettingsMenuController : SettingsMenuController<IViewCreator<InterfaceSettingsMenuView>, InterfaceSettingsMenuView, InterfaceSettingsMenuModel>
{

    private readonly int _controllerIconsNumber;
    public override MenuType Type => MenuType.InterfaceSettings;

    public InterfaceSettingsMenuController(IViewCreator<InterfaceSettingsMenuView> viewCreator, InterfaceSettingsMenuModel model, 
        IMenusManager<MenuType> menusManager, IPopupsManager<PopupType, PopupResult> popupsManager) 
        : base(viewCreator, model, menusManager, popupsManager)
    {
        _controllerIconsNumber = Enum.GetNames(typeof(ControllerIconsType)).Length;
    }

    protected override void SetupElements()
    {
        SetupControllerIconsDropdown();
        base.SetupElements();
        _view.SaveSettingsButton.ButtonDown += OnSaveSettingsButtonDown;
    }

    private void OnSaveSettingsButtonDown()
    {
        _model.SaveSettings();
        _view.SetLastSelectedElement(_view.SaveSettingsButton);
    }

    private void SetupControllerIconsDropdown()
    {
        OptionButtonItem[] items = new OptionButtonItem[_controllerIconsNumber];
        for (int i = 0; i < items.Length; i++)
        {
            var name = ((ControllerIconsType)i).ToString();
            items[i] = new OptionButtonItem(name, i);
        }
        _view.ControllerIconsDropdown.AddMultipleItems(items);
        _view.ControllerIconsDropdown.ItemSelected += SelectControllerIconsType;
        _view.ControllerIconsDropdown.SelectItem((int)_model.ControllerIconsType);
    }

    private void SelectControllerIconsType(long index)
    {
        _model.SelectIconType((int)index);
        _view.SetLastSelectedElement(_view.ControllerIconsDropdown);
    }

    protected override void ResetViewToDefault()
    {
        _view.ControllerIconsDropdown.SelectItem((int)_model.ControllerIconsType);
    }

}
