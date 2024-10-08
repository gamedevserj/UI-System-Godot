using Godot;
using System;
using UISystem.Common.Helpers;
using UISystem.Core.Common.ElementViews;
using UISystem.Core.Common.Interfaces;
using UISystem.Core.PopupSystem.Views;

namespace UISystem.PopupSystem.Views;
public partial class YesNoPopupView : PopupView
{

    [Export] private ButtonView noButton;

    public ButtonView NoButton => noButton;

    public override IFocusableControl DefaultSelectedElement => NoButton;

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
        _focusableElements = new IFocusableControl[] { YesButton, NoButton };
    }
}
