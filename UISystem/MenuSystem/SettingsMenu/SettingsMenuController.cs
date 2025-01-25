using Godot;
using UISystem.Constants;
using UISystem.Core.Elements.Interfaces;
using UISystem.Core.MenuSystem.Controllers;
using UISystem.Core.MenuSystem.Interfaces;
using UISystem.Core.PopupSystem.Interfaces;
using UISystem.Core.Views.Interfaces;
using UISystem.MenuSystem.SettingsMenu.Interfaces;
using UISystem.PopupSystem;
using UISystem.PopupSystem.Constants;

namespace UISystem.MenuSystem.SettingsMenu;
internal abstract class SettingsMenuController<TViewHandler, TInputEvent, TView, TModel>
    : MenuController<TViewHandler, TView, TModel, InputEvent, IFocusableControl>
    where TViewHandler : IViewHandler<TView>
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
    private void OnReturnButtonDown()
    {
        _view.SetLastSelectedElement(_view.ReturnButton);
        OnCancelButtonDown();
    }

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
        _view.SetLastSelectedElement(_view.ResetButton);
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
