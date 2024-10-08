using System;
using UISystem.Core.Common;
using UISystem.Core.MenuSystem.Interfaces;

namespace UISystem.Core.MenuSystem.Views;
public abstract partial class MenuView : BaseInteractableWindow, IMenuView
{

    public abstract void Show(Action onShown, bool instant = false);
    public abstract void Hide(Action onHidden, bool instant = false);

}
