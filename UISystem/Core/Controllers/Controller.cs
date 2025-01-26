using UISystem.Core.Views.Interfaces;

namespace UISystem.Core.Controllers;
internal abstract class Controller<TViewHandler, TView, TInputEvent>
{

    protected TViewHandler _viewHandler;
    protected TView _view;

    public abstract int Type { get; }
    public bool CanReceivePhysicalInput { get; protected set; } // to prevent input processing during transitions

    public abstract void Init();

    public abstract void OnCancelButtonDown();
    public virtual void OnPauseButtonDown() { } // for in-game menu
    public virtual void OnAnyButtonDown(TInputEvent inputEvent) { }  // for rebind menu
    protected abstract void DestroyView();
    protected abstract void SetupElements();

}
