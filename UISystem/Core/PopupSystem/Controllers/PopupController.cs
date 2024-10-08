using Godot;
using System;
using UISystem.Common.Constants;
using UISystem.Core.Common.Interfaces;
using UISystem.Core.Extensions;
using UISystem.Core.MenuSystem.Interfaces;
using UISystem.Core.PopupSystem.Enums;
using UISystem.Core.PopupSystem.Interfaces;
using UISystem.Core.PopupSystem.Views;

namespace UISystem.Core.PopupSystem.Controllers;
public abstract class PopupController<T> : IPopupController where T : PopupView
{

    protected const float fadeDuration = 0.25f;

    protected T _view;
    protected IFocusableControl _defaultSelectedElement;
    protected Action<PopupResult> _onHideAction;
    private IMenuController _caller;

    protected readonly string _prefab;
    protected readonly PopupsManager _popupsManager;
    protected readonly SceneTree _sceneTree;

    public abstract PopupType PopupType { get; }
    public abstract PopupResult PressedReturnPopupResult { get; }

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

    public void HandleInputPressedWhenActive(InputEvent key)
    {
        if (key.IsActionPressed(InputsData.ReturnToPreviousMenu))
            _popupsManager.HidePopup(PressedReturnPopupResult);
    }

    public void Show(IMenuController caller, string message, Action<PopupResult> onHideAction, bool instant = false)
    {
        _caller = caller;
        _caller.CanReturnToPreviousMenu = false;
        _view.Message.Text = message;
        _onHideAction = onHideAction;
        SwitchFocusAvailability(false);
        _view.Show(() =>
        {
            SwitchFocusAvailability(true);
            if (_defaultSelectedElement?.IsValidElement() == true)
            {
                _defaultSelectedElement.SwitchFocus(true);
            }
        }, instant);
    }

    public void Hide(PopupResult result, bool instant = false)
    {
        SwitchFocusAvailability(false);
        _view.Hide(() =>
        {
            _caller.CanReturnToPreviousMenu = true;
            _onHideAction?.Invoke(result);
            DestroyView();
        }, instant);
    }

    private void SwitchFocusAvailability(bool enable)
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
