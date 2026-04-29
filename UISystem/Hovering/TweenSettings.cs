using Godot;

namespace UISystem.Hovering;

/// <summary>
/// Generic class for tween settings.
/// </summary>
/// <typeparam name="T">Type of data to tween (Color, Vector2 etc.).</typeparam>
public abstract partial class TweenSettings<T> : Resource
{
    /// <summary>
    /// Gets value when control is in normal state.
    /// </summary>
    protected virtual T NormalValue { get; }

    /// <summary>
    /// Gets value when control is hovered.
    /// </summary>
    protected abstract T HoverValue { get; }

    /// <summary>
    /// Gets value when control is in focus.
    /// </summary>
    protected abstract T FocusValue { get; }

    /// <summary>
    /// Gets value when control is in focus and being hovered over.
    /// </summary>
    protected abstract T FocusHoverValue { get; }

    /// <summary>
    /// Gets value when control is in disabled state.
    /// </summary>
    protected abstract T DisabledValue { get; }

    /// <summary>
    /// Creates tweener.
    /// </summary>
    /// <param name="target">Target control.</param>
    /// <param name="transitionAndEaseSettings">Transition and ease settings.</param>
    /// <param name="parallel">Whether tween should run in parallel.</param>
    /// <returns>Instance of a class implementing IHoverTweener.</returns>
    public abstract IHoverTweener CreateTweener(Control target, TweeningSettings transitionAndEaseSettings, bool parallel = true);

    /// <summary>
    /// Base class for tweener that will control tweening of the data.
    /// </summary>
    /// <typeparam name="TData">Type of data to tween (Color, Vector2 etc.).</typeparam>
    protected abstract class Tweener<TData> : IHoverTweener
    {
        private readonly bool _parallel;

        /// <summary>
        /// Initializes a new instance of the <see cref="Tweener{TData}"/> class.
        /// </summary>
        /// <param name="target">Target control.</param>
        /// <param name="transitionAndEaseSettings">Transition and ease settings.</param>
        /// <param name="settings">Tween settings.</param>
        /// <param name="parallel">Whether tween should run in parallel.</param>
        protected Tweener(
            Control target,
            TweeningSettings transitionAndEaseSettings,
            TweenSettings<TData> settings,
            bool parallel)
        {
            Target = target;
            Settings = settings;
            TransitionAndEaseSettings = transitionAndEaseSettings;
            _parallel = parallel;
        }

        /// <summary>
        /// Gets the target control.
        /// </summary>
        protected Control Target { get; private set; }

        /// <summary>
        /// Gets the tween settings.
        /// </summary>
        protected TweenSettings<TData> Settings { get; private set; }

        /// <summary>
        /// Gets the transition and ease settings.
        /// </summary>
        protected TweeningSettings TransitionAndEaseSettings { get; private set; }

        /// <summary>
        /// Gets the value for normal state.
        /// </summary>
        protected virtual TData NormalValue => Settings.NormalValue;

        /// <inheritdoc/>
        public void Tween(Tween tween, ControlDrawMode mode)
        {
            tween.SetEase(TransitionAndEaseSettings.Ease).SetTrans(TransitionAndEaseSettings.Transition);
            if (_parallel)
                tween.Parallel();

            Tween(tween, SelectValue(mode));
        }

        /// <inheritdoc/>
        public virtual void Reset(Tween tween)
        {
            if (_parallel)
                tween.Parallel();
            tween.SetEase(TransitionAndEaseSettings.ResetEase).SetTrans(TransitionAndEaseSettings.ResetTransition);
        }

        /// <summary>
        /// Base method to tween value.
        /// </summary>
        /// <param name="tween">Target tween.</param>
        /// <param name="value">Value to tween.</param>
        protected abstract void Tween(Tween tween, TData value);

        /// <summary>
        /// Gets the value based on the control drawing mode.
        /// </summary>
        /// <param name="mode">Control drawing mode.</param>
        /// <returns>Value based on the control drawing mode.</returns>
        protected TData SelectValue(ControlDrawMode mode) => mode switch
        {
            ControlDrawMode.Normal => NormalValue,
            ControlDrawMode.Hover => Settings.HoverValue,
            ControlDrawMode.Focus => Settings.FocusValue,
            ControlDrawMode.HoverFocus => Settings.FocusHoverValue,
            ControlDrawMode.Disabled => Settings.DisabledValue,
            _ => Settings.NormalValue,
        };
    }
}
