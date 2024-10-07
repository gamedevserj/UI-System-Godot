﻿using Godot;
using GodotExtensions;
using System;
using System.Threading.Tasks;
using UISystem.Common.Structs;
using UISystem.Common.Transitions.Interfaces;
using UISystem.Extensions;

namespace UISystem.Common.Transitions;
public class PanelSizeTransition : IViewTransition
{

    private const float FadeDuration = 0.1f;

    private SceneTree _sceneTree;
    private bool _initializedParameters;

    private ResizableControlSettings _panelSizeSettings;
    private ResizableControlSettings[] _elementsSizeSettings;

    private readonly Control _caller;
    private readonly Control _fadeObjectsContainer;
    private readonly Control _panel;
    private readonly Control[] _elements;
    private readonly float _panelDuration;
    private readonly float _elementsDuration;

    private SceneTree SceneTree
    {
        get
        {
            _sceneTree ??= _caller.GetTree();
            return _sceneTree;
        }
    } 

    public PanelSizeTransition(Control caller, Control fadeObjectsContainer, Control panel, Control[] resizableControls, float panelDuration,
        float elementsDuration)
    {
        _caller = caller;
        _fadeObjectsContainer = fadeObjectsContainer;
        _panel = panel;
        _elements = resizableControls;
        _panelDuration = panelDuration;
        _elementsDuration = elementsDuration;
    }

    public void Hide(Action onHidden, bool instant)
    {
        Tween tween = SceneTree.CreateTween();
        tween.SetPauseMode(Tween.TweenPauseMode.Process);

        tween.SetEase(Tween.EaseType.Out);
        tween.SetTrans(Tween.TransitionType.Linear);
        for (int i = 0; i < _elements.Length; i++)
        {
            tween.TweenControlSize(true, _elements[i], Vector2.Zero, _elementsDuration, _elementsSizeSettings[i]);
        }
        tween.TweenCallback(Callable.From(() =>
        {
            for (int i = 0; i < _elements.Length; i++)
            {
                _elements[i].HideItem();
            }
        }));

        tween.SetEase(Tween.EaseType.In);
        tween.SetTrans(Tween.TransitionType.Back);
        tween.TweenControlSize(false, _panel, Vector2.Zero, _panelDuration, _panelSizeSettings);

        tween.SetTrans(Tween.TransitionType.Quad);
        tween.TweenCanvasItemAlpha(false, _fadeObjectsContainer, 0, FadeDuration);
        tween.TweenCallback(Callable.From(() =>
        {
            onHidden?.Invoke();
        }));
    }

    public async void Show(Action onShown, bool instant)
    {
        _fadeObjectsContainer.HideItem();

        if (!_initializedParameters)
            await InitElementParameters();

        _panel.SetSizeAndPosition(Vector2.Zero, _panelSizeSettings.CenterPosition);
        for (int i = 0; i < _elements.Length; i++)
        {
            _elements[i].SetSizeAndPosition(Vector2.Zero, _elementsSizeSettings[i].CenterPosition);
        }

        Tween tween = SceneTree.CreateTween();
        tween.SetPauseMode(Tween.TweenPauseMode.Process);

        tween.SetEase(Tween.EaseType.In);
        tween.SetTrans(Tween.TransitionType.Linear);

        tween.TweenCanvasItemAlpha(false, _fadeObjectsContainer, 1, FadeDuration);
        tween.TweenControlSize(false, _panel, _panelSizeSettings.OriginalSize, _panelDuration, _panelSizeSettings);
        for (int i = 0; i < _elements.Length; i++)
        {
            bool parallel = i != 0;
            tween.TweenControlSize(parallel, _elements[i], _elementsSizeSettings[i].OriginalSize, _elementsDuration, _elementsSizeSettings[i]);
        }
        tween.TweenCallback(Callable.From(() =>
        {
            onShown?.Invoke();
        }));
    }

    private async Task InitElementParameters()
    {
        await _caller.ToSignal(RenderingServer.Singleton, RenderingServerInstance.SignalName.FramePostDraw);

        _elementsSizeSettings = new ResizableControlSettings[_elements.Length];
        var horizontalDirection = Enums.HorizontalDirection.FromCenter;
        var verticalDirection = Enums.VerticalDirection.FromCenter;

        _panelSizeSettings = new(_panel.Position, _panel.Size, horizontalDirection, verticalDirection);
        for (int i = 0; i < _elements.Length; i++)
        {
            _elementsSizeSettings[i] = new(_elements[i].Position, _elements[i].Size, horizontalDirection, verticalDirection);
        }
        _initializedParameters = true;

    }
}
