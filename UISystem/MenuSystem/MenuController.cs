using Godot;
using UISystem.Core.MenuSystem.Controllers;
using UISystem.Core.MenuSystem.Interfaces;
using UISystem.Core.Views;

namespace UISystem.MenuSystem;
// controller for Godot
internal abstract class MenuController<TView, TModel> : MenuControllerBase<TView, TModel> where TView : BaseWindowView where TModel : IMenuModel
{

    protected override bool IsViewValid => _view != null && _view.IsValid;

    protected MenuController(string prefab, TModel model, IMenusManager menusManager, Node parent) : base(prefab, model, menusManager, parent)
    {
    }

    protected override void CreateView(Node menuParent)
    {
        PackedScene loadedPrefab = ResourceLoader.Load<PackedScene>(_prefab);
        _view = loadedPrefab.Instantiate() as TView;
        _view.Init(CreateTransition());
        SetupElements();
        menuParent.AddChild(_view);
    }

}
