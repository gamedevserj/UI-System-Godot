using GodotExtensions;
using System;
using UISystem.Common;
using UISystem.Common.Enums;
using UISystem.Constants;
using UISystem.MenuSystem;
using UISystem.MenuSystem.Controllers;
using UISystem.MenuSystem.Enums;
using UISystem.MenuSystem.Models;
using UISystem.MenuSystem.Views;
using UISystem.PopupSystem;
using UISystem.PopupSystem.Enums;

namespace UISystem.UISystem.MenuSystem.Controllers;
public class InterfaceSettingsMenuController : SettingsMenuController<InterfaceSettingsMenuView, InterfaceSettingsMenuModel>
{

    private readonly int _controllerIconsNumber;
    private readonly PopupsManager _popupsManager;

    public override MenuType MenuType => MenuType.InterfaceSettings;


    public InterfaceSettingsMenuController(string prefab, InterfaceSettingsMenuModel model, MenusManager menusManager, PopupsManager popupsManager) : 
        base(prefab, model, menusManager)
    {
        _controllerIconsNumber = Enum.GetNames(typeof(ControllerIconsType)).Length;
        _popupsManager = popupsManager;
    }

    protected override void OnReturnToPreviousMenuButtonDown()
    {
        if (_model.HasUnappliedSettings && CanReturnToPreviousMenu)
        {
            CanReturnToPreviousMenu = false;
            _popupsManager.ShowPopup(PopupType.YesNoCancel, this, PopupMessages.SaveChanges, (result) =>
            {
                CanReturnToPreviousMenu = true;
                if (result == PopupResult.Yes)
                {
                    _model.SaveSettings();
                    base.OnReturnToPreviousMenuButtonDown();
                }
                else if (result == PopupResult.No)
                {
                    _model.DiscardChanges();
                    base.OnReturnToPreviousMenuButtonDown();
                }
            });
        }
        else
        {
            base.OnReturnToPreviousMenuButtonDown();
        }
    }

    protected override void SetupElements()
    {
        _view.ReturnButton.ButtonDown += OnReturnToPreviousMenuButtonDown;
        SetupControllerIconsDropdown();
        _view.SaveSettingsButton.ButtonDown += _model.SaveSettings;
        _view.ResetToDefaultButton.ButtonDown += OnResetToDefaultButtonDown;
        _defaultSelectedElement = _view.ControllerIconsDropdown;
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
    }

    private void OnResetToDefaultButtonDown()
    {
        _popupsManager.ShowPopup(PopupType.YesNo, this, PopupMessages.ResetToDefault, (result) =>
        {
            if (result == PopupResult.Yes)
            {
                _model.ResetToDefault();
                _view.ControllerIconsDropdown.Select((int)_model.ControllerIconsType);
            }
        });
    }

    protected override void ResetViewToDefault()
    {
        _view.ControllerIconsDropdown.Select((int)_model.ControllerIconsType);
    }
}
