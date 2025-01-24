using Godot;
using UISystem.Core.PhysicalInput;
using UISystem.Core.PopupSystem.Interfaces;
using UISystem.Core.PopupSystem.Views;

namespace UISystem.Core.PopupSystem.Controllers;
internal abstract class PopupController<TPrefab, TView, TParent, TInputEvent> : PopupControllerBase<string, TView, Node, TInputEvent> where TView : PopupView
{
    protected PopupController(string prefab, IPopupsManager<TInputEvent> popupsManager, Node parent, IInputProcessor<TInputEvent> inputProcessor) 
        : base(prefab, popupsManager, parent, inputProcessor)
    { }

    protected override void CreateView(Node menuParent)
    {
        PackedScene obj = ResourceLoader.Load<PackedScene>(_prefab);
        _view = obj.Instantiate() as TView;
        _view.Init(CreateTransition());
        SetupElements();
        menuParent.AddChild(_view);
    }
}
