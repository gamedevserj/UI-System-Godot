using System;
using UISystem.Core.PhysicalInput;

namespace UISystem.Core;
internal abstract class Controller<TViewCreator, TView, TInputEvent, TType> : IController<TInputEvent, TType>, IInputReceiver<TInputEvent>
    where TType : Enum
{

    protected TViewCreator _viewCreator;
    protected TView _view;

    public abstract TType Type { get; }
    public bool CanReceivePhysicalInput { get; protected set; } // to prevent input processing during transitions

    public abstract void Init();
    public abstract void OnReturnButtonDown();
    public virtual void OnPauseButtonDown() { } // for in-game menu
    public virtual void OnAnyButtonDown(TInputEvent inputEvent) { }  // for rebind menu
    protected abstract void DestroyView();
    protected abstract void SetupElements();

}
