using System;
using UISystem.Constants;
using UISystem.Core.MenuSystem;
using UISystem.Core.PopupSystem;
using UISystem.Core.Views;
using UISystem.Elements;
using UISystem.PopupSystem;
using UISystem.PopupSystem.Popups.Controllers;
using UISystem.PopupSystem.Popups.Views;

namespace UISystem.MenuSystem.SettingsMenu;
internal abstract class SettingsMenuController<TViewCreator, TView, TModel>
    : MenuController<TViewCreator, TView, TModel, IFocusableControl>
    where TViewCreator : IViewCreator<TView>
    where TView : SettingsMenuView
    where TModel : ISettingsMenuModel
{

    protected readonly IPopupsManager<PopupResult> _popupsManager;

    protected SettingsMenuController(TViewCreator viewCreator, TModel model, IMenusManager menusManager, 
        IPopupsManager<PopupResult> popupsManager) : base(viewCreator, model, menusManager)
    {
        _popupsManager = popupsManager;
    }

    protected abstract void ResetViewToDefault();

    protected override void SetupElements()
    {
        _view.ReturnButton.ButtonDown += OnReturnButtonDown;
        _view.ResetButton.ButtonDown += OnResetToDefaultButtonDown;
    }

    public override void OnReturnButtonDown()
    {
        if (_model.HasUnappliedSettings)
        {
            _view.SetLastSelectedElement(_view.ReturnButton);
            CanReceivePhysicalInput = false;
            SwitchInteractability(false);
            _popupsManager.ShowPopup(typeof(YesNoCancelPopupView), PopupMessages.SaveChanges, (result) =>
            {
                OnReturnToPreviousMenuPopupClosed(result);
                CanReceivePhysicalInput = true;
            });
        }
        else
        {
            base.OnReturnButtonDown();
        }
    }

    protected void OnReturnToPreviousMenuPopupClosed(PopupResult result)
    {
        switch (result)
        {
            case PopupResult.No:
                _model.DiscardChanges();
                base.OnReturnButtonDown();
                break;
            case PopupResult.Yes:
                _model.SaveSettings();
                base.OnReturnButtonDown();
                break;
            case PopupResult.Cancel:
                SwitchInteractability(true);
                break;
            default:
                SwitchInteractability(true);
                break;
        }
    }

    protected virtual void OnResetToDefaultButtonDown()
    {
        _view.SetLastSelectedElement(_view.ResetButton);
        SwitchInteractability(false);
        _popupsManager.ShowPopup(typeof(YesNoPopupView), PopupMessages.ResetToDefault, (result) =>
        {
            if (result == PopupResult.Yes)
            {
                _model.ResetToDefault();
                ResetViewToDefault();
            }
            SwitchInteractability(true);
        });
    }
}
