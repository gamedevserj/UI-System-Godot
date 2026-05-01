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

namespace UISystem.MenuSystem.Controllers;

/// <summary>
/// Interface settings menu controller.
/// </summary>
internal class InterfaceSettingsMenuController : SettingsMenuController<IViewCreator<InterfaceSettingsMenuView>, InterfaceSettingsMenuView, InterfaceSettingsMenuModel>
{
    private readonly int _controllerIconsTypesAmount;

    /// <summary>
    /// Initializes a new instance of the <see cref="InterfaceSettingsMenuController"/> class.
    /// </summary>
    /// <param name="viewCreator">View creator.</param>
    /// <param name="menusManager">Menus manager.</param>
    /// <param name="model">Interface settings menu model.</param>
    /// <param name="popupsManager">Popups manager.</param>
    public InterfaceSettingsMenuController(
        IViewCreator<InterfaceSettingsMenuView> viewCreator,
        IMenusManager menusManager,
        InterfaceSettingsMenuModel model,
        IPopupsManager popupsManager)
        : base(viewCreator, menusManager, model, popupsManager)
    {
        _controllerIconsTypesAmount = Enum.GetNames(typeof(ControllerIconsType)).Length;
    }

    /// <inheritdoc/>
    protected override void SetupElements()
    {
        base.SetupElements();
        SetupControllerIconsDropdown();
        View.SaveSettingsButton.ButtonDown += OnSaveSettingsButtonDown;
    }

    /// <inheritdoc/>
    protected override void UpdateFullView()
    {
        View.ControllerIconsDropdown.SelectItem((int)Model.ControllerIconsType);
    }

    private void OnSaveSettingsButtonDown()
    {
        Model.SaveSettings();
        View.SetLastSelectedElement(View.SaveSettingsButton);
    }

    private void SetupControllerIconsDropdown()
    {
        OptionButtonItem[] items = new OptionButtonItem[_controllerIconsTypesAmount];
        for (int i = 0; i < items.Length; i++)
        {
            var name = ((ControllerIconsType)i).ToString();
            items[i] = new OptionButtonItem(name, i);
        }

        View.ControllerIconsDropdown.AddMultipleItems(items);
        View.ControllerIconsDropdown.ItemSelected += SelectControllerIconsType;
        View.ControllerIconsDropdown.SelectItem((int)Model.ControllerIconsType);
    }

    private void SelectControllerIconsType(long index)
    {
        Model.SelectIconType((int)index);
        View.SetLastSelectedElement(View.ControllerIconsDropdown);
    }
}
