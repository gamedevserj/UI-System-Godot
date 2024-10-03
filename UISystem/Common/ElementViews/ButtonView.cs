using Godot;
using UISystem.Common.ElementViews;
using UISystem.Common.Enums;
using UISystem.Common.Interfaces;
using UISystem.Common.Resources;

namespace UISystem.Common.Elements;
public partial class ButtonView : BaseButton, IFocusableControl
{

    [Export] private ButtonHoverSettings buttonHoverSettings;
    [Export] private AnimatedButtonView animatedButtonView;

    private ITweener _tweener;
    private bool _mouseOver;

    public Control ResizableizeControl => animatedButtonView.ResizableControl;
    private Control Border => animatedButtonView.Border;
    private bool CanAnimateHover => !Disabled && FocusMode == FocusModeEnum.All && MouseFilter == MouseFilterEnum.Stop;

    public override async void _EnterTree()
    {
        if (buttonHoverSettings == null) return;

        await ToSignal(RenderingServer.Singleton, RenderingServerInstance.SignalName.FramePostDraw);
        
        _tweener = buttonHoverSettings.CreateTweener(GetTree(), ResizableizeControl, Border);
        Subscribe();
    }

    public override void _ExitTree() => Unsubscribe();

    private void Subscribe()
    {
        FocusEntered += OnFocusEntered;
        FocusExited += OnFocusExited;
        MouseEntered += OnMouseEntered;
        MouseExited += OnMouseExited;
        
    }

    private void Unsubscribe()
    {
        FocusEntered -= OnFocusEntered;
        FocusExited -= OnFocusExited;
        MouseEntered -= OnMouseEntered;
        MouseExited -= OnMouseExited;
    }

    private void OnMouseEntered()
    {
        _mouseOver = true;
        Tween();
    }
    private void OnMouseExited()
    {
        _mouseOver = false;
        Tween();
    }

    private void OnFocusEntered() => Tween();
    private void OnFocusExited() => Tween();

    private void Tween()
    {
        if (!CanAnimateHover) return;
        _tweener.Tween(GetDrawingMode());
    }

    private ControlDrawMode GetDrawingMode()
    {
        if (Disabled) return ControlDrawMode.Disabled;
        if (HasFocus())
        {
            return _mouseOver ? ControlDrawMode.HoverFocus : ControlDrawMode.Focus;
        }
        else
            return _mouseOver ? ControlDrawMode.Hover : ControlDrawMode.Normal;
    }

    public void FosucabilitySwitched(bool on)
    {
        if (_tweener == null) return;

        if (on)
            _tweener.Tween(ControlDrawMode.Normal);
        else
            _tweener.Tween(ControlDrawMode.Disabled);
    }
}
