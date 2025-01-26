using System;
using UISystem.Core.PhysicalInput;

namespace UISystem.Core.PopupSystem;
public partial interface IPopupController<TInputEvent> : IController<TInputEvent>, IInputReceiver<TInputEvent>
{

    void Hide(int result, bool instant = false);
    void Show(string message, Action<int> onHideAction, bool instant);

}
