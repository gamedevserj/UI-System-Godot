using System;

namespace UISystem.Core.PopupSystem;
public partial interface IPopupsManager<TInputEvent>
{
    void Init(IPopupController<TInputEvent>[] controllers);
    void ShowPopup(int popupType, string message, Action<int> onHideAction = null, bool instant = false);
    void HidePopup(int result);

}
