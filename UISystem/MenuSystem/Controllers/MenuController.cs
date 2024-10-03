﻿using Godot;
using GodotExtensions;
using System;
using UISystem.Common.Constants;
using UISystem.Common.Interfaces;
using UISystem.MenuSystem.Enums;
using UISystem.MenuSystem.Interfaces;
using UISystem.MenuSystem.Views;

namespace UISystem.MenuSystem.Controllers;
public abstract class MenuController<TView, TModel> : IMenuController where TView : MenuView where TModel : IMenuModel
{

    protected TView _view;
    protected TModel _model;

    protected IFocusableControl _lastSelectedElement;
    private IFocusableControl _defaultSelectedElement;

    protected readonly string _prefab;
    protected readonly MenusManager _menusManager;

    public virtual bool CanReturnToPreviousMenu { get; set; } = true; // when you want to temporarly disable retuning to previous menu, i.e. when player is rebinding keys
    public abstract MenuType MenuType { get; }
    protected IFocusableControl DefaultSelectedElement
    {
        get => _defaultSelectedElement;
        set => _defaultSelectedElement = _lastSelectedElement = value;
    }

    public MenuController(string prefab, TModel model, MenusManager menusManager)
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
        SwitchFocusAvailability(false);
        _view.Show(() =>
        {
            onComplete?.Invoke();
            SwitchFocusAvailability(true);
        }, instant);
    }

    public virtual void Hide(MenuStackBehaviourEnum stackBehaviour, Action onComplete = null, bool instant = false)
    {
        SwitchFocusAvailability(false);
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

    protected virtual void CreateView(Node menuParent)
    {
        PackedScene loadedPrefab = ResourceLoader.Load<PackedScene>(_prefab);
        _view = loadedPrefab.Instantiate() as TView;
        _view.Init();
        SetupElements();
        menuParent.AddChild(_view);
    }

    protected virtual void OnReturnToPreviousMenuButtonDown()
    {
        if (CanReturnToPreviousMenu)
            _menusManager.ReturnToPreviousMenu();
    }

    protected virtual void SwitchFocusAvailability(bool enable)
    {
        _view.SwitchFocusAwailability(enable);
        if (!enable) return;

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
