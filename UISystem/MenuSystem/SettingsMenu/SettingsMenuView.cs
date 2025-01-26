using Godot;
using UISystem.Elements;
using UISystem.Elements.ElementViews;

namespace UISystem.MenuSystem.SettingsMenu;
public abstract partial class SettingsMenuView : MenuView
{

    [Export] protected Control fadeObjectsContainer;
    [Export] private ButtonView returnButton;
    [Export] private ButtonView resetButton;

    public Control FadeObjectsContainer => fadeObjectsContainer;
    public ButtonView ReturnButton => returnButton;
    public ButtonView ResetButton => resetButton;

    protected override IFocusableControl DefaultSelectedElement => ReturnButton;

}
