using Godot;
using UISystem.Common.Enums;

namespace UISystem.Common.Resources;
public abstract partial class TweenSettings<T> : Resource
{

    [Export] private float duration = 1;
    [Export] private Tween.EaseType ease = Tween.EaseType.Out;
    [Export] private Tween.TransitionType transition = Tween.TransitionType.Elastic;

    public float Duration => duration;
    public Tween.EaseType Ease => ease;
    public Tween.TransitionType Transition => transition;

    public abstract T HoverValue { get; }
    public abstract T FocusValue { get; }
    public abstract T FocusHoverValue { get; }
    public abstract T DisabledValue { get; }


    protected abstract class Tweener<T>
    {

        protected Tween _tween;
        protected T _originalValue;

        protected readonly SceneTree _tree;
        protected readonly Control _target;
        protected readonly bool _parallel;
        protected readonly TweenSettings<T> _settings;

        public Tweener(SceneTree tree, Control target, bool parallel, TweenSettings<T> settings, T originalValue)
        {
            _tree = tree;
            _target = target;
            _parallel = parallel;
            _settings = settings;
            _originalValue = originalValue;
        }

        protected abstract void Tween(T value);

        public void OnFocusEntered(ControlDrawMode mode)
        {
            Tween(SelectValue(mode));
        }

        public void OnFocusExited(ControlDrawMode mode)
        {
            Tween(SelectValue(mode));
        }

        public void OnMouseEntered(ControlDrawMode mode)
        {
            Tween(SelectValue(mode));
        }

        public void OnMouseExited(ControlDrawMode mode)
        {
            Tween(SelectValue(mode));
        }

        private T SelectValue(ControlDrawMode mode) => mode switch
        {
            ControlDrawMode.Normal => _originalValue,
            ControlDrawMode.Hover => _settings.HoverValue,
            ControlDrawMode.Focus => _settings.FocusValue,
            ControlDrawMode.HoverFocus => _settings.FocusHoverValue,
            ControlDrawMode.Disabled => _settings.DisabledValue,
            _ => _originalValue,
        };
    }

}
