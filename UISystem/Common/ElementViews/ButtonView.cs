using Godot;
using System.Threading.Tasks;
using UISystem.Common.ElementViews;
using UISystem.Common.Enums;
using UISystem.Common.Interfaces;
using UISystem.Common.Resources;

namespace UISystem.Common.Elements;
public partial class ButtonView : BaseButton, IFocusableControl, ISizeTweenable
{

    [Export] private ButtonHoverSettings buttonHoverSettings;
    [Export] private AnimatedButtonView animatedButtonView;

    private ITweener _hoverTweener;
    private bool _mouseOver;
    private Tween _tween;

    public Control ResizableControl => animatedButtonView.ResizableControl;
    private Control Border => animatedButtonView.Border;

    public override async void _EnterTree()
    {
        if (buttonHoverSettings == null) return;

        await ToSignal(RenderingServer.Singleton, RenderingServerInstance.SignalName.FramePostDraw);
        
        _hoverTweener = buttonHoverSettings.CreateTweener(animatedButtonView.ResizableControl, Border);
        Subscribe();
    }

    public override void _ExitTree() => Unsubscribe();

    public async Task ResetHover()
    {
        _tween?.Kill();
        _tween = GetTree().CreateTween();
        _hoverTweener.Reset(_tween);
        await ToSignal(_tween, Tween.SignalName.Finished);
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
        if (Disabled) return ControlDrawMode.Disabled;
        if (HasFocus())
        {
            return _mouseOver ? ControlDrawMode.HoverFocus : ControlDrawMode.Focus;
        }
        else
            return _mouseOver ? ControlDrawMode.Hover : ControlDrawMode.Normal;
    }

}
