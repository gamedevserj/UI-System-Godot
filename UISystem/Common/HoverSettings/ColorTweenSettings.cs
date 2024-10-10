using Godot;
using UISystem.Common.Extensions;
using UISystem.Common.Interfaces;

namespace UISystem.Common.HoverSettings;
[GlobalClass]
public partial class ColorTweenSettings : TweenSettings<Color>
{

    [Export] private Color normalColor = new(1, 1, 1, 1);
    [Export] private Color hoverColor = new(1, 1, 1, 1);
    [Export] private Color focusColor = new(1, 1, 1, 1);
    [Export] private Color focusHoverColor = new(1, 1, 1, 1);
    [Export] private Color disabledColor = new(0.5f, 0.5f, 0.5f, 1);

    public override Color NormalValue => normalColor;
    protected override Color HoverValue => hoverColor;
    protected override Color FocusValue => focusColor;
    protected override Color FocusHoverValue => focusHoverColor;
    protected override Color DisabledValue => disabledColor;

    public ITweener CreateTweener(Control target, TransitionAndEaseSettings transitionAndEaseSettings, bool parallel = true)
        => new ColorTweener(target, transitionAndEaseSettings, this, parallel);

    protected partial class ColorTweener : Tweener<Color>
    {

        public ColorTweener(Control target, TransitionAndEaseSettings transitionAndEaseSettings, 
            TweenSettings<Color> settings, bool parallel) 
            : base(target, transitionAndEaseSettings, settings, parallel)
        {
        }

        protected override void Tween(Tween tween, Color value)
        {
            base.Tween(tween, value);
            tween.TweenCanvasItemSelfModulate(_parallel, _target, value, _settings.Duration);
        }

        public override void Reset(Tween tween)
        {
            base.Reset(tween);
            tween.TweenCanvasItemSelfModulate(_parallel, _target, _settings.NormalValue, _settings.ResetDuration);
        }
    }

}
