using Godot;
using UISystem.Extensions;

namespace UISystem.Hovering;

/// <summary>
/// Settings for tweening position.
/// </summary>
[GlobalClass]
public partial class PositionTweenSettings : TweenSettings<Vector2>
{
    [Export] private Vector2 _changePositionHover = new(0, 0);
    [Export] private Vector2 _changePositionFocus = new(0, 0);
    [Export] private Vector2 _changePositionFocusHover = new(0, 0);

    /// <inheritdoc/>
    protected override Vector2 NormalValue => Vector2.Zero;

    /// <inheritdoc/>
    protected override Vector2 HoverValue => _changePositionHover;

    /// <inheritdoc/>
    protected override Vector2 FocusValue => _changePositionFocus;

    /// <inheritdoc/>
    protected override Vector2 FocusHoverValue => _changePositionFocusHover;

    /// <inheritdoc/>
    protected override Vector2 DisabledValue => Vector2.Zero;

    /// <inheritdoc/>
    public override IHoverTweener CreateTweener(Control target, TweeningSettings transitionAndEaseSettings, bool parallel = true) =>
        new PositionTweener(target, target.Position, transitionAndEaseSettings, this, parallel);

    private sealed class PositionTweener : Tweener<Vector2>
    {
        private Vector2 _originalValue;

        public PositionTweener(
            Control target,
            Vector2 originalValue,
            TweeningSettings transitionAndEaseSettings,
            TweenSettings<Vector2> settings,
            bool parallel)
            : base(target, transitionAndEaseSettings, settings, parallel)
        {
            _originalValue = originalValue;
        }

        public override void Reset(Tween tween)
        {
            base.Reset(tween);
            tween.TweenControlPosition(Target, _originalValue, TransitionAndEaseSettings.ResetDuration);
        }

        protected override void Tween(Tween tween, Vector2 value)
        {
            tween.TweenControlPosition(Target, _originalValue + value, TransitionAndEaseSettings.Duration);
        }
    }
}
