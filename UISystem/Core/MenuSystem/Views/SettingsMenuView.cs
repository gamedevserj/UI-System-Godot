using Godot;
using System;
using UISystem.Common.ElementViews;

namespace UISystem.Core.MenuSystem.Views;
public partial class SettingsMenuView : MenuView
{

    [Export] protected Control fadeObjectsContainer;
    [Export] private ButtonView resetButton;

    public ButtonView ResetButton => resetButton;

    public override void Hide(Action onHidden, bool instant = false)
    {

    }

    public override void Show(Action onShown, bool instant = false)
    {

    }

}
