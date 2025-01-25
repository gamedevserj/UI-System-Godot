using Godot;
using UISystem.Core.Extensions;
using UISystem.Core.PopupSystem.Views;
using UISystem.Core.Views;

namespace UISystem.PopupSystem;
internal abstract class PopupViewHandler<TView> : ViewHandler<string, TView, Node> where TView : PopupView
{

    protected override bool IsViewValid => _view != null && _view.IsValid;

    public PopupViewHandler(string prefab, Node parent) : base(prefab, parent)
    { }

    public override void DestroyView() => _view.SafeQueueFree();

    public override void SwitchFocusAvailability(bool enable) => _view.SwitchFocusAvailability(enable);

    protected override TView CreateView()
    {
        PackedScene loadedPrefab = ResourceLoader.Load<PackedScene>(_prefab);
        _view = loadedPrefab.Instantiate() as TView;
        _view.Init(CreateTransition());
        _parent.AddChild(_view);
        return _view;
    }
}
