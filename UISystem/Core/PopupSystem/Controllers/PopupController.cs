using System;
using UISystem.Core.Controllers;
using UISystem.Core.MenuSystem;
using UISystem.Core.Views.Interfaces;

namespace UISystem.Core.PopupSystem;
internal abstract class PopupController<TViewHandler, TInputEvent, TView>
    : Controller<TViewHandler, TView, TInputEvent>, IPopupController<TInputEvent>
    where TViewHandler : IViewHandler<TView>
    where TView : IPopupView
{

    protected Action<int> _onHideAction;
    private IMenuController<TInputEvent> _caller;

    protected readonly IPopupsManager<TInputEvent> _popupsManager;

    public abstract int PressedReturnPopupResult { get; }

    public PopupController(TViewHandler viewHandler, IPopupsManager<TInputEvent> popupsManager)
    {
        _viewHandler = viewHandler;
        _popupsManager = popupsManager;
    }

    public override void Init()
    {
        if (!_viewHandler.IsViewValid)
        {
            _view = _viewHandler.CreateView();
            SetupElements();
        }
    }

    public void Show(IMenuController<TInputEvent> caller, string message, Action<int> onHideAction, bool instant = false)
    {
        CanReceivePhysicalInput = false;
        _caller = caller;
        _caller.CanReturnToPreviousMenu = false;
        _view.SetMessage(message);
        _onHideAction = onHideAction;
        _view.Show(() =>
        {
            CanReceivePhysicalInput = true;
            _view.FocusElement();
        }, instant);
    }

    public void Hide(int result, bool instant = false)
    {
        CanReceivePhysicalInput = false;
        _view.Hide(() =>
        {
            CanReceivePhysicalInput = true;
            _caller.CanReturnToPreviousMenu = true;
            _onHideAction?.Invoke(result);
            DestroyView();
        }, instant);
    }
    protected override void DestroyView() => _viewHandler.DestroyView();

    public override void OnReturnButtonDown()
    {
        _popupsManager.HidePopup(PressedReturnPopupResult);
    }

}
