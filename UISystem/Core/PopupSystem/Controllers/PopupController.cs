using System;
using UISystem.Core.Views;

namespace UISystem.Core.PopupSystem;
internal abstract class PopupController<TViewCreator, TInputEvent, TView>
    : Controller<TViewCreator, TView, TInputEvent>, IPopupController<TInputEvent>
    where TViewCreator : IViewCreator<TView>
    where TView : IPopupView
{

    protected Action<int> _onHideAction;

    protected readonly IPopupsManager<TInputEvent> _popupsManager;

    public abstract int PressedReturnPopupResult { get; }

    public PopupController(TViewCreator viewCreator, IPopupsManager<TInputEvent> popupsManager)
    {
        _viewCreator = viewCreator;
        _popupsManager = popupsManager;
    }

    public override void Init()
    {
        if (!_viewCreator.IsViewValid)
        {
            _view = _viewCreator.CreateView();
            SetupElements();
        }
    }

    public void Show(string message, Action<int> onHideAction, bool instant = false)
    {
        CanReceivePhysicalInput = false;
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
            _onHideAction?.Invoke(result);
            DestroyView();
        }, instant);
    }
    protected override void DestroyView() => _viewCreator.DestroyView();

    public override void OnReturnButtonDown()
    {
        _popupsManager.HidePopup(PressedReturnPopupResult);
    }

}
