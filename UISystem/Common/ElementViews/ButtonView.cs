using Godot;
using UISystem.Common.ElementViews;
using UISystem.Common.Interfaces;
using UISystem.Common.Resources;

namespace UISystem.Common.Elements;
public partial class ButtonView : BaseButton, IFocusableControl
{

    [Export] private ButtonHoverSettings buttonHoverSettings;
    [Export] private AnimatedButtonView animatedButtonView;

    private ITweener _tweener;

    public Control SizeControl => animatedButtonView.ResizeableControl;

    public override async void _EnterTree()
    {
        if (buttonHoverSettings == null) return;

        await ToSignal(RenderingServer.Singleton, RenderingServerInstance.SignalName.FramePostDraw);
        _tweener = buttonHoverSettings.SizeChangeSettings.CreateTweener(GetTree(), SizeControl);
        Subscribe();
    }

    public override void _ExitTree()
    {
        Unsubscribe();
    }

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
        if (HasFocus()) return;
        _tweener.OnMouseEntered();
    }
    private void OnMouseExited()
    {
        if (HasFocus()) return;
        _tweener.OnMouseExited();
    }

    private void OnFocusEntered()
    {
        if (Disabled) return;
        _tweener.OnFocusEntered();
    }
    private void OnFocusExited()
    {
        if (Disabled) return;
        _tweener.OnFocusExited();
    }

}
