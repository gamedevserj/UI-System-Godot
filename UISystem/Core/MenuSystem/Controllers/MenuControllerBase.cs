using System;
using UISystem.Core.PhysicalInput;
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
    protected readonly IInputProcessor<TInputEvent> _inputProcessor;

    public virtual bool CanReturnToPreviousMenu { get; set; } = true; // when you want to temporarly disable retuning to previous menu, i.e. when player is rebinding keys
    public abstract int Type { get; }
    public bool CanProcessInput { get; private set; } = true;// to prevent input processing during transitions
    protected abstract bool IsViewValid { get; }
    protected TFocusableElement DefaultSelectedElement
    {
        get => _defaultSelectedElement;
        set => _defaultSelectedElement = _lastSelectedElement = value;
    }

    public MenuControllerBase(TPrefab prefab, TModel model, IMenusManager<TInputEvent> menusManager, TParent parent, IInputProcessor<TInputEvent> inputProcessor)
    {
        _prefab = prefab;
        _model = model;
        _menusManager = menusManager;
        _parent = parent;
        _inputProcessor = inputProcessor;
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
        CanProcessInput = false;
        _view.Show(() =>
        {
            onComplete?.Invoke();
            FocusElement();
            CanProcessInput = true;
        }, instant);
    }

    public virtual void Hide(StackingType stackingType, Action onComplete = null, bool instant = false) 
    {
        CanProcessInput = false;
        _view.Hide(() =>
        {
            onComplete?.Invoke();
            CanProcessInput = true;
        }, instant);
    }

    public void DestroyView() => _view.DestroyView();

    public virtual void ProcessInput(TInputEvent key)
    {
        if (_inputProcessor.IsPressingReturnToPreviousMenuButton(key))
            OnReturnToPreviousMenuButtonDown();
    }

    // when showing popups
    protected void SwitchFocusAvailability(bool enable)
    {
        _view.SwitchFocusAvailability(enable);
        if (enable)
            FocusElement();
    }

    protected virtual void OnReturnToPreviousMenuButtonDown(Action onComplete = null, bool instant = false)
    {
        if (CanReturnToPreviousMenu)
            _menusManager.ReturnToPreviousMenu(onComplete, instant);
    }

    protected abstract void FocusElement();
    protected abstract void CreateView(TParent menuParent);
    protected abstract void SetupElements();
    protected abstract IViewTransition CreateTransition();
}
