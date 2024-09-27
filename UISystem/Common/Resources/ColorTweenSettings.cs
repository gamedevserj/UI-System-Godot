using Godot;
using UISystem.Common.Enums;
using UISystem.Common.Interfaces;
using UISystem.Extensions;

namespace UISystem.Common.Resources;
[GlobalClass]
public partial class ColorTweenSettings : TweenSettings
{

    [Export] private Color hoverColor = new(1, 1, 1, 1);
    [Export] private Color focusColor = new(1, 1, 1, 1);
    [Export] private Color focusHoverColor = new(1, 1, 1, 1);
    [Export] private Color disabledColor = new(0.5f, 0.5f, 0.5f, 1);

    public ITweener CreateTweener(SceneTree tree, Control target, bool parallel = true)
        => new ColorTweener(tree, target, parallel, this);

    private class ColorTweener : ITweener
    {

        private Tween _tween;
        private bool _mouseOver;
        private bool _hasFocus;

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

        public void OnFocusEntered(ControlDrawMode mode)
        {
            _hasFocus = true;
            Tween(SelectColor(mode));
        }

        public void OnFocusExited(ControlDrawMode mode)
        {
            _hasFocus = false;
            Tween(SelectColor(mode));
        }

        public void OnMouseEntered(ControlDrawMode mode)
        {
            _mouseOver = true;
            Tween(SelectColor(mode));
        }

        public void OnMouseExited(ControlDrawMode mode)
        {
            _mouseOver = false;
            Tween(SelectColor(mode));
        }

        private void Tween(Color color)
        {
            _tween?.Kill();
            _tween = _tree.CreateTween();
            _tween.SetEase(_settings.Ease);
            _tween.SetTrans(_settings.Transition);
            _tween.TweenCanvasItemSelfModulate(_parallel, _target, color, _settings.Duration);
        }

        private Color SelectColor(ControlDrawMode mode) => mode switch
        {
            ControlDrawMode.Normal => _originalColor,
            ControlDrawMode.Hover => _settings.hoverColor,
            ControlDrawMode.Focus => _settings.focusColor,
            ControlDrawMode.HoverFocus => _settings.focusHoverColor,
            ControlDrawMode.Disabled => _settings.disabledColor,
            _ => _originalColor,
        };

    }

}
