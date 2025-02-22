﻿using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UISystem.Core.Transitions;
using UISystem.Extensions;
using UISystem.Transitions.Interfaces;

namespace UISystem.Transitions;
public class MainElementDropTransition : IViewTransition
{

    private const float FadeDuration = 0.1f;
    private const float MainElementAnimationDuration = 0.2f;
    private const float SecondaryElementAnimationDuration = 0.1f;

    private Vector2 _mainElementSize;
    private bool _initializedParameters;
    private SceneTree _sceneTree;

    private readonly Control _caller;
    private readonly Control _fadeObjectsContainer;
    private readonly ITweenableMenuElement _mainElement;
    private readonly ITweenableMenuElement[] _secondaryElements;
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

    public MainElementDropTransition(Control caller, Control fadeObjectsContainer, ITweenableMenuElement mainResizableControl,
        ITweenableMenuElement[] secondaryElements, float mainElementDuration = MainElementAnimationDuration, 
        float secondaryElementDuration = SecondaryElementAnimationDuration)
    {
        _caller = caller;
        _fadeObjectsContainer = fadeObjectsContainer;
        _mainElement = mainResizableControl;
        _secondaryElements = secondaryElements;
        _mainElementDuration = mainElementDuration;
        _secondaryElementDuration = secondaryElementDuration;
    }

    public async void Hide(Action onHidden, bool instant)
    {
        if (instant)
        {
            _fadeObjectsContainer.HideItem();
            onHidden?.Invoke();
            return;
        }

        var tasks = new Task[_secondaryElements.Length + 1];
        for (int i = 0; i < _secondaryElements.Length; i++)
        {
            tasks[i] = _secondaryElements[i].ResetHover();
        }
        tasks[_secondaryElements.Length] = _mainElement.ResetHover();
        await Task.WhenAll(tasks);

        Tween tween = SceneTree.CreateTween();
        tween.SetPauseMode(Tween.TweenPauseMode.Process);

        tween.SetEase(Tween.EaseType.In);
        tween.SetTrans(Tween.TransitionType.Back);
        for (int i = 0; i < _secondaryElements.Length; i++)
        {
            tween.Parallel().TweenControlGlobalPosition(_secondaryElements[i].ResizableControl, _mainElement.ResizableControl.GlobalPosition, _secondaryElementDuration);
        }
        tween.TweenCallback(Callable.From(() => { SwitchSecondaryButtonsVisibility(false); }));

        Vector2 size = new(0, _mainElementSize.Y);
        tween.SetEase(Tween.EaseType.In);
        tween.SetTrans(Tween.TransitionType.Quad);
        tween.TweenControlSize(_mainElement.ResizableControl, size, _mainElementDuration);

        tween.SetTrans(Tween.TransitionType.Linear);
        tween.TweenAlpha(_fadeObjectsContainer, 0, FadeDuration);
        tween.Finished += () => onHidden?.Invoke();
    }

    public async void Show(Action onShown, bool instant)
    {
        // should always hide before showing because awaiting for parameters shows menu for a split second
        _mainElement.ResizableControl.HideItem();
        _fadeObjectsContainer.HideItem();
        SwitchSecondaryButtonsVisibility(false);

        if (!_initializedParameters)
            await InitElementParameters();

        if (instant)
        {
            _mainElement.ResizableControl.ShowItem();
            _fadeObjectsContainer.ShowItem();
            SwitchSecondaryButtonsVisibility(true);
            onShown?.Invoke();
            return;
        }

        _mainElement.ResizableControl.Size = new(0, _mainElementSize.Y);
        _mainElement.ResizableControl.ShowItem();

        for (int i = 0; i < _secondaryElements.Length; i++)
        {
            _secondaryElements[i].ResizableControl.GlobalPosition = _mainElement.ResizableControl.GlobalPosition;
        }

        Tween tween = SceneTree.CreateTween();
        tween.SetPauseMode(Tween.TweenPauseMode.Process);

        tween.SetTrans(Tween.TransitionType.Linear);
        tween.TweenAlpha(_fadeObjectsContainer, 1, FadeDuration);

        tween.SetEase(Tween.EaseType.Out);
        tween.SetTrans(Tween.TransitionType.Quad);
        tween.TweenControlSize(_mainElement.ResizableControl, _mainElementSize, _mainElementDuration);
        tween.TweenCallback(Callable.From(() => { SwitchSecondaryButtonsVisibility(true); }));

        tween.SetTrans(Tween.TransitionType.Back);
        for (int i = 0; i < _secondaryElements.Length; i++)
        {
            tween.Parallel().TweenControlPosition(_secondaryElements[i].ResizableControl, Vector2.Zero, _secondaryElementDuration);
        }
        tween.Finished += () => onShown?.Invoke();
    }

    private async Task InitElementParameters()
    {
        await _caller.ToSignal(RenderingServer.Singleton, RenderingServerInstance.SignalName.FramePostDraw);

        _mainElementSize = _mainElement.ResizableControl.Size;
        SetButtonsOrdering();
        _initializedParameters = true;
    }

    private void SetButtonsOrdering()
    {
        List<ITweenableMenuElement> buttonsByPosition = _secondaryElements.OrderByDescending(o => o.PositionControl.Position.Y).ToList();
        int last = 0;
        for (int i = 0; i < buttonsByPosition.Count; i++)
        {
            buttonsByPosition[i].PositionControl.ZIndex = i;
            last = i;
        }
        _mainElement.PositionControl.ZIndex = last + 1;
    }

    private void SwitchSecondaryButtonsVisibility(bool show)
    {
        for (int i = 0; i < _secondaryElements.Length; i++)
        {
            if (show)
                _secondaryElements[i].ResizableControl.ShowItem();
            else
                _secondaryElements[i].ResizableControl.HideItem();
        }
    }

}
