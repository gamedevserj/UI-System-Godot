using System;
using UISystem.Common.Helpers;
using UISystem.PopupSystem.Views;

namespace UISystem.PopupSystem.Popups.InformationPopup;
public partial class InformationPopupView : PopupView
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
