using Godot;
using UISystem.Common;
using UISystem.Common.Elements;
using UISystem.Common.Interfaces;

namespace PopupSystem.Views;

public partial class PopupView : BaseMenuView
{

    [Export] private Label message;
    [Export] protected ButtonView confirmButton;

    public Label Message { get => message; set => message = value; }
    public ButtonView ConfirmButton => confirmButton;
    public virtual Control DefaultSelectedElement => ConfirmButton;

    protected override void PopulateFocusableElements()
    {
        _focusableElements = new IFocusableControl[] { ConfirmButton};
    }

}
