using Godot;
using System;
using UISystem.Common.Helpers;
using UISystem.Core.MenuSystem.Views;

namespace UISystem.MenuSystem.Views;
public partial class InGameMenuView : MenuView
{

    [Export] private Control fadeObjectsContainer;

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
