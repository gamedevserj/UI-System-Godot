using Godot;
using UISystem.Core.PhysicalInput;
using UISystem.Core.PopupSystem.Interfaces;
using UISystem.Core.PopupSystem.Views;

namespace UISystem.Core.PopupSystem.Controllers;
internal abstract class PopupController<TPrefab, TView, TParent, TInputEvent> : PopupControllerBase<string, TView, Node, TInputEvent> where TView : PopupView
{

    protected override bool IsViewValid => _view != null && _view.IsValid;

    protected PopupController(string prefab, IPopupsManager<TInputEvent> popupsManager, Node parent) 
        : base(prefab, popupsManager, parent)
    { }

    protected override void CreateView(Node parent)
    {
        PackedScene obj = ResourceLoader.Load<PackedScene>(_prefab);
        _view = obj.Instantiate() as TView;
        _view.Init(CreateTransition());
        SetupElements();
        parent.AddChild(_view);
    }

    protected override void FocusElement()
    {
        if (_defaultSelectedElement?.IsValidElement() == true)
        {
            _defaultSelectedElement.SwitchFocus(true);
        }
    }
}
