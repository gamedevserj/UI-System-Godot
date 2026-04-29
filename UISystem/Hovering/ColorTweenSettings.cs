using Godot;
using UISystem.Extensions;

namespace UISystem.Hovering;

/// <summary>
/// Settings for tweening color.
/// </summary>
[GlobalClass]
public partial class ColorTweenSettings : TweenSettings<Color>
{
    [Export] private Color _hoverColor = new(1, 1, 1, 1);
    [Export] private Color _focusColor = new(1, 1, 1, 1);
    [Export] private Color _focusHoverColor = new(1, 1, 1, 1);
    [Export] private Color _disabledColor = new(0.5f, 0.5f, 0.5f, 1);

    /// <inheritdoc/>
    protected override Color HoverValue => _hoverColor;

    /// <inheritdoc/>
    protected override Color FocusValue => _focusColor;

    /// <inheritdoc/>
    protected override Color FocusHoverValue => _focusHoverColor;

    /// <inheritdoc/>
    protected override Color DisabledValue => _disabledColor;

    /// <inheritdoc/>
    public override IHoverTweener CreateTweener(Control target, TweeningSettings transitionAndEaseSettings, bool parallel = true)
        => new ColorTweener(target, target.SelfModulate, transitionAndEaseSettings, this, parallel);

    private sealed class ColorTweener : Tweener<Color>
    {
        private Color _originalValue;

        public ColorTweener(
            Control target,
            Color originalValue,
            TweeningSettings transitionAndEaseSettings,
            TweenSettings<Color> settings,
            bool parallel)
            : base(target, transitionAndEaseSettings, settings, parallel)
        {
            _originalValue = originalValue;
        }

        /// <inheritdoc/>
        protected override Color NormalValue => _originalValue;

        /// <inheritdoc/>
        public override void Reset(Tween tween)
        {
            base.Reset(tween);
            tween.TweenModulate(Target, _originalValue, TransitionAndEaseSettings.ResetDuration, true);
        }

        /// <inheritdoc/>
        protected override void Tween(Tween tween, Color value)
        {
            tween.TweenModulate(Target, value, TransitionAndEaseSettings.Duration, true);
        }
    }
}
