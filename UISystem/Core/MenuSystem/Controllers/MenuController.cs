using System;
using UISystem.Core.Views;

namespace UISystem.Core.MenuSystem;
internal abstract class MenuController<TViewCreator, TView, TModel, TInputEvent, TInteractableElement, TType>
    : Controller<TViewCreator, TView, TInputEvent, TType>, IMenuController<TInputEvent, TType>
    where TViewCreator : IViewCreator<TView>
    where TView : IMenuView<TInteractableElement>
    where TModel : IMenuModel
    where TType : Enum
{

    protected TModel _model;

    protected readonly IMenusManager<TInputEvent, TType> _menusManager;

    // when you want to temporarly disable retuning to previous menu, i.e. when player is rebinding keys
    public bool CanReturnToPreviousMenu { get; set; } = true; 

    public MenuController(TViewCreator viewCreator, TModel model, IMenusManager<TInputEvent, TType> menusManager)
    {
        _viewCreator = viewCreator;
        _model = model;
        _menusManager = menusManager;
    }

    public override void Init()
    {
        if (!_viewCreator.IsViewValid)
        {
            _view = _viewCreator.CreateView();
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
    protected override void DestroyView() => _viewCreator.DestroyView();

    // when showing popups
    protected void SwitchFocusAvailability(bool enable)
    {
        _view.SwitchFocusAvailability(enable);
        if (enable)
            _view.FocusElement();
    }

    public override void OnReturnButtonDown()
    {
        if (CanReturnToPreviousMenu)
            _menusManager.ReturnToPreviousMenu();
    }

}
