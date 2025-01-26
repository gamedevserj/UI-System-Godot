using System;

namespace UISystem.Core.MenuSystem;
public partial interface IMenusManager<TInputEvent>
{

    void Init(IMenuController<TInputEvent>[] controllers);
    void ShowMenu(int menuType, StackingType stackingType = StackingType.Add, Action onNewMenuShown = null, bool instant = false);
    void ReturnToPreviousMenu(Action onComplete = null, bool instant = false);

}
