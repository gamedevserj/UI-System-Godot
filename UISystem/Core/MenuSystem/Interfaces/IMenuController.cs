using System;
using UISystem.Core.PhysicalInput;

namespace UISystem.Core.MenuSystem;
public partial interface IMenuController<TInputEvent, TType> : IController<TInputEvent, TType>, IInputReceiver<TInputEvent>
    where TType : Enum
{

    bool CanReturnToPreviousMenu { get; set; }

    void Hide(StackingType stackingType, Action onComplete = null, bool instant = false);
    void Show(Action onComplete = null, bool instant = false);
    void ProcessStacking(StackingType stackingType);

}
