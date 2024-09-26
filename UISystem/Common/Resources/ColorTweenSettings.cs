using Godot;
using UISystem.Common.Interfaces;
using UISystem.Extensions;

namespace UISystem.Common.Resources;
[GlobalClass]
public partial class ColorTweenSettings : TweenSettings
{

    [Export] private Color hoverColor = new(1,1,1,1);
    [Export] private Color focusColor = new(1, 1, 1, 1);

    public Color HoverColor => hoverColor;
    public Color FocusColor => focusColor;

    public ITweener CreateTweener(SceneTree tree, Control target, bool parallel = true)
        => new ColorTweener(tree, target, parallel, this);

    private class ColorTweener : ITweener
    {

        private Tween _tween;

        private readonly SceneTree _tree;
        private readonly Control _target;
        private readonly bool _parallel;
        private readonly Color _originalColor;
        private readonly ColorTweenSettings _settings;

        public ColorTweener(SceneTree tree, Control target, bool parallel, ColorTweenSettings settings)
        {
            _tree = tree;
            _target = target;
            _parallel = parallel;
            _settings = settings;
            _originalColor = target.SelfModulate;
        }

        public void OnFocusEntered() => Tween(_settings.FocusColor);

        public void OnFocusExited() => Tween(_originalColor);

        public void OnMouseEntered() => Tween(_settings.HoverColor);

        public void OnMouseExited() => Tween(_originalColor);

        private void Tween(Color color)
        {
            _tween?.Kill();
            _tween = _tree.CreateTween();
            _tween.SetEase(_settings.Ease);
            _tween.SetTrans(_settings.Transition);
            _tween.TweenCanvasItemSelfModulate(_parallel, _target, color, _settings.Duration);
        }
    }

}
