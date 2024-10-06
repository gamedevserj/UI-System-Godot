﻿using Godot;
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

    public ITweener CreateTweener(Control target, bool parallel = true) 
        => new SizeTweener(target, parallel, this, Vector2.Zero, target.Size, target.Position, horizontalDirection, verticalDirection);

    private class SizeTweener : Tweener<Vector2>
    {

        private readonly ResizableControlSettings _sizeSettings;

        public SizeTweener(Control target, bool parallel, TweenSettings<Vector2> settings, Vector2 originalValue, 
            Vector2 originalSize,
            Vector2 originalPosition,
            HorizontalControlSizeChangeDirection horizontalDirection,
            VerticalControlSizeChangeDirection verticalDirection
            )
            : base(target, parallel, settings, originalValue)
        {
            _sizeSettings = new ResizableControlSettings(originalPosition, originalSize, horizontalDirection, verticalDirection);
        }

        protected override void Tween(Tween tween, Vector2 value)
        {
            base.Tween(tween, value);
            tween.TweenControlSize(_parallel, _target, _sizeSettings.OriginalSize + value, _settings.Duration, _sizeSettings);
        }

        public override void Reset(Tween tween)
        {
            base.Reset(tween);
            tween.TweenControlSize(_parallel, _target, _sizeSettings.OriginalSize, _settings.ResetDuration, _sizeSettings);
        }
    }
    
}

