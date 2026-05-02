using System.Threading.Tasks;
using Godot;
using UISystem.Core.Elements;
using UISystem.Elements.HoverSettings;
using UISystem.Extensions;
using UISystem.Hovering;
using UISystem.Transitions.Interfaces;

namespace UISystem.Elements.ElementViews;

/// <summary>
/// Base class for horizontal slider view.
/// </summary>
public partial class HSliderView : HSlider, IInteractableElement, ITweenableMenuElement
{
    [Export] private HSliderHoverSettings hoverSettings;
    [Export] private Control grabber;
    [Export] private Control grabberResizableControl;
    [Export] private Control background;
    [Export] private Control fill;
    [Export] private Control resizableControl;

    private IHoverTweener _hoverTweener;
    private bool _mouseOver;
    private bool _isDragging;
    private Tween _tween;

    /// <summary>
    /// Gets the control responsible for button position.
    /// </summary>
    public Control PositionControl => this;

    /// <summary>
    /// Gets the control responsible for resizing the button.
    /// </summary>
    public Control ResizableControl => resizableControl;

    /// <inheritdoc/>
    public override async void _EnterTree()
    {
        if (hoverSettings == null)
            return;

        await ToSignal(RenderingServer.Singleton, RenderingServerInstance.SignalName.FramePostDraw);

        _hoverTweener = hoverSettings.CreateTweener(grabberResizableControl, background, fill);
        Subscribe();
        UpdateSliderVisual(Value);
    }

    /// <inheritdoc/>
    public override void _ExitTree() => Unsubscribe();

    /// <inheritdoc/>
    public async Task ResetHover()
    {
        if (_hoverTweener == null)
            await Task.CompletedTask;

        _tween?.Kill();
        _tween = GetTree().CreateTween();
        _hoverTweener.Reset(_tween);
        await ToSignal(_tween, Tween.SignalName.Finished);
    }

    /// <inheritdoc/>
    public override void _ValueChanged(double newValue)
    {
        if (hoverSettings == null)
            return;

        UpdateSliderVisual(newValue);
    }

    /// <inheritdoc/>
    public void SwitchFocus(bool focus)
    {
        if (focus)
            GrabFocus();
        else
            ReleaseFocus();
    }

    /// <inheritdoc/>
    public bool IsValidElement() => this.IsValid();

    /// <inheritdoc/>
    public void SwitchFocusAvailability(bool focusable)
    {
        FocusMode = focusable ? FocusModeEnum.All : FocusModeEnum.None;
        MouseFilter = focusable ? MouseFilterEnum.Stop : MouseFilterEnum.Ignore;

        if (!focusable && HasFocus())
            SwitchFocus(false);
    }

    private void Subscribe()
    {
        FocusEntered += OnFocusEntered;
        FocusExited += OnFocusExited;
        MouseEntered += OnMouseEntered;
        MouseExited += OnMouseExited;
        DragStarted += OnDragStarted;
        DragEnded += OnDragEnded;
    }

    private void Unsubscribe()
    {
        if (hoverSettings == null)
            return;
        FocusEntered -= OnFocusEntered;
        FocusExited -= OnFocusExited;
        MouseEntered -= OnMouseEntered;
        MouseExited -= OnMouseExited;
        DragStarted -= OnDragStarted;
        DragEnded -= OnDragEnded;
    }

    private void OnMouseEntered()
    {
        _mouseOver = true;
        HoverTween();
    }

    private void OnMouseExited()
    {
        _mouseOver = false;
        HoverTween();
    }

    private void OnFocusEntered() => HoverTween();

    private void OnFocusExited() => HoverTween();

    private void HoverTween()
    {
        if (_hoverTweener == null)
            return;

        _tween?.Kill();
        _tween = GetTree().CreateTween();
        _hoverTweener.Tween(_tween, GetDrawingMode());
    }

    private ControlDrawMode GetDrawingMode()
    {
        if (HasFocus())
        {
            var isDragginFocus = _isDragging ? ControlDrawMode.HoverFocus : ControlDrawMode.Focus;
            return _mouseOver ? ControlDrawMode.HoverFocus : isDragginFocus;
        }
        else
        {
            return _mouseOver ? ControlDrawMode.Hover : ControlDrawMode.Normal;
        }
    }

    private void OnDragStarted() => _isDragging = true;

    private void OnDragEnded(bool changed)
    {
        _isDragging = false;
        HoverTween();
    }

    private void UpdateSliderVisual(double newValue)
    {
        float value = (float)newValue;
        fill.SetAnchor(Side.Right, value, true);
        grabber.Position = new Vector2((background.Size.X * value) - (grabber.Size.X * 0.5f), grabber.Position.Y);
    }
}
