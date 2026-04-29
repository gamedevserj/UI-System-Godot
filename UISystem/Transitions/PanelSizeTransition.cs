using System.Threading.Tasks;
using Godot;
using UISystem.Core.Transitions;
using UISystem.Extensions;
using UISystem.Transitions.Enums;
using UISystem.Transitions.Extensions;
using UISystem.Transitions.Interfaces;
using UISystem.Transitions.Structs;

namespace UISystem.Transitions;

/// <summary>
/// Transition where panel is grows/shrinks in size.
/// </summary>
public class PanelSizeTransition : IViewTransition
{
    private const float FadeDuration = 0.1f;
    private const float PanelDuration = 0.2f;
    private const float ElementsDuration = 0.1f;

    private readonly Control _caller;
    private readonly Control _fadeObjectsContainer;
    private readonly Control _panel;
    private readonly ITweenableMenuElement[] _elements;
    private readonly float _panelDuration;
    private readonly float _elementsDuration;

    private SceneTree _sceneTree;
    private bool _initializedParameters;

    private ResizableControlSettings _panelSettings;
    private ResizableControlSettings[] _elementsSettings;

    /// <summary>
    /// Initializes a new instance of the <see cref="PanelSizeTransition"/> class.
    /// </summary>
    /// <param name="caller">Control that called this transition.</param>
    /// <param name="fadeObjectsContainer">Canvas group that contains all objects.</param>
    /// <param name="panel">Panel that will be resized.</param>
    /// <param name="resizableControls">Resizable controls.</param>
    /// <param name="panelDuration">Duration to resize panel.</param>
    /// <param name="elementsDuration">Duration to resize elements.</param>
    public PanelSizeTransition(
        Control caller,
        Control fadeObjectsContainer,
        Control panel,
        ITweenableMenuElement[] resizableControls,
        float panelDuration = PanelDuration,
        float elementsDuration = ElementsDuration)
    {
        _caller = caller;
        _fadeObjectsContainer = fadeObjectsContainer;
        _panel = panel;
        _elements = resizableControls;
        _panelDuration = panelDuration;
        _elementsDuration = elementsDuration;
    }

    private SceneTree SceneTree
    {
        get
        {
            _sceneTree ??= _caller.GetTree();
            return _sceneTree;
        }
    }

    /// <inheritdoc/>
    public async Task Hide(bool instant = false)
    {
        if (instant)
        {
            _fadeObjectsContainer.HideItem();
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
            tween.TweenControlSize(true, _elements[i].ResizableControl, Vector2.Zero, _elementsDuration, _elementsSettings[i]);
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
        tween.TweenControlSize(false, _panel, Vector2.Zero, _panelDuration, _panelSettings);

        tween.SetTrans(Tween.TransitionType.Quad);
        tween.TweenAlpha(_fadeObjectsContainer, 0, FadeDuration);

        await SceneTree.ToSignal(tween, Tween.SignalName.Finished);
    }

    /// <inheritdoc/>
    public async Task Show(bool instant = false)
    {
        // should always hide before showing because awaiting for parameters shows menu for a split second
        _fadeObjectsContainer.HideItem();

        if (!_initializedParameters)
            await InitElementParameters();

        if (instant)
        {
            _panel.Size = _panelSettings.OriginalSize;
            for (int i = 0; i < _elements.Length; i++)
            {
                _elements[i].ResizableControl.Size = _elementsSettings[i].OriginalSize;
                _elements[i].ResizableControl.Position = _elementsSettings[i].OriginalPosition;
            }

            _fadeObjectsContainer.ShowItem();
            return;
        }

        _panel.SetSizeAndPosition(Vector2.Zero, _panelSettings.CenterPosition);
        for (int i = 0; i < _elements.Length; i++)
        {
            _elements[i].ResizableControl.SetSizeAndPosition(Vector2.Zero, _elementsSettings[i].CenterPosition);
        }

        Tween tween = SceneTree.CreateTween();
        tween.SetPauseMode(Tween.TweenPauseMode.Process);

        tween.SetEase(Tween.EaseType.In);
        tween.SetTrans(Tween.TransitionType.Linear);

        tween.TweenAlpha(_fadeObjectsContainer, 1, FadeDuration);
        tween.TweenControlSize(false, _panel, _panelSettings.OriginalSize, _panelDuration, _panelSettings);
        for (int i = 0; i < _elements.Length; i++)
        {
            bool parallel = i != 0;
            tween.TweenControlSize(parallel, _elements[i].ResizableControl, _elementsSettings[i].OriginalSize, _elementsDuration, _elementsSettings[i]);
        }

        await SceneTree.ToSignal(tween, Tween.SignalName.Finished);
    }

    private async Task InitElementParameters()
    {
        await _caller.ToSignal(RenderingServer.Singleton, RenderingServerInstance.SignalName.FramePostDraw);
        _elementsSettings = new ResizableControlSettings[_elements.Length];
        var horizontalDirection = HorizontalDirection.FromCenter;
        var verticalDirection = VerticalDirection.FromCenter;

        _panelSettings = new(_panel.Position, _panel.Size, horizontalDirection, verticalDirection);
        for (int i = 0; i < _elements.Length; i++)
        {
            _elementsSettings[i] = new(_elements[i].ResizableControl.Position, _elements[i].ResizableControl.Size, horizontalDirection, verticalDirection);
        }

        _initializedParameters = true;
    }
}
