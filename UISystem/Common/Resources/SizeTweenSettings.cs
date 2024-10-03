using Godot;
using UISystem.Common.Enums;
using UISystem.Common.Interfaces;
using UISystem.Common.Structs;
using UISystem.Extensions;

namespace UISystem.Common.Resources;
[GlobalClass]
public partial class SizeTweenSettings : TweenSettings<Vector2>
{

    [Export] private HorizontalControlSizeChangeDirection horizontalDirection = HorizontalControlSizeChangeDirection.FromLeft;
    [Export] private VerticalControlSizeChangeDirection verticalDirection = VerticalControlSizeChangeDirection.FromTop;
    [Export] private Vector2 changeSizeHover = new(75, 0);
    [Export] private Vector2 changeSizeFocus = new(100, 0);
    [Export] private Vector2 changeSizeFocusHover = new(150, 0);

    public override Vector2 HoverValue => changeSizeHover;
    public override Vector2 FocusValue => changeSizeFocus;
    public override Vector2 FocusHoverValue => changeSizeFocusHover;
    public override Vector2 DisabledValue => Vector2.Zero;

    public ITweener CreateTweener(SceneTree tree, Control target, bool parallel = true) 
        => new SizeTweener(tree, target, parallel, this, Vector2.Zero, target.Size, target.Position, horizontalDirection, verticalDirection);

    private class SizeTweener : Tweener<Vector2>
    {

        private readonly Vector2 _originalSize;
        private readonly Vector2 _originalPosition;
        private readonly SizeSettings _sizeSettings;

        public SizeTweener(SceneTree tree, Control target, bool parallel, TweenSettings<Vector2> settings, Vector2 originalValue, 
            Vector2 originalSize,
            Vector2 originalPosition,
            HorizontalControlSizeChangeDirection horizontalDirection,
            VerticalControlSizeChangeDirection verticalDirection
            )
            : base(tree, target, parallel, settings, originalValue)
        {
            _originalSize = originalSize;
            _originalPosition = originalPosition;
            _sizeSettings = new SizeSettings(_originalPosition, _originalSize, horizontalDirection, verticalDirection);
        }

        protected override void Tween(Vector2 value)
        {
            _tween?.Kill();
            _tween = _tree.CreateTween();
            _tween.SetEase(_settings.Ease);
            _tween.SetTrans(_settings.Transition);
            _tween.TweenControlSize(_parallel, _target, _originalSize + value, _settings.Duration, _sizeSettings);
        }
    }
    
}

