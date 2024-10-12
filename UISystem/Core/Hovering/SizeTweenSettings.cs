using Godot;
using UISystem.Core.Extensions;
using UISystem.Core.Interfaces;

namespace UISystem.Core.Hovering;
[GlobalClass]
public partial class SizeTweenSettings : TweenSettings<Vector2>
{

    [Export] private Vector2 changeSizeHover = new(0, 0);
    [Export] private Vector2 changeSizeFocus = new(0, 0);
    [Export] private Vector2 changeSizeFocusHover = new(0, 0);

    protected override Vector2 NormalValue => Vector2.Zero;
    protected override Vector2 HoverValue => changeSizeHover;
    protected override Vector2 FocusValue => changeSizeFocus;
    protected override Vector2 FocusHoverValue => changeSizeFocusHover;
    protected override Vector2 DisabledValue => Vector2.Zero;


    public IHoverTweener CreateTweener(Control target, TweeningSettings transitionAndEaseSettings, bool parallel = true)
        => new SizeTweener(target, target.Size, transitionAndEaseSettings, this, parallel);

    private class SizeTweener : Tweener<Vector2>
    {

        private Vector2 _originalValue;

        public SizeTweener(Control target, Vector2 originalSize, TweeningSettings transitionAndEaseSettings,
            TweenSettings<Vector2> settings, bool parallel)
            : base(target, transitionAndEaseSettings, settings, parallel)
        {
            _originalValue = originalSize;
        }

        protected override void Tween(Tween tween, Vector2 value)
        {
            base.Tween(tween, value);
            if (_parallel)
                tween.Parallel().TweenControlSize(_target, _originalValue + value, _transitionAndEaseSettings.Duration);
            else
                tween.TweenControlSize(_target, _originalValue + value, _transitionAndEaseSettings.Duration);
        }

        public override void Reset(Tween tween)
        {
            base.Reset(tween);
            if (_parallel)
                tween.Parallel().TweenControlSize(_target, _originalValue, _transitionAndEaseSettings.ResetDuration);
            else
                tween.TweenControlSize(_target, _originalValue, _transitionAndEaseSettings.ResetDuration);
        }

    }
}

