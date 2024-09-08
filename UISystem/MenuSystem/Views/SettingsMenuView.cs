using Godot;
using System;
using UISystem.Common.Elements;
using UISystem.Common.Helpers;

namespace UISystem.MenuSystem.Views;
public partial class SettingsMenuView : MenuView
{

    [Export] private Control fadeObjectsContainer;
    [Export] private ButtonView resetButton;

    public ButtonView ResetButton => resetButton;

    public override void Init()
    {
        base.Init();
        Fader.Init(fadeObjectsContainer);
    }

    public override void Hide(Action onHidden, bool instant)
    {
        Fader.Hide(GetTree(), fadeObjectsContainer, onHidden, instant);
    }

    public override void Show(Action onShown, bool instant)
    {
        Fader.Show(GetTree(), fadeObjectsContainer, onShown, instant);
    }
}
