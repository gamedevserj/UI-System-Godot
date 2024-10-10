using Godot;
using UISystem.Common.Enums;
using UISystem.Common.Extensions;
using UISystem.Common.Interfaces;

namespace UISystem.Common.HoverSettings;
[GlobalClass]
public partial class SizeTweenSettings : TweenSettings<Vector2>
{

    [Export] private HorizontalDirection horizontalDirection = HorizontalDirection.FromLeft;
    [Export] private VerticalDirection verticalDirection = VerticalDirection.FromTop;
    [Export] private Vector2 changeSizeHover = new(0, 0);
    [Export] private Vector2 changeSizeFocus = new(0, 0);
    [Export] private Vector2 changeSizeFocusHover = new(0, 0);

    public override Vector2 NormalValue => Vector2.Zero;
    protected override Vector2 HoverValue => changeSizeHover;
    protected override Vector2 FocusValue => changeSizeFocus;
    protected override Vector2 FocusHoverValue => changeSizeFocusHover;
    protected override Vector2 DisabledValue => Vector2.Zero;


    public ITweener CreateTweener(Control target, TransitionAndEaseSettings transitionAndEaseSettings, bool parallel = true)
        => new SizeTweener(target, target.Size, transitionAndEaseSettings, this, parallel);

    private class SizeTweener : Tweener<Vector2>
    {

        //private readonly ResizableControlSettings _sizeSettings;
        private Vector2 _originalValue;

        public SizeTweener(Control target, Vector2 originalSize, TransitionAndEaseSettings transitionAndEaseSettings,
            TweenSettings<Vector2> settings, bool parallel)
            : base(target, transitionAndEaseSettings, settings, parallel)
        {
            _originalValue = originalSize;
            //_sizeSettings = new ResizableControlSettings(originalPosition, originalSize, horizontalDirection, verticalDirection);
        }

        protected override void Tween(Tween tween, Vector2 value)
        {
            base.Tween(tween, value);
            tween.TweenControlSize(_parallel, _target, _originalValue + value, _settings.Duration);//, _sizeSettings);
        }

        public override void Reset(Tween tween)
        {
            base.Reset(tween);
            tween.TweenControlSize(_parallel, _target, _originalValue, _settings.ResetDuration);//, _sizeSettings);
        }

        //protected override Vector2 SelectValue(ControlDrawMode mode) => mode switch
        //{
        //    ControlDrawMode.Normal => Vector2.Zero,
        //    ControlDrawMode.Hover => _settings.HoverValue,
        //    ControlDrawMode.Focus => _settings.FocusValue,
        //    ControlDrawMode.HoverFocus => _settings.FocusHoverValue,
        //    ControlDrawMode.Disabled => _settings.DisabledValue,
        //    _ => _originalValue,
        //};
    }

}

