using Godot;
using UISystem.Common.ElementViews;
using UISystem.Common.Transitions;
using UISystem.Core.Elements.Interfaces;
using UISystem.Core.PopupSystem.Views;

namespace UISystem.PopupSystem.Popups.YesPopup;
internal partial class YesPopupView : PopupView
{

    [Export] private Control fadeObjectsContainer;
    [Export] protected ButtonView yesButton;

    public ButtonView YesButton => yesButton;
    public override IFocusableControl DefaultSelectedElement => YesButton;

    protected override void PopulateFocusableElements()
    {
        _focusableElements = new IFocusableControl[] { YesButton };
    }

    public override void Init()
    {
        base.Init();
        _transition = new FadeTransition(fadeObjectsContainer);
    }

}
