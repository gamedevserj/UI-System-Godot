using Godot;
using System;
using UISystem.Common.Elements;
using UISystem.Common.Helpers;

namespace UISystem.MenuSystem.Views;
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

    //public override void Init()
    //{
    //    base.Init();
    //    Fader.Init(fadeObjectsContainer);
    //}

    //public override void Hide(Action onHidden, bool instant)
    //{
    //    Fader.Hide(GetTree(), fadeObjectsContainer, onHidden, instant);
    //}

    //public override void Show(Action onShown, bool instant)
    //{
    //    Fader.Show(GetTree(), fadeObjectsContainer, onShown, instant);
    //}
}
