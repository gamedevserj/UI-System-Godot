using Godot;
using System;
using System.Threading.Tasks;
using UISystem.Core.Transitions;
using UISystem.Extensions;
using UISystem.Transitions.Enums;
using UISystem.Transitions.Extensions;
using UISystem.Transitions.Interfaces;
using UISystem.Transitions.Structs;

namespace UISystem.Transitions;
public class PanelSizeTransition : IViewTransition
{

    private const float FadeDuration = 0.1f;
    protected const float PanelDuration = 0.2f;
    protected const float ElementsDuration = 0.1f;

    private SceneTree _sceneTree;
    private bool _initializedParameters;

    private ResizableControlSettings _panelSizeSettings;
    private ResizableControlSettings[] _elementsSizeSettings;

    private readonly Control _caller;
    private readonly Control _fadeObjectsContainer;
    private readonly Control _panel;
    private readonly ITweenableMenuElement[] _elements;
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

    public PanelSizeTransition(Control caller, Control fadeObjectsContainer, Control panel, ITweenableMenuElement[] resizableControls, 
        float panelDuration = PanelDuration, float elementsDuration = ElementsDuration)
    {
        _caller = caller;
        _fadeObjectsContainer = fadeObjectsContainer;
        _panel = panel;
        _elements = resizableControls;
        _panelDuration = panelDuration;
        _elementsDuration = elementsDuration;
    }

    public async void Hide(Action onHidden, bool instant)
    {
        if (instant)
        {
            _fadeObjectsContainer.HideItem();
            onHidden?.Invoke();
            return;
        }

        var tasks = new Task[_elements.Length];
        for (int i = 0; i < tasks.Length; i++)
        {
            tasks[i] = _elements[i].ResetHover();
        }
        await Task.WhenAll(tasks);

        Tween tween = SceneTree.CreateTween();
        tween.SetPauseMode(Tween.TweenPauseMode.Process);

        tween.SetEase(Tween.EaseType.Out);
        tween.SetTrans(Tween.TransitionType.Linear);
        for (int i = 0; i < _elements.Length; i++)
        {
            tween.TweenControlSize(true, _elements[i].ResizableControl, Vector2.Zero, _elementsDuration, _elementsSizeSettings[i]);
        }
        tween.TweenCallback(Callable.From(() =>
        {
            for (int i = 0; i < _elements.Length; i++)
            {
                _elements[i].ResizableControl.HideItem();
            }
        }));

        tween.SetEase(Tween.EaseType.In);
        tween.SetTrans(Tween.TransitionType.Back);
        tween.TweenControlSize(false, _panel, Vector2.Zero, _panelDuration, _panelSizeSettings);

        tween.SetTrans(Tween.TransitionType.Quad);
        tween.TweenAlpha(_fadeObjectsContainer, 0, FadeDuration);
        tween.Finished += () => onHidden?.Invoke();
    }

    public async void Show(Action onShown, bool instant)
    {
        // should always hide before showing because awaiting for parameters shows menu for a split second
        _fadeObjectsContainer.HideItem();

        if (!_initializedParameters)
            await InitElementParameters();

        if (instant)
        {
            _panel.Size = _panelSizeSettings.OriginalSize;
            for (int i = 0; i < _elements.Length; i++)
            {
                _elements[i].ResizableControl.Size = _elementsSizeSettings[i].OriginalSize;
                _elements[i].ResizableControl.Position = _elementsSizeSettings[i].OriginalPosition;
            }
            _fadeObjectsContainer.ShowItem();
            onShown?.Invoke();
            return;
        }
        
        _panel.SetSizeAndPosition(Vector2.Zero, _panelSizeSettings.CenterPosition);
        for (int i = 0; i < _elements.Length; i++)
        {
            _elements[i].ResizableControl.SetSizeAndPosition(Vector2.Zero, _elementsSizeSettings[i].CenterPosition);
        }

        Tween tween = SceneTree.CreateTween();
        tween.SetPauseMode(Tween.TweenPauseMode.Process);

        tween.SetEase(Tween.EaseType.In);
        tween.SetTrans(Tween.TransitionType.Linear);

        tween.TweenAlpha(_fadeObjectsContainer, 1, FadeDuration);
        tween.TweenControlSize(false, _panel, _panelSizeSettings.OriginalSize, _panelDuration, _panelSizeSettings);
        for (int i = 0; i < _elements.Length; i++)
        {
            bool parallel = i != 0;
            tween.TweenControlSize(parallel, _elements[i].ResizableControl, _elementsSizeSettings[i].OriginalSize, _elementsDuration, _elementsSizeSettings[i]);
        }
        tween.Finished += () => onShown?.Invoke();
    }

    private async Task InitElementParameters()
    {
        await _caller.ToSignal(RenderingServer.Singleton, RenderingServerInstance.SignalName.FramePostDraw);

        _elementsSizeSettings = new ResizableControlSettings[_elements.Length];
        var horizontalDirection = HorizontalDirection.FromCenter;
        var verticalDirection = VerticalDirection.FromCenter;

        _panelSizeSettings = new(_panel.Position, _panel.Size, horizontalDirection, verticalDirection);
        for (int i = 0; i < _elements.Length; i++)
        {
            _elementsSizeSettings[i] = new(_elements[i].ResizableControl.Position, _elements[i].ResizableControl.Size, horizontalDirection, verticalDirection);
        }
        _initializedParameters = true;

    }
}
