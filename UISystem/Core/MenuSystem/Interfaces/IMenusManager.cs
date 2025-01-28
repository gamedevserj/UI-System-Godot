using System;

namespace UISystem.Core.MenuSystem;
public partial interface IMenusManager<TInputEvent, TMenuType>
    where TMenuType : Enum
{

    void Init(IMenuController<TInputEvent, TMenuType>[] controllers);
    void ShowMenu(TMenuType menuType, StackingType stackingType = StackingType.Add, Action onNewMenuShown = null, bool instant = false);
    void ReturnToPreviousMenu(Action onComplete = null, bool instant = false);

}
