using Godot;
using System;
using UISystem.Common.ElementViews;
using UISystem.Common.Helpers;
using UISystem.Core.Common.Interfaces;
using UISystem.Core.PopupSystem.Views;

namespace UISystem.PopupSystem.Popups.YesPopup;
public partial class YesPopupView : PopupView
{

    [Export] protected ButtonView yesButton;

    public ButtonView YesButton => yesButton;
    public override IFocusableControl DefaultSelectedElement => YesButton;

    protected override void PopulateFocusableElements()
    {
        _focusableElements = new IFocusableControl[] { YesButton };
    }

    public override void Hide(Action onHidden, bool instant = false)
    {
        Fader.Hide(GetTree(), this, onHidden, instant);
    }

    public override void Show(Action onShown, bool instant = false)
    {
        Fader.Show(GetTree(), this, onShown, instant);
    }

}
