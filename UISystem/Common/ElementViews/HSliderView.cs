using Godot;
using UISystem.Common.Enums;
using UISystem.Common.Interfaces;
using UISystem.Common.Resources;

namespace UISystem.Common.Elements;
public partial class HSliderView : HSlider, IFocusableControl
{

    [Export] private ColorTweenSettings hoverSettings;
    [Export] private Control sliderBackground;
    [Export] private Control sliderFill;
    [Export] private Control grabber;

    private ITweener _tweener;
    private bool _mouseOver;
    private bool _isDragging;

    public override async void _EnterTree()
    {
        if (hoverSettings == null) return;

        await ToSignal(RenderingServer.Singleton, RenderingServerInstance.SignalName.FramePostDraw);

        _tweener = hoverSettings.CreateTweener(GetTree(), grabber);
        Subscribe();
        UpdateSliderVisual();
    }

    public override void _ExitTree() => Unsubscribe();

    public override void _Process(double delta)
    {
        if (hoverSettings == null) return;
        if (_isDragging)
        {
            UpdateSliderVisual();
        }
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
        _tweener.Tween(GetDrawingMode());
    }
    private void OnMouseExited()
    {
        _mouseOver = false;
        _tweener.Tween(GetDrawingMode());
    }

    private void OnFocusEntered() => _tweener.Tween(GetDrawingMode());
    private void OnFocusExited() => _tweener.Tween(GetDrawingMode());

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
        _tweener.Tween(GetDrawingMode());
    }

    private void UpdateSliderVisual()
    {
        grabber.Position = new Vector2((sliderBackground.Size.X * (float)Value) - grabber.Size.X * 0.5f, grabber.Position.Y);
        sliderFill.Size = new Vector2((sliderBackground.Size.X * (float)Value), sliderFill.Size.Y);
    }

}
