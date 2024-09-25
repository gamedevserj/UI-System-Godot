using Godot;
using System.Threading.Tasks;
using UISystem.Common.Elements;
using UISystem.Common.Interfaces;
using UISystem.Common.Resources;
using UISystem.Common.Structs;
using UISystem.Extensions;

namespace UISystem.Common.ElementViews;
public partial class AnimatedButtonView : Control, IAnimatedControl
{

    [Export] private AnimatedButtonSettings settings;
    //[Export] 
    private ButtonView _button;

    private Tween _tween;
    private Vector2 _size;
    private Vector2 _position;
    private TweenSizeSettings _tweenSizeSettings;

    public Vector2 OriginalSize => _size;
    public Vector2 OriginalPosition => _position;

    public async Task Init(ButtonView button)
    {
        await ToSignal(RenderingServer.Singleton, RenderingServerInstance.SignalName.FramePostDraw);
        _button = button;
        _size = Size;
        _position = Position;
        _tweenSizeSettings = new TweenSizeSettings(OriginalPosition, OriginalSize, settings.HorizontalDirection, settings.VerticalDirection);
        Subscribe();
    }

    //public override void _EnterTree() => Subscribe();
    public override void _ExitTree() => Unsubscribe();

    private void Subscribe()
    {
        _button.FocusEntered += OnFocusEntered;
        _button.FocusExited += OnFocusExited;
        _button.MouseEntered += OnMouseEntered;
        _button.MouseExited += OnMouseExited;
    }

    private void Unsubscribe()
    {
        _button.FocusEntered -= OnFocusEntered;
        _button.FocusExited -= OnFocusExited;
        _button.MouseEntered -= OnMouseEntered;
        _button.MouseExited -= OnMouseExited;
    }

    private void OnMouseEntered() => MouseEnteredTweenSize(OriginalSize + settings.ChangeSizeHover);
    private void OnMouseExited() => MouseEnteredTweenSize(OriginalSize);
    private void OnFocusEntered() => Animate(OriginalSize + settings.ChangeSizeFocus);
    private void OnFocusExited() => Animate(OriginalSize);

    private void MouseEnteredTweenSize(Vector2 size)
    {
        if (_button.HasFocus()) return;
        Animate(size);
    }

    private void Animate(Vector2 size)
    {
        if (_button.Disabled) return;
        _tween?.Kill();
        _tween = GetTree().CreateTween();
        _tween.SetEase(settings.Ease);
        _tween.SetTrans(settings.Transition);
        _tween.ControlSize(true, this, size, settings.Duration, _tweenSizeSettings);
    }

}
