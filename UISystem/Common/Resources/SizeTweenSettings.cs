using Godot;
using UISystem.Common.Enums;
using UISystem.Common.Interfaces;
using UISystem.Common.Structs;
using UISystem.Extensions;

namespace UISystem.Common.Resources;
[GlobalClass]
public partial class SizeTweenSettings : TweenSettings
{

    [Export] private HorizontalControlSizeChangeDirection horizontalDirection = HorizontalControlSizeChangeDirection.FromLeft;
    [Export] private VerticalControlSizeChangeDirection verticalDirection = VerticalControlSizeChangeDirection.FromTop;
    [Export] private Vector2 changeSizeHover = new(75, 0);
    [Export] private Vector2 changeSizeFocus = new(150, 0);

    public HorizontalControlSizeChangeDirection HorizontalDirection => horizontalDirection;
    public VerticalControlSizeChangeDirection VerticalDirection => verticalDirection;
    public Vector2 ChangeSizeHover => changeSizeHover;
    public Vector2 ChangeSizeFocus => changeSizeFocus;

    public ITweener CreateTweener(SceneTree tree, Control target, bool parallel = true) => new SizeTweener(tree, target, parallel, this);

    private class SizeTweener : ITweener
    {

        private Tween _tween;
        private Vector2 _originalSize;
        private Vector2 _originalPosition;

        private readonly SceneTree _tree;
        private readonly Control _target;
        private readonly SizeSettings _sizeSettings;
        private readonly SizeTweenSettings _settings;
        private readonly bool _parallel;

        public SizeTweener(SceneTree tree, Control target, bool parallel, SizeTweenSettings settings)
        {
            _tree = tree;
            _target = target;
            _parallel = parallel;
            _settings = settings;
            _originalSize = target.Size;
            _originalPosition = target.Position;
            _sizeSettings = new SizeSettings(_originalPosition, _originalSize, settings.HorizontalDirection, settings.VerticalDirection);
        }

        public void OnMouseEntered() => Tween(_originalSize + _settings.ChangeSizeHover);
        public void OnMouseExited() => Tween(_originalSize);
        public void OnFocusEntered() => Tween(_originalSize + _settings.ChangeSizeFocus);
        public void OnFocusExited() => Tween(_originalSize);

        private void Tween(Vector2 size)
        {
            _tween?.Kill();
            _tween = _tree.CreateTween();
            _tween.SetEase(_settings.Ease);
            _tween.SetTrans(_settings.Transition);
            _tween.TweenControlSize(_parallel, _target, size, _settings.Duration, _sizeSettings);
        }
    }
}

