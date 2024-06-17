using Godot;
using System;
using UISystem.Constants;
using UISystem.MenuSystem.Enums;
using UISystem.MenuSystem.Interfaces;
using UISystem.MenuSystem.Views;

namespace UISystem.MenuSystem.Controllers;
public abstract class MenuControllerFade<TView, TModel> : MenuController<TView, TModel>, IMenuController where TView : MenuView where TModel : IMenuModel
{

    protected MenuControllerFade(string prefab, TModel model, MenusManager menusManager, SceneTree sceneTree) : base(prefab, model, menusManager, sceneTree)
    {
    }

    public override void Show(Action onComplete = null, bool instant = false)
    {
        SwitchFocusAvailability(false);
        Tween tween = _sceneTree.CreateTween();
        tween.SetPauseMode(Tween.TweenPauseMode.Process);
        Color originalColor = _view.FadeObjectsContainer.Modulate;
        tween.TweenProperty(_view.FadeObjectsContainer, PropertyConstants.Modulate, new Color(originalColor, 1), GetDuration(instant));
        tween.TweenCallback(Callable.From(() =>
        {
            onComplete?.Invoke();
            SwitchFocusAvailability(true);
            if (IsElementValid(_lastSelectedElement))
            {
                _lastSelectedElement.FocusElement();
            }
            else if (IsElementValid(_defaultSelectedElement))
            {
                _defaultSelectedElement.FocusElement();
            }
        }));
    }

    public override void Hide(MenuStackBehaviourEnum stackBehaviour, Action onComplete = null, bool instant = false)
    {
        SwitchFocusAvailability(false);
        Tween tween = _sceneTree.CreateTween();
        tween.SetPauseMode(Tween.TweenPauseMode.Process);
        tween.TweenProperty(_view.FadeObjectsContainer, PropertyConstants.Modulate, new Color(_view.FadeObjectsContainer.Modulate, 0), GetDuration(instant));
        tween.TweenCallback(Callable.From(() =>
        {
            onComplete?.Invoke();
        }));
    }


    protected override void CreateView(Node menuParent)
    {
        base.CreateView(menuParent);
        _view.FadeObjectsContainer.Modulate = new Color(_view.Modulate.R, _view.Modulate.G, _view.Modulate.B, 0);
    }

}
