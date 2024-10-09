using System;
using UISystem.Constants;
using UISystem.Core.MenuSystem;
using UISystem.Core.MenuSystem.Controllers;
using UISystem.Core.MenuSystem.Interfaces;
using UISystem.Core.PopupSystem;
using UISystem.Core.PopupSystem.Enums;
using UISystem.PopupSystem;

namespace UISystem.MenuSystem.SettingsMenu;
public abstract class SettingsMenuController<TView, TModel> : MenuController<TView, TModel> where TView : SettingsMenuView where TModel : ISettingsMenuModel
{

    protected readonly PopupsManager _popupsManager;

    protected SettingsMenuController(string prefab, TModel model, MenusManager menusManager, PopupsManager popupsManager) : base(prefab, model, menusManager)
    {
        _popupsManager = popupsManager;
    }

    protected abstract void ResetViewToDefault();

    protected override void OnReturnToPreviousMenuButtonDown(Action onComplete = null, bool instant = false)
    {
        if (_model.HasUnappliedSettings)
        {
            SwitchFocusAvailability(false);
            _popupsManager.ShowPopup(PopupType.YesNoCancel, this, PopupMessages.SaveChanges, (result) =>
            {
                OnReturnToPreviousMenuPopupClosed(result, onComplete, instant);
                onComplete?.Invoke();
            });
        }
        else
        {
            base.OnReturnToPreviousMenuButtonDown(onComplete, instant);
        }
    }

    protected void OnReturnToPreviousMenuPopupClosed(PopupResult result, Action onComplete = null, bool instant = false)
    {
        switch (result)
        {
            case PopupResult.No:
                _model.DiscardChanges();
                base.OnReturnToPreviousMenuButtonDown(onComplete, instant);
                break;
            case PopupResult.Yes:
                _model.SaveSettings();
                base.OnReturnToPreviousMenuButtonDown(onComplete, instant);
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
