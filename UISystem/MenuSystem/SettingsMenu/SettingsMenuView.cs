using Godot;
using UISystem.Core.Views;
using UISystem.Elements.ElementViews;

namespace UISystem.MenuSystem.SettingsMenu;
public abstract partial class SettingsMenuView : BaseInteractableWindow
{

    [Export] protected Control fadeObjectsContainer;
    [Export] private ButtonView resetButton;

    public ButtonView ResetButton => resetButton;

}
