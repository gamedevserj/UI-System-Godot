using Godot;
using System.Threading.Tasks;
using UISystem.Elements.HoverSettings;
using UISystem.Hovering;
using UISystem.Transitions.Interfaces;

namespace UISystem.Elements.ElementViews;
public partial class ButtonView : BaseButton, IFocusableControl, ITweenableMenuElement
{

    [Export] private ButtonHoverSettings buttonHoverSettings;
    [Export] private Control resizableControl;
    [Export] private Control innerColor;
    [Export] private Control border;
    [Export] private Label label;

    private IHoverTweener _hoverTweener;
    private bool _mouseOver;
    private Tween _tween;

    public Control PositionControl => this;
    public Control ResizableControl => resizableControl;

    public override async void _EnterTree()
    {
        if (buttonHoverSettings == null) return;

        await ToSignal(RenderingServer.Singleton, RenderingServerInstance.SignalName.FramePostDraw);

        _hoverTweener = buttonHoverSettings.CreateTweener(resizableControl, innerColor, border, label);
        Subscribe();
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

    // there is no OnDisabled event in BaseButton, so it should be disabled via this method to change appearance
    public void SwitchButton(bool disable)
    {
        Disabled = disable;
        HoverTween();
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
        if (buttonHoverSettings == null) return;
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
        if (_hoverTweener == null) return;

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
