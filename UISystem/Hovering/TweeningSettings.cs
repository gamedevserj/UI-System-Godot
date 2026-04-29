using static Godot.Tween;

namespace UISystem.Hovering;

/// <summary>
/// Stores tweening settings.
/// </summary>
public readonly struct TweeningSettings
{
    /// <summary>
    /// Tween duration.
    /// </summary>
    public readonly float Duration = 1f;

    /// <summary>
    /// Reset tween duration.
    /// </summary>
    public readonly float ResetDuration = 0.25f;

    /// <summary>
    /// Tween ease.
    /// </summary>
    public readonly EaseType Ease = EaseType.Out;

    /// <summary>
    /// Reset tween ease.
    /// </summary>
    public readonly EaseType ResetEase = EaseType.Out;

    /// <summary>
    /// Tween transition.
    /// </summary>
    public readonly TransitionType Transition = TransitionType.Elastic;

    /// <summary>
    /// Reset tween transition.
    /// </summary>
    public readonly TransitionType ResetTransition = TransitionType.Back;

    /// <summary>
    /// Initializes a new instance of the <see cref="TweeningSettings"/> struct.
    /// </summary>
    /// <param name="duration">Tween duration.</param>
    /// <param name="resetDuration">Tween reset duration.</param>
    /// <param name="ease">Easy type.</param>
    /// <param name="resetEase">Reset ease type.</param>
    /// <param name="transition">Transition type.</param>
    /// <param name="resetTransition">Reset transition type.</param>
    public TweeningSettings(
        float duration,
        float resetDuration,
        EaseType ease,
        EaseType resetEase,
        TransitionType transition,
        TransitionType resetTransition)
    {
        Duration = duration;
        ResetDuration = resetDuration;
        Ease = ease;
        ResetEase = resetEase;
        Transition = transition;
        ResetTransition = resetTransition;
    }
}
