using Godot;
using UISystem.Common.Extensions;
using UISystem.Common.Interfaces;

namespace UISystem.Common.HoverSettings;
[GlobalClass]
public partial class ColorTweenSettings : TweenSettings<Color>
{

    [Export] private Color hoverColor = new(1, 1, 1, 1);
    [Export] private Color focusColor = new(1, 1, 1, 1);
    [Export] private Color focusHoverColor = new(1, 1, 1, 1);
    [Export] private Color disabledColor = new(0.5f, 0.5f, 0.5f, 1);

    protected override Color NormalValue => hoverColor; // is not used because we're replacing it with original color
    protected override Color HoverValue => hoverColor;
    protected override Color FocusValue => focusColor;
    protected override Color FocusHoverValue => focusHoverColor;
    protected override Color DisabledValue => disabledColor;

    public ITweener CreateTweener(Control target, TransitionAndEaseSettings transitionAndEaseSettings, bool parallel = true)
        => new ColorTweener(target, target.SelfModulate, transitionAndEaseSettings, this, parallel);

    protected partial class ColorTweener : Tweener<Color>
    {

        private Color _originalValue;
        protected override Color NormalValue => _originalValue;

        public ColorTweener(Control target, Color originalValue, TransitionAndEaseSettings transitionAndEaseSettings, 
            TweenSettings<Color> settings, bool parallel) 
            : base(target, transitionAndEaseSettings, settings, parallel)
        {
            _originalValue = originalValue;
        }

        protected override void Tween(Tween tween, Color value)
        {
            base.Tween(tween, value);
            tween.TweenCanvasItemSelfModulate(_parallel, _target, value, _transitionAndEaseSettings.Duration);
        }

        public override void Reset(Tween tween)
        {
            base.Reset(tween);
            tween.TweenCanvasItemSelfModulate(_parallel, _target, _originalValue, _transitionAndEaseSettings.ResetDuration);
        }
    }

}
