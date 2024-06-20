using Godot;
using GodotExtensions;
using System;
using UISystem.Constants;
using UISystem.PopupSystem.Enums;
using UISystem.PopupSystem.Interfaces;
using UISystem.PopupSystem.Views;

namespace UISystem.PopupSystem.Controllers;
public abstract class PopupController<T> : IPopupController where T : PopupView
{

    protected const float fadeDuration = 0.25f;

    protected T _view;
    protected Control _defaultSelectedElement;
    protected Action<PopupResult> _onHideAction;

    protected readonly string _prefab;
    protected readonly PopupsManager _popupsManager;
    protected readonly SceneTree _sceneTree;

    public abstract PopupType PopupType { get; }

    public PopupController(string prefab, PopupsManager popupsManager, SceneTree sceneTree)
    {
        _prefab = prefab;
        _popupsManager = popupsManager;
        _sceneTree = sceneTree;
    }

    public virtual void Init(Node popupParent)
    {
        if (!_view.IsValid())
        {
            CreateView(popupParent);
        }
        _defaultSelectedElement = _view.DefaultSelectedElement;
    }

    public void Show(string message, Action<PopupResult> onHideAction)
    {
        _view.Message.Text = message;
        _onHideAction = onHideAction;
        SwitchFocusAvailability(false);

        Tween tween = _sceneTree.CreateTween();
        tween.SetPauseMode(Tween.TweenPauseMode.Process);
        tween.TweenProperty(_view, PropertyConstants.Modulate, new Color(_view.Modulate, 1), fadeDuration);
        tween.TweenCallback(Callable.From(() =>
        {
            SwitchFocusAvailability(true);
            if (_defaultSelectedElement.IsValid())
            {
                _defaultSelectedElement.GrabFocus();
            }
        }));
    }

    public void Hide(PopupResult result)
    {
        SwitchFocusAvailability(false);
        Tween tween = _sceneTree.CreateTween();
        tween.SetPauseMode(Tween.TweenPauseMode.Process);
        tween.TweenProperty(_view, PropertyConstants.Modulate, new Color(_view.Modulate, 0), fadeDuration);
        tween.TweenCallback(Callable.From(() =>
        {
            _onHideAction?.Invoke(result);
            DestroyView();
        }));
    }

    protected virtual void SwitchFocusAvailability(bool enable)
    {
        _view.SwitchFocusAwailability(enable);
    }

    private void CreateView(Node menuParent)
    {
        PackedScene obj = ResourceLoader.Load<PackedScene>(_prefab);
        _view = obj.Instantiate() as T;
        _view.Init();
        menuParent.AddChild(_view);
        _view.Modulate = new Color(_view.Modulate.R, _view.Modulate.G, _view.Modulate.B, 0);
    }

    private void DestroyView()
    {
        _view.SafeQueueFree();
    }
    
}
