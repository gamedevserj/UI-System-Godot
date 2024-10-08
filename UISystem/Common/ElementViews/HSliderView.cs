using Godot;
using UISystem.Common.Enums;
using UISystem.Common.HoverSettings;
using UISystem.Common.Interfaces;
using UISystem.Core.Elements.Interfaces;

namespace UISystem.Common.ElementViews;
public partial class HSliderView : HSlider, IFocusableControl
{

    [Export] private HSliderHoverSettings hoverSettings;
    [Export] private Control grabber;
    [Export] private Control grabberResizableControl;
    [Export] private Control background;
    [Export] private Control fill;
    [Export] private Control resizableControl;

    private ITweener _hoverTweener;
    private bool _mouseOver;
    private bool _isDragging;
    private Tween _tween;

    public Control ResizableControl => resizableControl;

    public override async void _EnterTree()
    {
        if (hoverSettings == null) return;

        await ToSignal(RenderingServer.Singleton, RenderingServerInstance.SignalName.FramePostDraw);

        _hoverTweener = hoverSettings.CreateTweener(grabberResizableControl, background, fill);
        Subscribe();
        UpdateSliderVisual();
    }

    public override void _ExitTree() => Unsubscribe();

    public override void _Process(double delta)
    {
        if (hoverSettings == null) return;
        if (_isDragging)
            UpdateSliderVisual();
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
        _tween?.Kill();
        _tween = GetTree().CreateTween();
        _hoverTweener.Tween(_tween, GetDrawingMode());
    }

    private ControlDrawMode GetDrawingMode()
    {
        if (HasFocus())
        {
            return _mouseOver ? ControlDrawMode.HoverFocus : _isDragging ? ControlDrawMode.HoverFocus : ControlDrawMode.Focus;
        }
        else
            return _mouseOver ? ControlDrawMode.Hover : ControlDrawMode.Normal;
    }

    private void OnDragStarted() => _isDragging = true;

    private void OnDragEnded(bool changed)
    {
        _isDragging = false;
        HoverTween();
    }

    private void UpdateSliderVisual()
    {
        float value = (float)Value;
        fill.SetAnchor(Side.Right, value, true);
        grabber.Position = new Vector2((background.Size.X * value) - grabber.Size.X * 0.5f, grabber.Position.Y);
    }

}
