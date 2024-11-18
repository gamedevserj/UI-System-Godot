using System;
using UISystem.Core.MenuSystem.Enums;

namespace UISystem.Core.MenuSystem.Interfaces;
public partial interface IMenusManager
{

    void Init(IMenuController[] controllers);
    void ShowMenu(int menuType, StackingType stackingType = StackingType.Add, Action onNewMenuShown = null, bool instant = false);
    void ReturnToPreviousMenu(Action onComplete = null, bool instant = false);

}
