﻿using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UISystem.Common.Elements;
using UISystem.Constants;
using UISystem.Extensions;
using UISystem.Common.Transitions.Interfaces;
using VisibilityManger = UISystem.Common.Helpers.CanvasItemVisibilityManager;

namespace UISystem.Common.Transitions;
public class MainElementDropTransition : IViewTransition
{

    private const float FadeDuration = 0.1f;

    private Vector2 _mainElementSize;
    private bool _initializedParameters;
    private Dictionary<Control, Vector2> _secondaryElementsPositions = new();
    private SceneTree _sceneTree;

    private readonly Control _caller;
    private readonly Control _fadeObjectsContainer;
    private readonly Control _mainElement;
    private readonly Control[] _secondaryElements;
    private readonly float _mainElementDuration;
    private readonly float _secondaryElementDuration;

    private SceneTree SceneTree
    {
        get
        {
            _sceneTree ??= _caller.GetTree();
            return _sceneTree;
        }
    }

    public MainElementDropTransition(Control caller, Control fadeObjectsContainer, Control mainResizableControl,
        Control[] secondaryElements, float mainElementDuration, float secondaryElementDuration)
    {
        _caller = caller;
        _fadeObjectsContainer = fadeObjectsContainer;
        _mainElement = mainResizableControl;
        _secondaryElements = secondaryElements;
        _mainElementDuration = mainElementDuration;
        _secondaryElementDuration = secondaryElementDuration;
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
            tween.TweenNode2DPosition(true, _secondaryElements[i], Vector2.Zero, _secondaryElementDuration);
        }
        tween.TweenCallback(Callable.From(() => { SwitchSecondaryButtonsVisibility(false); }));

        Vector2 size = new(0, _mainElementSize.Y);
        tween.SetEase(Tween.EaseType.In);
        tween.SetTrans(Tween.TransitionType.Quad);
        tween.TweenControlSize(false, _mainElement, size, _mainElementDuration);
        tween.TweenCallback(Callable.From(() =>
        {
            VisibilityManger.HideItem(_mainElement);
        }));

        tween.SetTrans(Tween.TransitionType.Linear);
        tween.TweenCanvasItemAlpha(false, _fadeObjectsContainer, 0, FadeDuration);
        tween.TweenCallback(Callable.From(() =>
        {
            onHidden?.Invoke();
        }));
    }

    public async void Show(Action onShown, bool instant)
    {
        if (instant)
        {
            VisibilityManger.ShowItem(_mainElement);
            VisibilityManger.ShowItem(_fadeObjectsContainer);
            SwitchSecondaryButtonsVisibility(true);
            onShown?.Invoke();
            return;
        }

        VisibilityManger.HideItem(_mainElement);
        VisibilityManger.HideItem(_fadeObjectsContainer);
        SwitchSecondaryButtonsVisibility(false);

        if (!_initializedParameters)
            await InitElementParameters();

        _mainElement.Size = new(0, _mainElementSize.Y);
        VisibilityManger.ShowItem(_mainElement);
        for (int i = 0; i < _secondaryElements.Length; i++)
        {
            _secondaryElements[i].Position = Vector2.Zero;
        }

        Tween tween = SceneTree.CreateTween();
        tween.SetPauseMode(Tween.TweenPauseMode.Process);

        tween.SetTrans(Tween.TransitionType.Linear);
        tween.TweenCanvasItemAlpha(false, _fadeObjectsContainer, 1, FadeDuration);

        tween.SetEase(Tween.EaseType.Out);
        tween.SetTrans(Tween.TransitionType.Quad);
        tween.TweenControlSize(true, _mainElement, _mainElementSize, _mainElementDuration);
        tween.TweenCallback(Callable.From(() => { SwitchSecondaryButtonsVisibility(true); }));

        tween.SetTrans(Tween.TransitionType.Back);
        for (int i = 0; i < _secondaryElements.Length; i++)
        {
            tween.TweenNode2DPosition(true, _secondaryElements[i], _secondaryElementsPositions[_secondaryElements[i]], _secondaryElementDuration);
        }
        tween.TweenCallback(Callable.From(() => { onShown?.Invoke(); }));
    }

    private async Task InitElementParameters()
    {
        await _caller.ToSignal(RenderingServer.Singleton, RenderingServerInstance.SignalName.FramePostDraw);
        _mainElementSize = _mainElement.Size;
        for (int i = 0; i < _secondaryElements.Length; i++)
        {
            _secondaryElementsPositions.Add(_secondaryElements[i], _secondaryElements[i].Position);
        }
        SetButtonsOrdering();
        _initializedParameters = true;
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
        _mainElement.ZIndex = last + 1;
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
