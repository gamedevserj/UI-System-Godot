using Godot;
using System;
using UISystem.Constants;
using UISystem.Core.Elements.Interfaces;
using UISystem.Core.PhysicalInput;
using UISystem.Core.MenuSystem.Interfaces;
using UISystem.Core.PopupSystem.Interfaces;
using UISystem.MenuSystem.SettingsMenu.Interfaces;
using UISystem.PopupSystem;
using UISystem.PopupSystem.Constants;

namespace UISystem.MenuSystem.SettingsMenu;
internal abstract class SettingsMenuController<TView, TModel, TParent, TFocusableElement> : MenuController<string, TView, TModel, TParent, IFocusableControl> 
    where TView : SettingsMenuView
    where TModel : ISettingsMenuModel
{

    protected readonly IPopupsManager<InputEvent> _popupsManager;

    protected SettingsMenuController(string prefab, TModel model, IMenusManager<InputEvent> menusManager, Node parent,
        IPopupsManager<InputEvent> popupsManager) 
        : base(prefab, model, menusManager, parent)
    {
        _popupsManager = popupsManager;
    }

    protected abstract void ResetViewToDefault();

    public override void OnCancelButtonDown()
    {
        if (_model.HasUnappliedSettings)
        {
            SwitchFocusAvailability(false);
            _popupsManager.ShowPopup(PopupType.YesNoCancel, this, PopupMessages.SaveChanges, (result) =>
            {
                OnReturnToPreviousMenuPopupClosed(result);
            });
        }
        else
        {
            base.OnCancelButtonDown();
        }
    }

    protected void OnReturnToPreviousMenuPopupClosed(int result)
    {
        switch (result)
        {
            case PopupResult.No:
                _model.DiscardChanges();
                base.OnCancelButtonDown();
                break;
            case PopupResult.Yes:
                _model.SaveSettings();
                base.OnCancelButtonDown();
                break;
            case PopupResult.Cancel:
                SwitchFocusAvailability(true);
                break;
            default:
                SwitchFocusAvailability(true);
                break;
        }
    }

    protected virtual void OnResetToDefaultButtonDown()
    {
        _lastSelectedElement = _view.ResetButton;
        SwitchFocusAvailability(false);
        _popupsManager.ShowPopup(PopupType.YesNo, this, PopupMessages.ResetToDefault, (result) =>
        {
            if (result == PopupResult.Yes)
            {
                _model.ResetToDefault();
                ResetViewToDefault();
            }
            SwitchFocusAvailability(true);
        });
    }
}
