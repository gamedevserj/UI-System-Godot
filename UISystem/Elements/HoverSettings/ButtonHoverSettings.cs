using Godot;
using UISystem.Hovering;

namespace UISystem.Elements.HoverSettings;

/// <summary>
/// Class containing settings for button states (normal/hover/focus/focus hovered/disabled).
/// </summary>
[GlobalClass]
public partial class ButtonHoverSettings : Resource
{
    [Export] private float duration = 1;
    [Export] private float resetDuration = 0.25f;
    [Export] private Tween.EaseType ease = Tween.EaseType.Out;
    [Export] private Tween.EaseType resetEase = Tween.EaseType.Out;
    [Export] private Tween.TransitionType transition = Tween.TransitionType.Elastic;
    [Export] private Tween.TransitionType resetTransition = Tween.TransitionType.Back;
    [Export] private SizeTweenSettings sizeChangeSettings;
    [Export] private PositionTweenSettings positionChangeSettings;
    [Export] private ColorTweenSettings borderColorChangeSettings;
    [Export] private ColorTweenSettings colorChangeSettings;
    [Export] private ColorTweenSettings labelColorChangeSettings;

    /// <summary>
    /// Creates tweener for the button.
    /// </summary>
    /// <param name="resizableControl">Control handling size.</param>
    /// <param name="colorTarget">Control handling color.</param>
    /// <param name="borderColorTarget">Control handling border color.</param>
    /// <param name="labelColorTarget">Control handling label color.</param>
    /// <returns>Instance of a class implementing IHoverTweener.</returns>
    public IHoverTweener CreateTweener(
        Control resizableControl,
        Control colorTarget,
        Control borderColorTarget,
        Control labelColorTarget)
    {
        return new ButtonTweenerFacade(
            new TweeningSettings(duration, resetDuration, ease, resetEase, transition, resetTransition),
            (resizableControl, sizeChangeSettings),
            (resizableControl, positionChangeSettings),
            (colorTarget, colorChangeSettings),
            (borderColorTarget, borderColorChangeSettings),
            (labelColorTarget, labelColorChangeSettings));
    }

    private sealed class ButtonTweenerFacade : IHoverTweener
    {
        private readonly IHoverTweener[] _tweeners;

        public ButtonTweenerFacade(
            TweeningSettings transitionAndEaseSettings,
            (Control Target, SizeTweenSettings Settings) sizeData,
            (Control Target, PositionTweenSettings Settings) positionData,
            (Control Target, ColorTweenSettings Settings) colorData,
            (Control Target, ColorTweenSettings Settings) borderColorData,
            (Control Target, ColorTweenSettings Settings) labelColorData)
        {
            _tweeners = new IHoverTweener[]
            {
                sizeData.Settings?.CreateTweener(sizeData.Target, transitionAndEaseSettings),
                positionData.Settings?.CreateTweener(positionData.Target, transitionAndEaseSettings),
                colorData.Settings?.CreateTweener(colorData.Target, transitionAndEaseSettings),
                borderColorData.Settings?.CreateTweener(borderColorData.Target, transitionAndEaseSettings),
                labelColorData.Settings?.CreateTweener(labelColorData.Target, transitionAndEaseSettings),
            };
        }

        public void Reset(Tween tween)
        {
            for (int i = 0; i < _tweeners.Length; i++)
            {
                _tweeners[i]?.Reset(tween);
            }
        }

        public void Tween(Tween tween, ControlDrawMode mode)
        {
            for (int i = 0; i < _tweeners.Length; i++)
            {
                _tweeners[i]?.Tween(tween, mode);
            }
        }
    }
}
