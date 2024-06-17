using Godot;
using UISystem.Common;
using UISystem.Common.Elements;
using UISystem.Common.Interfaces;

namespace PopupSystem.Views;

public partial class PopupView : BaseMenuView
{

    [Export] private Label message;
    [Export] protected ButtonView yesButton;

    public Label Message { get => message; set => message = value; }
    public ButtonView YesButton => yesButton;
    public virtual Control DefaultSelectedElement => YesButton;

    protected override void PopulateFocusableElements()
    {
        _focusableElements = new IFocusableControl[] { YesButton };
    }

}
