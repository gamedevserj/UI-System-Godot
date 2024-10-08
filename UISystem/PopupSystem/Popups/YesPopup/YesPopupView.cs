using System;
using UISystem.Common.Helpers;
using UISystem.Core.PopupSystem.Views;

namespace UISystem.PopupSystem.Popups.YesPopup;
public partial class YesPopupView : PopupView
{

    public override void Hide(Action onHidden, bool instant = false)
    {
        Fader.Hide(GetTree(), this, onHidden, instant);
    }

    public override void Show(Action onShown, bool instant = false)
    {
        Fader.Show(GetTree(), this, onShown, instant);
    }

}
