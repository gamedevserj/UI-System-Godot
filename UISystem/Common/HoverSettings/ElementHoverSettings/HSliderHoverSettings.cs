using Godot;
using UISystem.Common.Enums;
using UISystem.Common.Interfaces;

namespace UISystem.Common.HoverSettings.ElementHoverSettings;
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

    public ITweener CreateTweener(Control grabberResizableControl, Control background, Control fill, bool sizeParallel = true,
        bool colorParallel = true)
    {
        return new HSliderTweenerFacade(new TweeningSettings(duration, resetDuration, ease, resetEase, transition, resetTransition),
            grabberResizableControl, background, fill,
            grabberSizeSettings, grabberColorSettings, grabberPositionsSettings, backgroundColorSettings, fillColorSettings,
            sizeParallel, colorParallel);
    }

    private class HSliderTweenerFacade : ITweener
    {

        private readonly ITweener[] _tweeners;

        public HSliderTweenerFacade(TweeningSettings transitionAndEaseSettings, Control grabberResizableControl, Control background, Control fill,
            SizeTweenSettings grabberSizeSettings, ColorTweenSettings grabberColorSettings, PositionTweenSettings grabberPositionsSettings,
            ColorTweenSettings backgroundColorSettings, ColorTweenSettings fillColorSettings,
            bool sizeParallel = true, bool colorParallel = true)
        {
            _tweeners = new ITweener[] {
                grabberSizeSettings?.CreateTweener(grabberResizableControl, transitionAndEaseSettings, sizeParallel),
                grabberPositionsSettings?.CreateTweener(grabberResizableControl, transitionAndEaseSettings, true),
                grabberColorSettings?.CreateTweener(grabberResizableControl, transitionAndEaseSettings, colorParallel),
                backgroundColorSettings?.CreateTweener(background, transitionAndEaseSettings, colorParallel),
                fillColorSettings?.CreateTweener(fill, transitionAndEaseSettings, colorParallel)
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
