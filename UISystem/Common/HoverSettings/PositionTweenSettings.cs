using Godot;
using UISystem.Common.Extensions;
using UISystem.Common.Interfaces;

namespace UISystem.Common.HoverSettings;
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

    public ITweener CreateTweener(Control target, TransitionAndEaseSettings transitionAndEaseSettings, bool parallel = true) =>
        new PositionTweener(target, target.Position, transitionAndEaseSettings, this, parallel);

    private class PositionTweener : Tweener<Vector2>
    {

        private Vector2 _originalValue;

        public PositionTweener(Control target, Vector2 originalValue, TransitionAndEaseSettings transitionAndEaseSettings,
            TweenSettings<Vector2> settings, bool parallel)
            : base(target, transitionAndEaseSettings, settings, parallel)
        {
            _originalValue = originalValue;
        }

        protected override void Tween(Tween tween, Vector2 value)
        {
            base.Tween(tween, value);
            tween.TweenControlPosition(_parallel, _target, _originalValue + value, _transitionAndEaseSettings.Duration);
        }

        public override void Reset(Tween tween)
        {
            base.Reset(tween);
            tween.TweenControlPosition(_parallel, _target, _originalValue, _transitionAndEaseSettings.ResetDuration);
        }
    }

}
