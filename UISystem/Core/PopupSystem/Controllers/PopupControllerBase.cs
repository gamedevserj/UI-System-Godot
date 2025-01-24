using System;
using UISystem.Core.Elements.Interfaces;
using UISystem.Core.Extensions;
using UISystem.Core.MenuSystem.Interfaces;
using UISystem.Core.PhysicalInput;
using UISystem.Core.PopupSystem.Interfaces;
using UISystem.Core.PopupSystem.Views;
using UISystem.Core.Transitions.Interfaces;

namespace UISystem.Core.PopupSystem.Controllers;
internal abstract class PopupControllerBase<TPrefab, TView, TParent, TInputEvent> : IPopupController<TInputEvent> where TView : PopupView
{

    protected const float fadeDuration = 0.25f;

    protected TView _view;
    protected IFocusableControl _defaultSelectedElement;
    protected Action<int> _onHideAction;
    private IMenuController<TInputEvent> _caller;

    protected readonly TPrefab _prefab;
    protected readonly IPopupsManager<TInputEvent> _popupsManager;
    protected readonly TParent _parent;

    public abstract int Type { get; }
    public abstract int PressedReturnPopupResult { get; }
    public bool CanReceivePhysicalInput { get; private set; } = true; // to prevent input processing during transitions
    protected abstract bool IsViewValid { get; }

    public PopupControllerBase(TPrefab prefab, IPopupsManager<TInputEvent> popupsManager, TParent parent)
    {
        _prefab = prefab;
        _popupsManager = popupsManager;
        _parent = parent;
    }

    public virtual void Init()
    {
        if (!IsViewValid)
        {
            CreateView(_parent);
        }
    }

    public void Show(IMenuController<TInputEvent> caller, string message, Action<int> onHideAction, bool instant = false)
    {
        CanReceivePhysicalInput = false;
        _caller = caller;
        _caller.CanReturnToPreviousMenu = false;
        _view.Message.Text = message;
        _onHideAction = onHideAction;
        _view.Show((() =>
        {
            CanReceivePhysicalInput = true;
            FocusElement();
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

    private void DestroyView() => _view.SafeQueueFree();

    protected abstract void FocusElement();
    protected abstract void CreateView(TParent menuParent);
    protected abstract void SetupElements();
    protected abstract IViewTransition CreateTransition();

}
