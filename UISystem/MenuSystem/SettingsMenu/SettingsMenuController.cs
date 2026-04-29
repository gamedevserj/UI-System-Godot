using UISystem.Constants;
using UISystem.Core.MenuSystem;
using UISystem.Core.PopupSystem;
using UISystem.Core.Views;
using UISystem.Elements;
using UISystem.PopupSystem;
using UISystem.PopupSystem.Popups.Views;

namespace UISystem.MenuSystem.SettingsMenu;
internal abstract class SettingsMenuController<TViewCreator, TView, TModel>
    : MenuController<TViewCreator, TView, IFocusableUiElement>
    where TViewCreator : IViewCreator<TView>
    where TView : SettingsMenuView
    where TModel : ISettingsMenuModel
{
    protected readonly TModel _model;
    protected readonly IPopupsManager<PopupResult> _popupsManager;

    protected SettingsMenuController(
        TViewCreator viewCreator, 
        IMenusManager menusManager, 
        TModel model,
        IPopupsManager<PopupResult> popupsManager) 
        : base(viewCreator, menusManager)
    {
        _model = model;
        _popupsManager = popupsManager;
    }

    protected abstract void ResetViewToDefault();

    protected override void SetupElements()
    {
        View.ReturnButton.ButtonDown += OnReturnButtonDown;
        View.ResetButton.ButtonDown += OnResetToDefaultButtonDown;
    }

    public override void OnReturnButtonDown()
    {
        if (_model.HasUnappliedSettings)
        {
            View.SetLastSelectedElement(View.ReturnButton);
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
        View.SetLastSelectedElement(View.ResetButton);
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
