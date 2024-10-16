using Godot;
using UISystem.Core.Elements.Interfaces;
using UISystem.Core.PopupSystem.Views;
using UISystem.Elements.ElementViews;
using UISystem.Transitions;
using UISystem.Transitions.Interfaces;

namespace UISystem.PopupSystem.Views;
internal partial class YesPopupView : PopupView
{

    protected const float PanelDuration = 0.5f;
    protected const float ElementsDuration = 0.25f;

    [Export] protected Control fadeObjectsContainer;
    [Export] protected Control panel;
    [Export] protected ButtonView yesButton;
    [Export] protected ResizableControlView messageMask;

    public ButtonView YesButton => yesButton;
    public override IFocusableControl DefaultSelectedElement => YesButton;

    protected override void PopulateFocusableElements()
    {
        _focusableElements = new IFocusableControl[] { YesButton };
    }

    protected virtual ITweenableMenuElement[] GetTweenableElements()
    {
        return new ITweenableMenuElement[] { YesButton, messageMask };
    }

    public override void Init()
    {
        base.Init();
        _transition = new PanelSizeTransition(this, fadeObjectsContainer, panel, GetTweenableElements(), PanelDuration, ElementsDuration);
    }

}
