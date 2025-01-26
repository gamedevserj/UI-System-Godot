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
    public abstract void OnPauseButtonDown();
    public abstract void OnResumeButtonDown();
    public abstract void OnAnyButtonDown(TInputEvent inputEvent);
    protected abstract void DestroyView();
    protected abstract void SetupElements();

}
