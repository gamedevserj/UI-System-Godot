using Godot;
using System;
using UISystem.Constants;

namespace UISystem.ScreenFade;
public partial class ScreenFadeManager : TextureRect
{

    private const float Duration = 0.25f;

    private bool _isFading;

    public void FadeOut(Action onFadeOutComplete = null)
    {
        if (_isFading)
            return;

        _isFading = true;
        MouseFilter = MouseFilterEnum.Stop;
        Fade(1, () =>
        {
            onFadeOutComplete?.Invoke();
            FadeIn();
        });
    }

    private void Fade(float targetValue, Action onFadeOutComplete = null)
    {
        ShaderMaterial material = Material as ShaderMaterial;

        Tween tween = GetTree().CreateTween();
        tween.SetPauseMode(Tween.TweenPauseMode.Process);
        tween.TweenProperty(this, PropertyConstants.Modulate, new Color(Modulate, targetValue), Duration);
        tween.TweenCallback(Callable.From(() =>
        {
            onFadeOutComplete?.Invoke();
        }));
    }

    private void FadeIn()
    {
        Fade(0, () =>
        { 
            _isFading = false;
            MouseFilter = MouseFilterEnum.Ignore;
        });
    }

}
