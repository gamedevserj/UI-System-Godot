using Godot;
using UISystem.Common.Interfaces;
using UISystem.Extensions;

namespace UISystem.Common.Resources;
[GlobalClass]
public partial class ColorTweenSettings : TweenSettings<Color>
{

    [Export] private Color hoverColor = new(1, 1, 1, 1);
    [Export] private Color focusColor = new(1, 1, 1, 1);
    [Export] private Color focusHoverColor = new(1, 1, 1, 1);
    [Export] private Color disabledColor = new(0.5f, 0.5f, 0.5f, 1);

    public override Color HoverValue => hoverColor;
    public override Color FocusValue => focusColor;
    public override Color FocusHoverValue => focusHoverColor;
    public override Color DisabledValue => disabledColor;

    public ITweener CreateTweener(SceneTree tree, Control target, bool parallel = true)
        => new ColorTweener(tree, target, parallel, this, target.SelfModulate);

    protected partial class ColorTweener : Tweener<Color>, ITweener
    {

        public ColorTweener(SceneTree tree, Control target, bool parallel, TweenSettings<Color> settings, Color originalValue) : base(tree, target, parallel, settings, originalValue)
        {
        }

        protected override void Tween(Color value)
        {
            _tween?.Kill();
            _tween = _tree.CreateTween();
            _tween.SetEase(_settings.Ease);
            _tween.SetTrans(_settings.Transition);
            _tween.TweenCanvasItemSelfModulate(_parallel, _target, value, _settings.Duration);
        }
    }

}
