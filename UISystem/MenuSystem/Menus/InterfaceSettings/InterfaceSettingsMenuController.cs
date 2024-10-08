using System;
using UISystem.Common.Enums;
using UISystem.Core.Common.Structs;
using UISystem.Core.Extensions;
using UISystem.Core.MenuSystem;
using UISystem.Core.MenuSystem.Controllers;
using UISystem.Core.PopupSystem;
using UISystem.MenuSystem.Models;
using UISystem.MenuSystem.Views;

namespace UISystem.MenuSystem.Controllers;
public class InterfaceSettingsMenuController : SettingsMenuController<InterfaceSettingsMenuView, InterfaceSettingsMenuModel>
{

    private readonly int _controllerIconsNumber;

    public override int Menu => MenuType.InterfaceSettings;


    public InterfaceSettingsMenuController(string prefab, InterfaceSettingsMenuModel model, MenusManager menusManager, PopupsManager popupsManager) : 
        base(prefab, model, menusManager, popupsManager)
    {
        _controllerIconsNumber = Enum.GetNames(typeof(ControllerIconsType)).Length;
    }

    protected override void SetupElements()
    {
        _view.ReturnButton.ButtonDown += OnReturnButtonDown;
        SetupControllerIconsDropdown();
        _view.SaveSettingsButton.ButtonDown += OnSaveSettingsButtonDown;
        _view.ResetButton.ButtonDown += OnResetToDefaultButtonDown;
        DefaultSelectedElement = _view.ReturnButton;
    }

    private void OnReturnButtonDown()
    {
        OnReturnToPreviousMenuButtonDown();
    }

    private void OnSaveSettingsButtonDown()
    {
        _model.SaveSettings();
        _lastSelectedElement = _view.SaveSettingsButton;
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
        _lastSelectedElement = _view.ControllerIconsDropdown;
    }

    protected override void ResetViewToDefault()
    {
        _view.ControllerIconsDropdown.Select((int)_model.ControllerIconsType);
    }
}
