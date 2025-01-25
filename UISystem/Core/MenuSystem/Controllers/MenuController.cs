using System;
using UISystem.Core.MenuSystem.Enums;
using UISystem.Core.MenuSystem.Interfaces;
using UISystem.Core.Views.Interfaces;

namespace UISystem.Core.MenuSystem.Controllers;
internal abstract class MenuController<TViewHandler, TView, TModel, TInputEvent, TInteractableElement> : IMenuController<TInputEvent>
    where TViewHandler : IViewHandler<TView>
    where TView : IMenuView<TInteractableElement>
    where TModel : IMenuModel
{

    protected TViewHandler _viewHandler;
    protected TView _view;
    protected TModel _model;

    protected readonly IMenusManager<TInputEvent> _menusManager;

    public virtual bool CanReturnToPreviousMenu { get; set; } = true; // when you want to temporarly disable retuning to previous menu, i.e. when player is rebinding keys
    public abstract int Type { get; }
    public bool CanReceivePhysicalInput { get; private set; } // to prevent input processing during transitions

    public MenuController(TViewHandler viewHandler, TModel model, IMenusManager<TInputEvent> menusManager)
    {
        _viewHandler = viewHandler;
        _model = model;
        _menusManager = menusManager;
    }

    public void Init()
    {
        if (!_viewHandler.IsViewValid)
        {
            _view = _viewHandler.CreateView();
            SetupElements();
        }
    }

    public virtual void Show(Action onComplete = null, bool instant = false)
    {
        CanReceivePhysicalInput = false;
        _view.Show(() =>
        {
            onComplete?.Invoke();
            _view.FocusElement();
            CanReceivePhysicalInput = true;
        }, instant);
    }

    public virtual void Hide(StackingType stackingType, Action onComplete = null, bool instant = false) 
    {
        CanReceivePhysicalInput = false;
        _view.Hide(() =>
        {
            onComplete?.Invoke();
            CanReceivePhysicalInput = true;
        }, instant);
    }

    public void DestroyView() => _view.DestroyView();

    // when showing popups
    protected void SwitchFocusAvailability(bool enable)
    {
        _view.SwitchFocusAvailability(enable);
        CanReceivePhysicalInput = enable;
        if (enable)
            _view.FocusElement();
    }

    public virtual void OnCancelButtonDown()
    {
        if (CanReturnToPreviousMenu)
            _menusManager.ReturnToPreviousMenu();
    }

    public virtual void OnPauseButtonDown() // for in-game menu
    { }
    public virtual void OnResumeButtonDown() // for pause menu
    { }
    public virtual void OnAnyButtonDown(TInputEvent inputEvent) // for rebind menu
    { }

    protected abstract void SetupElements();
}
