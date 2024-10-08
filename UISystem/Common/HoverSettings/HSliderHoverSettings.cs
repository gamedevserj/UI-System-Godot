using Godot;
using UISystem.Common.Enums;
using UISystem.Common.Interfaces;

namespace UISystem.Common.HoverSettings;
[GlobalClass]
public partial class HSliderHoverSettings : Resource
{

    [Export] private SizeTweenSettings grabberSizeSettings;
    [Export] private ColorTweenSettings grabberColorSettings;
    [Export] private ColorTweenSettings backgroundColorSettings;
    [Export] private ColorTweenSettings fillColorSettings;

    public ITweener CreateTweener(Control grabberResizableControl, Control background, Control fill, bool sizeParallel = true,
        bool colorParallel = true)
    {
        return new HSliderTweenerFacade(grabberResizableControl, background, fill,
            grabberSizeSettings, grabberColorSettings, backgroundColorSettings, fillColorSettings,
            sizeParallel, colorParallel);
    }

    private class HSliderTweenerFacade : ITweener
    {

        private readonly ITweener[] _tweeners;

        public HSliderTweenerFacade(Control grabberResizableControl, Control background, Control fill,
            SizeTweenSettings grabberSizeSettings, ColorTweenSettings grabberColorSettings,
            ColorTweenSettings backgroundColorSettings, ColorTweenSettings fillColorSettings,
            bool sizeParallel = true, bool colorParallel = true)
        {
            _tweeners = new ITweener[] {
                grabberSizeSettings?.CreateTweener(grabberResizableControl, sizeParallel),
                grabberColorSettings?.CreateTweener(grabberResizableControl, colorParallel),
                backgroundColorSettings?.CreateTweener(background, colorParallel),
                fillColorSettings?.CreateTweener(fill, colorParallel)
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
