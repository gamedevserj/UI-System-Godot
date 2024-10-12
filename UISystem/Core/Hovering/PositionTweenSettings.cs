using Godot;
using UISystem.Core.Enums;
using UISystem.Core.Extensions;
using UISystem.Core.Interfaces;

namespace UISystem.Core.Hovering;
[GlobalClass]
public partial class PositionTweenSettings : TweenSettings<Vector2>
{

    [Export] private Vector2 changePositionHover = new(0, 0);
    [Export] private Vector2 changePositionFocus = new(0, 0);
    [Export] private Vector2 changePositionFocusHover = new(0, 0);

    protected override Vector2 NormalValue => Vector2.Zero;
    protected override Vector2 HoverValue => changePositionHover;
    protected override Vector2 FocusValue => changePositionFocus;
    protected override Vector2 FocusHoverValue => changePositionFocusHover;
    protected override Vector2 DisabledValue => Vector2.Zero;

    public IHoverTweener CreateTweener(Control target, TweeningSettings transitionAndEaseSettings, bool parallel = true) =>
        new PositionTweener(target, target.Position, transitionAndEaseSettings, this, parallel);

    private class PositionTweener : Tweener<Vector2>
    {

        private Vector2 _originalValue;

        public PositionTweener(Control target, Vector2 originalValue, TweeningSettings transitionAndEaseSettings,
            TweenSettings<Vector2> settings, bool parallel)
            : base(target, transitionAndEaseSettings, settings, parallel)
        {
            _originalValue = originalValue;
        }

        protected override void Tween(Tween tween, Vector2 value)
        {
            base.Tween(tween, value);
            if (_parallel) 
                tween.Parallel().TweenControlPosition(_target, _originalValue + value, _transitionAndEaseSettings.Duration);
            else
                tween.TweenControlPosition(_target, _originalValue + value, _transitionAndEaseSettings.Duration);
        }

        public override void Reset(Tween tween)
        {
            base.Reset(tween);
            if (_parallel)
                tween.Parallel().TweenControlPosition(_target, _originalValue, _transitionAndEaseSettings.ResetDuration);
            else
                tween.TweenControlPosition(_target, _originalValue, _transitionAndEaseSettings.ResetDuration);
        }

    }
}
