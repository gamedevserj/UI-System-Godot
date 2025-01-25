using Godot;
using UISystem.Core.Elements.Interfaces;
using UISystem.Core.MenuSystem.Controllers;
using UISystem.Core.MenuSystem.Interfaces;
using UISystem.Core.Views;

namespace UISystem.MenuSystem;
// controller for Godot
internal abstract class MenuController<TPrefab, TView, TModel, TParent, TFocusableElement> : MenuControllerBase<string, TView, TModel, Node, IFocusableControl, InputEvent>
    where TView : BaseWindowView 
    where TModel : IMenuModel
{

    protected override bool IsViewValid => _view != null && _view.IsValid;

    protected MenuController(string prefab, TModel model, IMenusManager<InputEvent> menusManager, Node parent) 
        : base(prefab, model, menusManager, parent)
    { }

    protected override void CreateView(Node parent)
    {
        PackedScene loadedPrefab = ResourceLoader.Load<PackedScene>(_prefab);
        _view = loadedPrefab.Instantiate() as TView;
        _view.Init(CreateTransition());
        parent.AddChild(_view);
        SetupElements();
    }

    protected override void FocusElement()
    {
        if (_lastSelectedElement?.IsValidElement() == true)
        {
            _lastSelectedElement.SwitchFocus(true);
        }
        else if (_defaultSelectedElement?.IsValidElement() == true)
        {
            _defaultSelectedElement.SwitchFocus(true);
        }
    }

}
