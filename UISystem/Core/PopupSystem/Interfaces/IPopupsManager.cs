using System;
using UISystem.Core.MenuSystem;
using UISystem.Core.PhysicalInput;

namespace UISystem.Core.PopupSystem;
public partial interface IPopupsManager<TInputEvent>
{

    void Init(IPopupController<TInputEvent>[] controllers, IInputProcessor<TInputEvent> inputProcessor);
    void ProcessInput(TInputEvent @event);
    void ShowPopup(int popupType, IMenuController<TInputEvent> caller, string message, Action<int> onHideAction = null, bool instant = false);
    void HidePopup(int result);

}
