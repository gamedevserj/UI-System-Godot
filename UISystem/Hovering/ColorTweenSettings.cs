using Godot;
using UISystem.Extensions;

namespace UISystem.Hovering;
[GlobalClass]
public partial class ColorTweenSettings : TweenSettings<Color>
{

    [Export] private Color hoverColor = new(1, 1, 1, 1);
    [Export] private Color focusColor = new(1, 1, 1, 1);
    [Export] private Color focusHoverColor = new(1, 1, 1, 1);
    [Export] private Color disabledColor = new(0.5f, 0.5f, 0.5f, 1);

    protected override Color HoverValue => hoverColor;
    protected override Color FocusValue => focusColor;
    protected override Color FocusHoverValue => focusHoverColor;
    protected override Color DisabledValue => disabledColor;

    public IHoverTweener CreateTweener(Control target, TweeningSettings transitionAndEaseSettings, bool parallel = true)
        => new ColorTweener(target, target.SelfModulate, transitionAndEaseSettings, this, parallel);

    protected partial class ColorTweener : Tweener<Color>
    {

        private Color _originalValue;
        protected override Color NormalValue => _originalValue;

        public ColorTweener(Control target, Color originalValue, TweeningSettings transitionAndEaseSettings, 
            TweenSettings<Color> settings, bool parallel) 
            : base(target, transitionAndEaseSettings, settings, parallel)
        {
            _originalValue = originalValue;
        }

        protected override void Tween(Tween tween, Color value)
        {
            base.Tween(tween, value);
            if(_parallel)
                tween.Parallel().TweenModulate(_target, value, _transitionAndEaseSettings.Duration, true);
            else
                tween.TweenModulate(_target, value, _transitionAndEaseSettings.Duration, true);
        }

        public override void Reset(Tween tween)
        {
            base.Reset(tween);
            if(_parallel)
                tween.Parallel().TweenModulate(_target, _originalValue, _transitionAndEaseSettings.ResetDuration, true);
            else
                tween.TweenModulate(_target, _originalValue, _transitionAndEaseSettings.ResetDuration, true);
        }

    }
}
