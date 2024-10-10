using Godot;
using UISystem.Common.Enums;
using UISystem.Common.Interfaces;

namespace UISystem.Common.HoverSettings;
public abstract partial class TweenSettings<T> : Resource
{

    [Export] private float duration = 1;
    [Export] private float resetDuration = 0.25f;

    public float Duration => duration;
    public float ResetDuration => resetDuration;

    public abstract T NormalValue { get; }
    protected abstract T HoverValue { get; }
    protected abstract T FocusValue { get; }
    protected abstract T FocusHoverValue { get; }
    protected abstract T DisabledValue { get; }


    protected abstract class Tweener<T> : ITweener
    {

        protected readonly Control _target;
        protected readonly bool _parallel;
        protected readonly TweenSettings<T> _settings;
        protected readonly TransitionAndEaseSettings _transitionAndEaseSettings;

        public Tweener(Control target, TransitionAndEaseSettings transitionAndEaseSettings, TweenSettings<T> settings,
            bool parallel)
        {
            _target = target;
            _parallel = parallel;
            _settings = settings;
            _transitionAndEaseSettings = transitionAndEaseSettings;
        }

        public void Tween(Tween tween, ControlDrawMode mode)
        {
            Tween(tween, SelectValue(mode));
        }

        public virtual void Reset(Tween tween)
        {
            tween.SetEase(_transitionAndEaseSettings.ResetEase).SetTrans(_transitionAndEaseSettings.ResetTransition);
        }

        protected virtual void Tween(Tween tween, T value)
        {
            tween.SetEase(_transitionAndEaseSettings.Ease).SetTrans(_transitionAndEaseSettings.Transition);
        }

        protected virtual T SelectValue(ControlDrawMode mode) => mode switch
        {
            ControlDrawMode.Normal => _settings.NormalValue,
            ControlDrawMode.Hover => _settings.HoverValue,
            ControlDrawMode.Focus => _settings.FocusValue,
            ControlDrawMode.HoverFocus => _settings.FocusHoverValue,
            ControlDrawMode.Disabled => _settings.DisabledValue,
            _ => _settings.NormalValue,
        };
    }

}
