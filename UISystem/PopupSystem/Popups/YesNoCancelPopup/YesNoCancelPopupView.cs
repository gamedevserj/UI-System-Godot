using Godot;
using System;
using UISystem.Common.Helpers;
using UISystem.Core.Common.ElementViews;
using UISystem.Core.Common.Interfaces;
using UISystem.Core.PopupSystem.Views;

namespace UISystem.PopupSystem.Views;
public partial class YesNoCancelPopupView : PopupView
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
