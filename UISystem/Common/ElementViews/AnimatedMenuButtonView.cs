using Godot;
using UISystem.Common.Elements;
using UISystem.Constants;

namespace UISystem.Common.ElementViews;
public partial class AnimatedMenuButtonView : ButtonView
{

    private const float AnimationDuration = 1f;

    [Export] private float mouseEnterSize = 75;

    private Tween _tween;
    private Vector2 _size;

    public override async void _Ready()
    {
        await ToSignal(RenderingServer.Singleton, RenderingServerInstance.SignalName.FramePostDraw);
        _size = new Vector2(GetParentAreaSize().X, Size.Y); 
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
        MouseEnteredTweenSize(_size + new Vector2(mouseEnterSize, 0));
    }

    private void OnMouseExited()
    {
        MouseEnteredTweenSize(_size);
    }

    private void OnFocusEntered()
    {
        TweenSize(_size + new Vector2(mouseEnterSize * 2f, 0));
    }

    private void OnFocusExited()
    {
        TweenSize(_size);
    }

    private void MouseEnteredTweenSize(Vector2 size)
    {
        if (HasFocus()) return;
        TweenSize(size);

    }

    private void TweenSize(Vector2 size)
    {
        if (Disabled) return;
        _tween?.Kill();
        _tween = GetTree().CreateTween();
        _tween.SetEase(Tween.EaseType.Out);
        _tween.SetTrans(Tween.TransitionType.Elastic);
        _tween.TweenProperty(this, PropertyConstants.Size, size, AnimationDuration);
    }

}
