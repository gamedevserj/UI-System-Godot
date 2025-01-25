using System;
using UISystem.Core.MenuSystem.Interfaces;
using UISystem.Core.PopupSystem.Interfaces;
using UISystem.Core.Views.Interfaces;

namespace UISystem.Core.PopupSystem.Controllers;
internal abstract class PopupController<TViewHandler, TInputEvent, TView> : IPopupController<TInputEvent> 
    where TViewHandler : IViewHandler<TView>
    where TView : IPopupView
{

    protected TViewHandler _viewHandler;
    protected TView _view;

    protected Action<int> _onHideAction;
    private IMenuController<TInputEvent> _caller;

    protected readonly IPopupsManager<TInputEvent> _popupsManager;

    public abstract int Type { get; }
    public abstract int PressedReturnPopupResult { get; }
    public bool CanReceivePhysicalInput { get; private set; } // to prevent input processing during transitions

    public PopupController(TViewHandler viewHandler, IPopupsManager<TInputEvent> popupsManager)
    {
        _viewHandler = viewHandler;
        _popupsManager = popupsManager;
    }

    public virtual void Init()
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
        //_viewHandler.Message.Text = message;
        _view.SetMessage(message);
        _onHideAction = onHideAction;
        _view.Show((() =>
        {
            CanReceivePhysicalInput = true;
            _view.FocusElement();
        }), instant);
    }

    public void Hide(int result, bool instant = false)
    {
        CanReceivePhysicalInput = false;
        _view.Hide((() =>
        {
            CanReceivePhysicalInput = true;
            _caller.CanReturnToPreviousMenu = true;
            _onHideAction?.Invoke(result);
            DestroyView();
        }), instant);
    }

    public virtual void OnCancelButtonDown()
    {
        _popupsManager.HidePopup(PressedReturnPopupResult);
    }

    public void OnPauseButtonDown()
    { }
    public void OnResumeButtonDown()
    { }
    public void OnAnyButtonDown(TInputEvent inputEvent)
    { }

    private void DestroyView() => _viewHandler.DestroyView();

    protected abstract void SetupElements();

}
