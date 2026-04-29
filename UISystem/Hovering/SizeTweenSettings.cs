using Godot;
using UISystem.Extensions;

namespace UISystem.Hovering;

/// <summary>
/// Settings for tweening size.
/// </summary>
[GlobalClass]
public partial class SizeTweenSettings : TweenSettings<Vector2>
{
    [Export] private Vector2 _changeSizeHover = new(0, 0);
    [Export] private Vector2 _changeSizeFocus = new(0, 0);
    [Export] private Vector2 _changeSizeFocusHover = new(0, 0);

    /// <inheritdoc/>
    protected override Vector2 NormalValue => Vector2.Zero;

    /// <inheritdoc/>
    protected override Vector2 HoverValue => _changeSizeHover;

    /// <inheritdoc/>
    protected override Vector2 FocusValue => _changeSizeFocus;

    /// <inheritdoc/>
    protected override Vector2 FocusHoverValue => _changeSizeFocusHover;

    /// <inheritdoc/>
    protected override Vector2 DisabledValue => Vector2.Zero;

    /// <inheritdoc/>
    public override IHoverTweener CreateTweener(Control target, TweeningSettings transitionAndEaseSettings, bool parallel = true)
        => new SizeTweener(target, target.Size, transitionAndEaseSettings, this, parallel);

    private sealed class SizeTweener : Tweener<Vector2>
    {
        private Vector2 _originalValue;

        public SizeTweener(
            Control target,
            Vector2 originalSize,
            TweeningSettings transitionAndEaseSettings,
            TweenSettings<Vector2> settings,
            bool parallel)
            : base(target, transitionAndEaseSettings, settings, parallel)
        {
            _originalValue = originalSize;
        }

        public override void Reset(Tween tween)
        {
            base.Reset(tween);
            tween.TweenControlSize(Target, _originalValue, TransitionAndEaseSettings.ResetDuration);
        }

        protected override void Tween(Tween tween, Vector2 value)
        {
            tween.TweenControlSize(Target, _originalValue + value, TransitionAndEaseSettings.Duration);
        }
    }
}
