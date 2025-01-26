using System;
using UISystem.Core.Controllers;
using UISystem.Core.MenuSystem.Enums;
using UISystem.Core.MenuSystem.Interfaces;
using UISystem.Core.Views.Interfaces;

namespace UISystem.Core.MenuSystem.Controllers;
internal abstract class MenuController<TViewHandler, TView, TModel, TInputEvent, TInteractableElement> 
    : Controller<TViewHandler, TView, TInputEvent>, IMenuController<TInputEvent>
    where TViewHandler : IViewHandler<TView>
    where TView : IMenuView<TInteractableElement>
    where TModel : IMenuModel
{

    protected TModel _model;

    protected readonly IMenusManager<TInputEvent> _menusManager;

    public bool CanReturnToPreviousMenu { get; set; } = true; // when you want to temporarly disable retuning to previous menu, i.e. when player is rebinding keys

    public MenuController(TViewHandler viewHandler, TModel model, IMenusManager<TInputEvent> menusManager)
    {
        _viewHandler = viewHandler;
        _model = model;
        _menusManager = menusManager;
    }

    public override void Init()
    {
        if (!_viewHandler.IsViewValid)
        {
            _view = _viewHandler.CreateView();
            SetupElements();
        }
    }

    public virtual void Show(Action onComplete = null, bool instant = false)
    {
        CanReceivePhysicalInput = false;
        _view.Show(() =>
        {
            onComplete?.Invoke();
            _view.FocusElement();
            CanReceivePhysicalInput = true;
        }, instant);
    }

    public virtual void Hide(StackingType stackingType, Action onComplete = null, bool instant = false) 
    {
        CanReceivePhysicalInput = false;
        _view.Hide(() =>
        {
            onComplete?.Invoke();
            CanReceivePhysicalInput = true;
        }, instant);
    }

    public virtual void ProcessStacking(StackingType stackingType)
    {
        switch (stackingType)
        {
            case StackingType.Add:
                break;
            case StackingType.Remove:
                DestroyView();
                break;
            case StackingType.Clear:
                DestroyView();
                break;
            case StackingType.Replace:
                DestroyView();
                break;
            default:
                break;
        }
    }
    protected override void DestroyView() => _viewHandler.DestroyView();

    // when showing popups
    protected void SwitchFocusAvailability(bool enable)
    {
        _view.SwitchFocusAvailability(enable);
        if (enable)
            _view.FocusElement();
    }

    public override void OnCancelButtonDown()
    {
        if (CanReturnToPreviousMenu)
            _menusManager.ReturnToPreviousMenu();
    }

    public override void OnPauseButtonDown() // for in-game menu
    { }
    public override void OnResumeButtonDown() // for pause menu
    { }
    public override void OnAnyButtonDown(TInputEvent inputEvent) // for rebind menu
    { }

}
