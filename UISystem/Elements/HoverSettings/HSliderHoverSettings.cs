using Godot;
using UISystem.Hovering;

namespace UISystem.Elements.HoverSettings;
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

    public IHoverTweener CreateTweener(Control grabberResizableControl, Control background, Control fill)
    {
        return new HSliderTweenerFacade(new TweeningSettings(duration, resetDuration, ease, resetEase, transition, resetTransition),
            grabberResizableControl, grabberSizeSettings, grabberPositionsSettings, grabberColorSettings,
            background, backgroundColorSettings,
            fill, fillColorSettings);
    }

    private class HSliderTweenerFacade : IHoverTweener
    {

        private readonly IHoverTweener[] _tweeners;

        public HSliderTweenerFacade(TweeningSettings transitionAndEaseSettings, 
            Control grabberResizableControl, SizeTweenSettings grabberSizeSettings, PositionTweenSettings grabberPositionsSettings, ColorTweenSettings grabberColorSettings,
            Control background, ColorTweenSettings backgroundColorSettings,
            Control fill, ColorTweenSettings fillColorSettings)
        {
            _tweeners = new IHoverTweener[] {
                grabberSizeSettings?.CreateTweener(grabberResizableControl, transitionAndEaseSettings),
                grabberPositionsSettings?.CreateTweener(grabberResizableControl, transitionAndEaseSettings),
                grabberColorSettings?.CreateTweener(grabberResizableControl, transitionAndEaseSettings),
                backgroundColorSettings?.CreateTweener(background, transitionAndEaseSettings),
                fillColorSettings?.CreateTweener(fill, transitionAndEaseSettings)
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
