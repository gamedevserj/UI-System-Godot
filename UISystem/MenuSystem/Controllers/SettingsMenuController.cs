using UISystem.Constants;
using UISystem.MenuSystem.Interfaces;
using UISystem.MenuSystem.Views;
using UISystem.PopupSystem;
using UISystem.PopupSystem.Enums;

namespace UISystem.MenuSystem.Controllers;
public abstract class SettingsMenuController<TView, TModel> : MenuController<TView, TModel> where TView : SettingsMenuView where TModel : ISettingsMenuModel
{

    protected readonly PopupsManager _popupsManager;

    protected SettingsMenuController(string prefab, TModel model, MenusManager menusManager, PopupsManager popupsManager) : base(prefab, model, menusManager)
    {
        _popupsManager = popupsManager;
    }

    protected abstract void ResetViewToDefault();

    protected override void OnReturnToPreviousMenuButtonDown()
    {
        if (_model.HasUnappliedSettings)
        {
            _popupsManager.ShowPopup(PopupType.YesNoCancel, this, PopupMessages.SaveChanges, (result) =>
            {
                OnReturnToPreviousMenuPopupClosed(result);
            });
        }
        else
        {
            base.OnReturnToPreviousMenuButtonDown();
        }
    }

    protected void OnReturnToPreviousMenuPopupClosed(PopupResult result)
    {
        switch (result)
        {
            case PopupResult.No:
                _model.DiscardChanges();
                base.OnReturnToPreviousMenuButtonDown();
                break;
            case PopupResult.Yes:
                _model.SaveSettings();
                base.OnReturnToPreviousMenuButtonDown();
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
