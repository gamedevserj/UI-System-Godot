using Godot;
using UISystem.Common.Elements;
using UISystem.Common.Resources;
using UISystem.Extensions;

namespace UISystem.Common.ElementViews;
public partial class AnimatedMenuButtonView : ButtonView
{

    [Export] private AnimatedButtonSettings settings;

    private Tween _tween;
    private Vector2 _size;
    private Vector2 _position;

    public override async void _Ready()
    {
        await ToSignal(RenderingServer.Singleton, RenderingServerInstance.SignalName.FramePostDraw);
        _size = Size;
        _position = Position;
    }

    public override void _EnterTree()
    {
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
        MouseEnteredTweenSize(settings.ChangeSizeHover);
    }

    private void OnMouseExited()
    {
        MouseEnteredTweenSize(Vector2.Zero);
    }

    private void OnFocusEntered()
    {
        Animate(settings.ChangeSizeFocus);
    }

    private void OnFocusExited()
    {
        Animate(Vector2.Zero);
    }

    private void MouseEnteredTweenSize(Vector2 size)
    {
        if (HasFocus()) return;
        Animate(size);

    }

    private void Animate(Vector2 size)
    {
        if (Disabled) return;
        _tween?.Kill();
        _tween = GetTree().CreateTween();
        _tween.SetEase(settings.Ease);
        _tween.SetTrans(settings.Transition);
        _tween.TweenControlSize(true, this, size, settings.Duration, _position, _size, settings.HorizontalDirection, settings.VerticalDirection);
    }

}
