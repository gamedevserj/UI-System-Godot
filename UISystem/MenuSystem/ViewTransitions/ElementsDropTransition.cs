using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UISystem.Constants;
using UISystem.MenuSystem.Views;
using UISystem.MenuSystem.Interfaces;
using VisibilityManger = UISystem.Common.Helpers.CanvasItemVisibilityManager;
using UISystem.Common.Helpers;
using UISystem.Extensions;

namespace UISystem.MenuSystem.ViewTransitions;
public class ElementsDropTransition : IViewTransition
{

    private Vector2 _primaryElementSize;
    private bool _initializedButtonParameters;
    private Dictionary<Control, Vector2> _secondaryElementsPositions = new();

    private readonly MenuView _view;
    private readonly Control _fadeObjectsContainer;
    private readonly Control _primaryElement;
    private readonly Control[] _secondaryElements;
    private readonly float _animationDuration;

    private SceneTree _sceneTree;
    private SceneTree SceneTree
    {
        get
        {
            _sceneTree ??= _view.GetTree();
            return _sceneTree;
        }
    }

    public ElementsDropTransition(MenuView view, Control fadeObjectsContainer, Control primaryElement,
        Control[] secondaryElements, float animationDuration)
    {
        _view = view;
        _fadeObjectsContainer = fadeObjectsContainer;
        _primaryElement = primaryElement;
        _secondaryElements = secondaryElements;
        _animationDuration = animationDuration;
    }

    public void Hide(Action onHidden, bool instant)
    {
        if (instant)
        {
            VisibilityManger.HideItem(_fadeObjectsContainer);
            onHidden?.Invoke();
            return;
        }

        Tween tween = SceneTree.CreateTween();
        tween.SetPauseMode(Tween.TweenPauseMode.Process);

        tween.SetEase(Tween.EaseType.In);
        tween.SetTrans(Tween.TransitionType.Back);
        for (int i = 0; i < _secondaryElements.Length; i++)
        {
            tween.Parallel().TweenProperty(_secondaryElements[i], PropertyConstants.Position, Vector2.Zero, _animationDuration);
        }
        tween.TweenCallback(Callable.From(() => { SwitchSecondaryButtonsVisibility(false); }));

        Vector2 size = new(0, _primaryElement.Size.Y);
        float duration = _animationDuration * 0.5f;
        tween.SetTrans(Tween.TransitionType.Linear);
        tween.Parallel().TweenProperty(_primaryElement, PropertyConstants.Modulate, new Color(_primaryElement.Modulate, 0), duration);
        tween.SetEase(Tween.EaseType.Out);
        tween.SetTrans(Tween.TransitionType.Quad);
        //tween.Parallel().TweenProperty(_primaryElement, PropertyConstants.Size, size, duration);
        tween.TweenControlSize(true, _primaryElement, size, duration);
        tween.Parallel().TweenProperty(_fadeObjectsContainer, PropertyConstants.Modulate, new Color(_fadeObjectsContainer.Modulate, 0), duration).SetDelay(duration);
        tween.TweenCallback(Callable.From(() =>
        {
            onHidden?.Invoke();
        }));
    }

    public async void Show(Action onShown, bool instant)
    {
        VisibilityManger.HideItem(_primaryElement);
        VisibilityManger.HideItem(_fadeObjectsContainer);
        SwitchSecondaryButtonsVisibility(false);

        if (!_initializedButtonParameters)
            await InitElementParameters();

        if (instant)
        {
            VisibilityManger.ShowItem(_primaryElement);
            VisibilityManger.ShowItem(_fadeObjectsContainer);
            SwitchSecondaryButtonsVisibility(true);
            onShown?.Invoke();
            return;
        }
        
        _primaryElement.Size = new(0, _primaryElement.Size.Y);
        VisibilityManger.ShowItem(_primaryElement);
        for (int i = 0; i < _secondaryElements.Length; i++)
        {
            _secondaryElements[i].Position = Vector2.Zero;
        }

        Tween tween = SceneTree.CreateTween();
        tween.SetPauseMode(Tween.TweenPauseMode.Process);
        tween.Parallel().TweenProperty(_fadeObjectsContainer, PropertyConstants.Modulate, new Color(_fadeObjectsContainer.Modulate, 1), 0.1f);

        tween.SetEase(Tween.EaseType.Out);
        tween.SetTrans(Tween.TransitionType.Quad);
        //tween.Parallel().TweenProperty(_primaryElement, PropertyConstants.Size, _primaryElementSize, _animationDuration * 0.5f);
        tween.TweenControlSize(true, _primaryElement, _primaryElementSize, _animationDuration * 0.5f);
        tween.TweenCallback(Callable.From(() => { SwitchSecondaryButtonsVisibility(true); }));

        tween.SetTrans(Tween.TransitionType.Back);
        for (int i = 0; i < _secondaryElements.Length; i++)
        {
            tween.Parallel().TweenProperty(_secondaryElements[i], PropertyConstants.Position,
                _secondaryElementsPositions[_secondaryElements[i]], _animationDuration);
        }
        tween.TweenCallback(Callable.From(() => { onShown?.Invoke(); }));
    }

    private async Task InitElementParameters()
    {
        await _view.ToSignal(RenderingServer.Singleton, RenderingServerInstance.SignalName.FramePostDraw);
        _primaryElementSize = _primaryElement.Size;
        for (int i = 0; i < _secondaryElements.Length; i++)
        {
            _secondaryElementsPositions.Add(_secondaryElements[i], _secondaryElements[i].Position);
        }
        SetButtonsOrdering();
        _initializedButtonParameters = true;
    }

    private void SetButtonsOrdering()
    {
        List<Control> buttonsByPosition = _secondaryElements.OrderByDescending(o => o.Position.Y).ToList();
        int last = 0;
        for (int i = 0; i < buttonsByPosition.Count; i++)
        {
            buttonsByPosition[i].ZIndex = i;
            last = i;
        }
        _primaryElement.ZIndex = last + 1;
    }

    private void SwitchSecondaryButtonsVisibility(bool show)
    {
        for (int i = 0; i < _secondaryElements.Length; i++)
        {
            if (show)
                VisibilityManger.ShowItem(_secondaryElements[i]);
            else
                VisibilityManger.HideItem(_secondaryElements[i]);
        }
    }

}
