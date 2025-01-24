using System;
using UISystem.Core.MenuSystem.Enums;
using UISystem.Core.MenuSystem.Interfaces;
using UISystem.Core.Transitions.Interfaces;
using UISystem.Core.Views.Interfaces;

namespace UISystem.Core.MenuSystem.Controllers;
internal abstract class MenuControllerBase<TPrefab, TView, TModel, TParent, TFocusableElement, TInputEvent> : IMenuController<TInputEvent>
    where TView : IView 
    where TModel : IMenuModel
{

    protected TView _view;
    protected TModel _model;

    protected TFocusableElement _lastSelectedElement;
    protected TFocusableElement _defaultSelectedElement;

    protected readonly TPrefab _prefab;
    protected readonly IMenusManager<TInputEvent> _menusManager;
    protected readonly TParent _parent;

    public virtual bool CanReturnToPreviousMenu { get; set; } = true; // when you want to temporarly disable retuning to previous menu, i.e. when player is rebinding keys
    public abstract int Type { get; }
    public bool CanReceivePhysicalInput { get; private set; } = true;// to prevent input processing during transitions
    protected abstract bool IsViewValid { get; }
    protected TFocusableElement DefaultSelectedElement
    {
        get => _defaultSelectedElement;
        set => _defaultSelectedElement = _lastSelectedElement = value;
    }

    public MenuControllerBase(TPrefab prefab, TModel model, IMenusManager<TInputEvent> menusManager, TParent parent)
    {
        _prefab = prefab;
        _model = model;
        _menusManager = menusManager;
        _parent = parent;
    }

    public void Init()
    {
        if (!IsViewValid)
        {
            CreateView(_parent);
        }
    }

    public virtual void Show(Action onComplete = null, bool instant = false)
    {
        CanReceivePhysicalInput = false;
        _view.Show(() =>
        {
            onComplete?.Invoke();
            FocusElement();
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
            FocusElement();
    }

    public virtual void OnCancelButtonDown(Action onComplete = null, bool instant = false)
    {
        if (CanReturnToPreviousMenu)
            _menusManager.ReturnToPreviousMenu(onComplete, instant);
    }

    public virtual void OnPauseButtonDown() // for in-game menu
    { }
    public virtual void OnResumeButtonDown() // for pause menu
    { }
    public virtual void OnAnyButtonDown(TInputEvent inputEvent) // for rebind menu
    { }

    protected abstract void FocusElement();
    protected abstract void CreateView(TParent menuParent);
    protected abstract void SetupElements();
    protected abstract IViewTransition CreateTransition();
}
