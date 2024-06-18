using Godot;
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

    protected const float TransitionDuration = 0.25f;

    protected TView _view;
    protected TModel _model;
    protected IFocusableControl _defaultSelectedElement;
    protected IFocusableControl _lastSelectedElement;

    protected readonly string _prefab;
    protected readonly MenusManager _menusManager;
    protected readonly SceneTree _sceneTree;

    public virtual bool CanReturnToPreviousMenu { get; set; } = true; // when you want to temporarly disable retuning to previous menu, i.e. when player is rebinding keys
    public abstract MenuType MenuType { get; }

    public MenuController(string prefab, TModel model, MenusManager menusManager, SceneTree sceneTree)
    {
        _prefab = prefab;
        _model = model;
        _menusManager = menusManager;
        _sceneTree = sceneTree;
    }

    public void Init(Node menuParent)
    {
        if (!_view.IsValid())
        {
            CreateView(menuParent);
        }
    }

    public abstract void Show(Action onComplete = null, bool instant = false);

    public abstract void Hide(MenuStackBehaviourEnum stackBehaviour, Action onComplete = null, bool instant = false);


    public virtual void HandleInputPressedWhenActive(InputEvent key)
    {
        if (key.IsActionPressed(InputsData.ReturnToPreviousMenu))
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
        menuParent.AddChild(_view);
    }

    protected virtual void OnReturnToPreviousMenuButtonDown()
    {
        _menusManager.ReturnToPreviousMenu();
    }

    protected virtual void SwitchFocusAvailability(bool enable)
    {
        _view.SwitchFocusAwailability(enable);

        if (enable && IsElementValid(_lastSelectedElement))
            _lastSelectedElement.SwitchFocus(true);
    }

    protected float GetDuration(bool instant)
    {
        return instant ? 0 : TransitionDuration;
    }

    protected bool IsElementValid(IFocusableControl element)
    {
        return element != null && element.IsValidElement();
    }
}
