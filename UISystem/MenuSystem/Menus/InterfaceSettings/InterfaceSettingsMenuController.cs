using Godot;
using System;
using UISystem.Common.Enums;
using UISystem.Core.Elements.Structs;
using UISystem.Core.Extensions;
using UISystem.Core.MenuSystem.Interfaces;
using UISystem.Core.PopupSystem.Interfaces;
using UISystem.Core.Transitions.Interfaces;
using UISystem.MenuSystem.Constants;
using UISystem.MenuSystem.Models;
using UISystem.MenuSystem.SettingsMenu;
using UISystem.MenuSystem.Views;
using UISystem.Transitions.Interfaces;
using UISystem.Transitions;
using UISystem.Core.Elements.Interfaces;
using UISystem.Core.PhysicalInput;

namespace UISystem.MenuSystem.Controllers;
internal class InterfaceSettingsMenuController : SettingsMenuController<InterfaceSettingsMenuView, InterfaceSettingsMenuModel, Node, IFocusableControl>
{

    private const float PanelDuration = 0.5f;
    private const float ElementsDuration = 0.25f;
    private readonly int _controllerIconsNumber;

    public override int Type => MenuType.InterfaceSettings;


    public InterfaceSettingsMenuController(string prefab, InterfaceSettingsMenuModel model, IMenusManager<InputEvent> menusManager, Node parent,
        IInputProcessor<InputEvent> inputProcessor, IPopupsManager<InputEvent> popupsManager) :
        base(prefab, model, menusManager, parent, inputProcessor, popupsManager)
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
        _lastSelectedElement = _view.ReturnButton;
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

    protected override IViewTransition CreateTransition()
    {
        return new PanelSizeTransition(_view, _view.FadeObjectsContainer, _view.Panel,
            new ITweenableMenuElement[] { _view.ReturnButton, _view.ControllerIconsDropdown, _view.SaveSettingsButton, _view.ResetButton },
            PanelDuration, ElementsDuration);
    }
}
