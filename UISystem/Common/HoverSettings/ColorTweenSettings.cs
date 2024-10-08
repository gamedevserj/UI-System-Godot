using Godot;
using UISystem.Common.Interfaces;
using UISystem.Core.Extensions;

namespace UISystem.Common.HoverSettings;
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

    public ITweener CreateTweener(Control target, bool parallel = true)
        => new ColorTweener(target, parallel, this, target.SelfModulate);

    protected partial class ColorTweener : Tweener<Color>
    {

        public ColorTweener(Control target, bool parallel, TweenSettings<Color> settings, Color originalValue) : base(target, parallel, settings, originalValue)
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
            tween.TweenCanvasItemSelfModulate(_parallel, _target, _originalValue, _settings.ResetDuration);
        }
    }

}
