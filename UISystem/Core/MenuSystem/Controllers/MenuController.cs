using Godot;
using System;
using UISystem.Core.Constants;
using UISystem.Core.Elements.Interfaces;
using UISystem.Core.Extensions;
using UISystem.Core.MenuSystem.Interfaces;
using UISystem.Core.Views;

namespace UISystem.Core.MenuSystem.Controllers;
internal abstract class MenuController<TView, TModel> : IMenuController where TView : BaseWindowView where TModel : IMenuModel
{

    protected TView _view;
    protected TModel _model;

    protected IFocusableControl _lastSelectedElement;
    private IFocusableControl _defaultSelectedElement;

    protected readonly string _prefab;
    protected readonly IMenusManager _menusManager;

    public virtual bool CanReturnToPreviousMenu { get; set; } = true; // when you want to temporarly disable retuning to previous menu, i.e. when player is rebinding keys
    public abstract int Type { get; }
    protected IFocusableControl DefaultSelectedElement
    {
        get => _defaultSelectedElement;
        set => _defaultSelectedElement = _lastSelectedElement = value;
    }

    public MenuController(string prefab, TModel model, IMenusManager menusManager)
    {
        _prefab = prefab;
        _model = model;
        _menusManager = menusManager;
    }

    public void Init(Node menuParent)
    {
        if (!_view.IsValid())
        {
            CreateView(menuParent);
        }
    }

    public virtual void Show(Action onComplete = null, bool instant = false)
    {
        _view.Show(() =>
        {
            onComplete?.Invoke();
            FocusElement();
        }, instant);
    }

    public virtual void Hide(int menuChangeType, Action onComplete = null, bool instant = false)
    {
        _view.Hide(() => onComplete?.Invoke(), instant);
    }

    public virtual void HandleInputPressedWhenActive(InputEvent key)
    {
        if (key.IsActionPressed(InputsData.ReturnToPreviousMenu) && CanReturnToPreviousMenu)
            OnReturnToPreviousMenuButtonDown();
    }

    public void DestroyView()
    {
        _view.SafeQueueFree();
    }

    // when showing popups
    protected void SwitchFocusAvailability(bool enable)
    {
        _view.SwitchFocusAwailability(enable);
        if (enable)
            FocusElement();
    }

    protected virtual void OnReturnToPreviousMenuButtonDown(Action onComplete = null, bool instant = false)
    {
        if (CanReturnToPreviousMenu)
            _menusManager.ReturnToPreviousMenu(onComplete, instant);
    }

    private void CreateView(Node menuParent)
    {
        PackedScene loadedPrefab = ResourceLoader.Load<PackedScene>(_prefab);
        _view = loadedPrefab.Instantiate() as TView;
        _view.Init();
        SetupElements();
        menuParent.AddChild(_view);
    }

    private void FocusElement()
    {
        if (_lastSelectedElement?.IsValidElement() == true)
        {
            _lastSelectedElement.SwitchFocus(true);
        }
        else if (_defaultSelectedElement?.IsValidElement() == true)
        {
            _defaultSelectedElement.SwitchFocus(true);
        }
    }

    protected abstract void SetupElements();
}
