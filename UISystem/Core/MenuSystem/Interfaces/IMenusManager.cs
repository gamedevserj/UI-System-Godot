using System;
using UISystem.Core.PhysicalInput;

namespace UISystem.Core.MenuSystem;
public partial interface IMenusManager<TInputEvent>
{

    void Init(IMenuController<TInputEvent>[] controllers, IInputProcessor<TInputEvent> inputProcessor);
    void ProcessInput(TInputEvent key);
    void ShowMenu(int menuType, StackingType stackingType = StackingType.Add, Action onNewMenuShown = null, bool instant = false);
    void ReturnToPreviousMenu(Action onComplete = null, bool instant = false);

}
