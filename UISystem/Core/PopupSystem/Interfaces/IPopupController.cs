using System;
using UISystem.Core.PhysicalInput;

namespace UISystem.Core.PopupSystem;
public partial interface IPopupController<TInputEvent, TType, TResult> : IController<TInputEvent, TType>, IInputReceiver<TInputEvent>
    where TType : Enum
    where TResult : Enum
{

    void Hide(TResult result, bool instant = false);
    void Show(string message, Action<TResult> onHideAction, bool instant);

}
