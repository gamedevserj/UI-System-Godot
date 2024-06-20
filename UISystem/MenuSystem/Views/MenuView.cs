using Godot;
using System;
using UISystem.Common;
using UISystem.MenuSystem.Interfaces;

namespace UISystem.MenuSystem.Views;
public abstract partial class MenuView : BaseInteractableView, IMenuView
{

    public abstract void Show(Action onShown, bool instant = false);
    public abstract void Hide(Action onHidden, bool instant = false);

}
