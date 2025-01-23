using Godot;
using System;
using UISystem.Core.Constants;
using UISystem.Core.Elements.Interfaces;
using UISystem.Core.Extensions;
using UISystem.Core.MenuSystem.Interfaces;
using UISystem.Core.PopupSystem.Interfaces;
using UISystem.Core.PopupSystem.Views;
using UISystem.Core.Transitions.Interfaces;

namespace UISystem.Core.PopupSystem.Controllers;
internal abstract class PopupController<T> : IPopupController where T : PopupView
{

    protected const float fadeDuration = 0.25f;

    protected T _view;
    protected IFocusableControl _defaultSelectedElement;
    protected Action<int> _onHideAction;
    private IMenuController _caller;
    private bool _canProcessInput = true; // to prevent input processing during transitions

    protected readonly string _prefab;
    protected readonly IPopupsManager _popupsManager;
    protected readonly SceneTree _sceneTree;
    protected readonly Node _parent;

    public abstract int Type { get; }
    public abstract int PressedReturnPopupResult { get; }

    public PopupController(string prefab, IPopupsManager popupsManager, Node parent, SceneTree sceneTree)
    {
        _prefab = prefab;
        _popupsManager = popupsManager;
        _sceneTree = sceneTree;
        _parent = parent;
    }

    public virtual void Init()
    {
        if (!_view.IsValid())
        {
            CreateView(_parent);
        }
        _defaultSelectedElement = _view.DefaultSelectedElement;
    }

    public void HandleInputPressedWhenActive(InputEvent key)
    {
        if (!_canProcessInput) return;

        if (key.IsActionPressed(InputsData.ReturnToPreviousMenu))
            _popupsManager.HidePopup(PressedReturnPopupResult);
    }

    public void Show(IMenuController caller, string message, Action<int> onHideAction, bool instant = false)
    {
        _canProcessInput = false;
        _caller = caller;
        _caller.CanReturnToPreviousMenu = false;
        _view.Message.Text = message;
        _onHideAction = onHideAction;
        _view.Show(() =>
        {
            _canProcessInput = true;
            if (_defaultSelectedElement?.IsValidElement() == true)
            {
                _defaultSelectedElement.SwitchFocus(true);
            }
        }, instant);
    }

    public void Hide(int result, bool instant = false)
    {
        _canProcessInput = false;
        _view.Hide(() =>
        {
            _canProcessInput = true;
            _caller.CanReturnToPreviousMenu = true;
            _onHideAction?.Invoke(result);
            DestroyView();
        }, instant);
    }

    private void CreateView(Node parent)
    {
        PackedScene obj = ResourceLoader.Load<PackedScene>(_prefab);
        _view = obj.Instantiate() as T;
        _view.Init(CreateTransition());
        SetupElements();
        parent.AddChild(_view);
    }

    private void DestroyView()
    {
        _view.SafeQueueFree();
    }

    protected abstract void SetupElements();
    protected abstract IViewTransition CreateTransition();

}
