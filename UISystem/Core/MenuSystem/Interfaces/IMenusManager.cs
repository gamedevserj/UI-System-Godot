using System;
using UISystem.MenuSystem.Constants;

namespace UISystem.Core.MenuSystem.Interfaces;
internal interface IMenusManager
{

    void ShowMenu(int menuType, int menuChangeType = MenuChangeType.AddToStack, Action onNewMenuShown = null, bool instant = false);
    void ReturnToPreviousMenu(Action onComplete = null, bool instant = false);

}
