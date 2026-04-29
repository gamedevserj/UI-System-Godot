using Godot;
using UISystem.Hovering;

namespace UISystem.Elements.HoverSettings;

/// <summary>
/// Class containing settings for horizontal slized states (normal/hover/focus/focus hovered/disabled).
/// </summary>
[GlobalClass]
public partial class HSliderHoverSettings : Resource
{
    [Export] private float duration = 1;
    [Export] private float resetDuration = 0.25f;
    [Export] private Tween.EaseType ease = Tween.EaseType.Out;
    [Export] private Tween.EaseType resetEase = Tween.EaseType.Out;
    [Export] private Tween.TransitionType transition = Tween.TransitionType.Elastic;
    [Export] private Tween.TransitionType resetTransition = Tween.TransitionType.Back;
    [Export] private SizeTweenSettings grabberSizeSettings;
    [Export] private PositionTweenSettings grabberPositionsSettings;
    [Export] private ColorTweenSettings grabberColorSettings;
    [Export] private ColorTweenSettings backgroundColorSettings;
    [Export] private ColorTweenSettings fillColorSettings;

    /// <summary>
    /// Creates tweener for the slider.
    /// </summary>
    /// <param name="grabberResizableControl">Resizable control of the grabber.</param>
    /// <param name="background">Background control.</param>
    /// <param name="fill">Fill control.</param>
    /// <returns>Instance of a class implementing IHoverTweener.</returns>
    public IHoverTweener CreateTweener(Control grabberResizableControl, Control background, Control fill)
    {
        return new HSliderTweenerFacade(
            new TweeningSettings(duration, resetDuration, ease, resetEase, transition, resetTransition),
            (grabberResizableControl, grabberSizeSettings, grabberPositionsSettings, grabberColorSettings),
            (background, backgroundColorSettings),
            (fill, fillColorSettings));
    }

    private sealed class HSliderTweenerFacade : IHoverTweener
    {
        private readonly IHoverTweener[] _tweeners;

        public HSliderTweenerFacade(
            TweeningSettings transitionAndEaseSettings,
            (Control Target,
            SizeTweenSettings SizeSettings,
            PositionTweenSettings PositionSettings,
            ColorTweenSettings ColorSettings) grabberData,
            (Control Target, ColorTweenSettings ColorSettings) backgroundData,
            (Control Target, ColorTweenSettings ColorSettings) fillData)
        {
            _tweeners = new IHoverTweener[]
            {
                grabberData.SizeSettings?.CreateTweener(grabberData.Target, transitionAndEaseSettings),
                grabberData.PositionSettings?.CreateTweener(grabberData.Target, transitionAndEaseSettings),
                grabberData.ColorSettings?.CreateTweener(grabberData.Target, transitionAndEaseSettings),
                backgroundData.ColorSettings?.CreateTweener(backgroundData.Target, transitionAndEaseSettings),
                fillData.ColorSettings?.CreateTweener(fillData.Target, transitionAndEaseSettings),
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
