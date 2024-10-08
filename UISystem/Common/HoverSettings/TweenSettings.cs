using Godot;
using UISystem.Common.Enums;
using UISystem.Common.Interfaces;

namespace UISystem.Common.HoverSettings;
public abstract partial class TweenSettings<T> : Resource
{

    [Export] private float duration = 1;
    [Export] private float resetDuration = 0.25f;
    [Export] private Tween.EaseType ease = Tween.EaseType.Out;
    [Export] private Tween.TransitionType transition = Tween.TransitionType.Elastic;
    [Export] private Tween.TransitionType resetTransition = Tween.TransitionType.Back;

    public float Duration => duration;
    public float ResetDuration => resetDuration;

    protected abstract T HoverValue { get; }
    protected abstract T FocusValue { get; }
    protected abstract T FocusHoverValue { get; }
    protected abstract T DisabledValue { get; }


    protected abstract class Tweener<T> : ITweener
    {

        protected T _originalValue;

        protected readonly Control _target;
        protected readonly bool _parallel;
        protected readonly TweenSettings<T> _settings;

        public Tweener(Control target, bool parallel, TweenSettings<T> settings, T originalValue)
        {
            _target = target;
            _parallel = parallel;
            _settings = settings;
            _originalValue = originalValue;
        }

        public void Tween(Tween tween, ControlDrawMode mode)
        {
            Tween(tween, SelectValue(mode));
        }

        public virtual void Reset(Tween tween)
        {
            tween.SetEase(_settings.ease).SetTrans(_settings.resetTransition);
        }

        protected virtual void Tween(Tween tween, T value)
        {
            tween.SetEase(_settings.ease).SetTrans(_settings.transition);
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
