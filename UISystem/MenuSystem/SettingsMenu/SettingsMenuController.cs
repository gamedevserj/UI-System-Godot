using Godot;
using UISystem.Constants;
using UISystem.Core.MenuSystem;
using UISystem.Core.PopupSystem;
using UISystem.Core.Views;
using UISystem.Elements;
using UISystem.PopupSystem;
using UISystem.PopupSystem.Constants;

namespace UISystem.MenuSystem.SettingsMenu;
internal abstract class SettingsMenuController<TViewHandler, TView, TModel>
    : MenuController<TViewHandler, TView, TModel, InputEvent, IFocusableControl>
    where TViewHandler : IViewModel<TView>
    where TView : SettingsMenuView
    where TModel : ISettingsMenuModel
{

    protected readonly IPopupsManager<InputEvent> _popupsManager;

    protected SettingsMenuController(TViewHandler viewHandler, TModel model, IMenusManager<InputEvent> menusManager, 
        IPopupsManager<InputEvent> popupsManager) : base(viewHandler, model, menusManager)
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
            SwitchFocusAvailability(false);
            _popupsManager.ShowPopup(PopupType.YesNoCancel, PopupMessages.SaveChanges, (result) =>
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

    protected void OnReturnToPreviousMenuPopupClosed(int result)
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
                SwitchFocusAvailability(true);
                break;
            default:
                SwitchFocusAvailability(true);
                break;
        }
    }

    protected virtual void OnResetToDefaultButtonDown()
    {
        _view.SetLastSelectedElement(_view.ResetButton);
        SwitchFocusAvailability(false);
        _popupsManager.ShowPopup(PopupType.YesNo, PopupMessages.ResetToDefault, (result) =>
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
