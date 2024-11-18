using System;
using System.Collections.Generic;
using UISystem.Core.MenuSystem.Enums;

namespace UISystem.Core.MenuSystem.Interfaces;
internal interface IMenusManager
{

    void Init(IMenuController[] controllers);
    void ShowMenu(int menuType, StackingType stackingType = StackingType.Add, Action onNewMenuShown = null, bool instant = false);
    void ReturnToPreviousMenu(Action onComplete = null, bool instant = false);

}
