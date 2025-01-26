using System;
using UISystem.Core.PhysicalInput;

namespace UISystem.Core.MenuSystem;
public partial interface IMenuController<TInputEvent> : IInputReceiver<TInputEvent>
{

    int Type { get; }
    bool CanReturnToPreviousMenu { get; set; }

    void Init();
    void Hide(StackingType stackingType, Action onComplete = null, bool instant = false);
    void Show(Action onComplete = null, bool instant = false);
    void ProcessStacking(StackingType stackingType);

}
