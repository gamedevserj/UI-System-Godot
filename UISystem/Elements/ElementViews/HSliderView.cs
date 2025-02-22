﻿using Godot;
using System.Threading.Tasks;
using UISystem.Elements.HoverSettings;
using UISystem.Hovering;
using UISystem.Transitions.Interfaces;

namespace UISystem.Elements.ElementViews;
public partial class HSliderView : HSlider, IFocusableControl, ITweenableMenuElement
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

    public Control PositionControl => this;
    public Control ResizableControl => resizableControl;

    public override async void _EnterTree()
    {
        if (hoverSettings == null) return;

        await ToSignal(RenderingServer.Singleton, RenderingServerInstance.SignalName.FramePostDraw);

        _hoverTweener = hoverSettings.CreateTweener(grabberResizableControl, background, fill);
        Subscribe();
        UpdateSliderVisual(Value);
    }

    public override void _ExitTree() => Unsubscribe();

    public async Task ResetHover()
    {
        if (_hoverTweener == null) await Task.CompletedTask;

        _tween?.Kill();
        _tween = GetTree().CreateTween();
        _hoverTweener.Reset(_tween);
        await ToSignal(_tween, Tween.SignalName.Finished);
    }

    public override void _ValueChanged(double newValue)
    {
        if (hoverSettings == null) return;
        
        UpdateSliderVisual(newValue);
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
        if (hoverSettings == null) return;
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
        if (_hoverTweener == null) return;

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

    private void UpdateSliderVisual(double newValue)
    {
        float value = (float)newValue;
        fill.SetAnchor(Side.Right, value, true);
        grabber.Position = new Vector2((background.Size.X * value) - grabber.Size.X * 0.5f, grabber.Position.Y);
    }

}
