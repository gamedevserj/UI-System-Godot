using System;

namespace UISystem.Core.PopupSystem;
public partial interface IPopupsManager<TInputEvent, TPopupType, TPopupResult> 
    where TPopupType : Enum 
    where TPopupResult : Enum
{
    void Init(IPopupController<TInputEvent, TPopupType, TPopupResult>[] controllers);
    void ShowPopup(TPopupType popupType, string message, Action<TPopupResult> onHideAction = null, bool instant = false);
    void HidePopup(TPopupResult result);

}
