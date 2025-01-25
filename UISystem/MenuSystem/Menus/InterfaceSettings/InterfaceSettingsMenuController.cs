using Godot;
using System;
using UISystem.Common.Enums;
using UISystem.Core.Elements.Structs;
using UISystem.Core.Extensions;
using UISystem.Core.MenuSystem.Interfaces;
using UISystem.Core.PopupSystem.Interfaces;
using UISystem.MenuSystem.Constants;
using UISystem.MenuSystem.Models;
using UISystem.MenuSystem.SettingsMenu;
using UISystem.MenuSystem.ViewHandlers;
using UISystem.MenuSystem.Views;

namespace UISystem.MenuSystem.Controllers;
internal class InterfaceSettingsMenuController<TViewHandler, TInputEvent>
    : SettingsMenuController<InterfaceSettingsMenuViewHandler<InterfaceSettingsMenuView>, InputEvent, InterfaceSettingsMenuView, InterfaceSettingsMenuModel>
{

    private readonly int _controllerIconsNumber;
    public override int Type => MenuType.InterfaceSettings;

    public InterfaceSettingsMenuController(InterfaceSettingsMenuViewHandler<InterfaceSettingsMenuView> viewHandler, InterfaceSettingsMenuModel model, IMenusManager<InputEvent> menusManager, IPopupsManager<InputEvent> popupsManager) : base(viewHandler, model, menusManager, popupsManager)
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
        _view.ControllerIconsDropdown.Select((int)_model.ControllerIconsType);
    }

    private void SelectControllerIconsType(long index)
    {
        _model.SelectIconType((int)index);
        _view.SetLastSelectedElement(_view.ControllerIconsDropdown);
    }

    protected override void ResetViewToDefault()
    {
        _view.ControllerIconsDropdown.Select((int)_model.ControllerIconsType);
    }

}
