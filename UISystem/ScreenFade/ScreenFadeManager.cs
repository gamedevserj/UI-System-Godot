using Godot;
using System;
using UISystem.Helpers;

namespace UISystem.ScreenFade;
public partial class ScreenFadeManager : TextureRect
{

    private bool _isFading;

    public void FadeOut(Action onFadeOutComplete = null)
    {
        if (_isFading)
            return;

        _isFading = true;
        MouseFilter = MouseFilterEnum.Stop;

        Fader.Show(GetTree(), this, () =>
        {
            onFadeOutComplete?.Invoke();

            Fader.Hide(GetTree(), this, () =>
            {
                _isFading = false;
                MouseFilter = MouseFilterEnum.Ignore;
            });
        });
    }

}
