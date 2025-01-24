using System;
using UISystem.Core.MenuSystem.Interfaces;

namespace UISystem.Core.PopupSystem.Interfaces;
public partial interface IPopupsManager<TInputEvent>
{

    void Init(IPopupController<TInputEvent>[] controllers);
    void ProcessInput(TInputEvent @event);
    void ShowPopup(int popupType, IMenuController<TInputEvent> caller, string message, Action<int> onHideAction = null, bool instant = false);
    void HidePopup(int result);

}
