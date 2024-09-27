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
        _tweener.OnMouseEntered(GetDrawingMode());
    }
    private void OnMouseExited()
    {
        _mouseOver = false;
        _tweener.OnMouseExited(GetDrawingMode());
    }

    private void OnFocusEntered()
    {
        _tweener.OnFocusEntered(GetDrawingMode());
    }
    private void OnFocusExited()
    {
        _tweener.OnFocusExited(GetDrawingMode());
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

}
