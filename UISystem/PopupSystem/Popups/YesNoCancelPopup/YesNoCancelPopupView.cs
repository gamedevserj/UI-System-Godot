using Godot;
using System;
using UISystem.Common.ElementViews;
using UISystem.Common.Helpers;
using UISystem.Core.Common.Interfaces;
using UISystem.PopupSystem.Popups.YesPopup;

namespace UISystem.PopupSystem.Views;
public partial class YesNoCancelPopupView : YesPopupView
{

    [Export] private ButtonView noButton;
    [Export] private ButtonView cancelButton;

    public ButtonView NoButton => noButton;
    public ButtonView CancelButton => cancelButton;

    public override IFocusableControl DefaultSelectedElement => CancelButton;

    public override void Hide(Action onHidden, bool instant = false)
    {
        Fader.Hide(GetTree(), this, onHidden, instant);
    }

    public override void Show(Action onShown, bool instant = false)
    {
        Fader.Show(GetTree(), this, onShown, instant);
    }

    protected override void PopulateFocusableElements()
    {
        _focusableElements = new IFocusableControl[] { YesButton, NoButton, CancelButton };
    }
}
