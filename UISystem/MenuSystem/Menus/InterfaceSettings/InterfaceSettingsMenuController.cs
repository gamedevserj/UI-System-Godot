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

    public InterfaceSettingsMenuController(
        IViewCreator<InterfaceSettingsMenuView> viewCreator, 
        IMenusManager menusManager, 
        InterfaceSettingsMenuModel model, 
        IPopupsManager<PopupResult> popupsManager) 
        : base(viewCreator, menusManager, model, popupsManager)
    {
        _controllerIconsNumber = Enum.GetNames(typeof(ControllerIconsType)).Length;
    }

    protected override void SetupElements()
    {
        SetupControllerIconsDropdown();
        base.SetupElements();
        View.SaveSettingsButton.ButtonDown += OnSaveSettingsButtonDown;
    }

    private void OnSaveSettingsButtonDown()
    {
        _model.SaveSettings();
        View.SetLastSelectedElement(View.SaveSettingsButton);
    }

    private void SetupControllerIconsDropdown()
    {
        OptionButtonItem[] items = new OptionButtonItem[_controllerIconsNumber];
        for (int i = 0; i < items.Length; i++)
        {
            var name = ((ControllerIconsType)i).ToString();
            items[i] = new OptionButtonItem(name, i);
        }
        View.ControllerIconsDropdown.AddMultipleItems(items);
        View.ControllerIconsDropdown.ItemSelected += SelectControllerIconsType;
        View.ControllerIconsDropdown.SelectItem((int)_model.ControllerIconsType);
    }

    private void SelectControllerIconsType(long index)
    {
        _model.SelectIconType((int)index);
        View.SetLastSelectedElement(View.ControllerIconsDropdown);
    }

    protected override void ResetViewToDefault()
    {
        View.ControllerIconsDropdown.SelectItem((int)_model.ControllerIconsType);
    }

}
