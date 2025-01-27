using Godot;
using UISystem.Core.Extensions;
using UISystem.Core.Views;

namespace UISystem.MenuSystem;
internal abstract class MenuViewModel<TView> : ViewModel<string, TView, Node> where TView : MenuView
{

    public override bool IsViewValid => _view != null && _view.IsValid;

    protected MenuViewModel(string prefab, Node parent) : base(prefab, parent)
    {
    }    

    public override void DestroyView() => _view.SafeQueueFree();

    public override void SwitchFocusAvailability(bool enable) => _view.SwitchFocusAvailability(enable);

    public override TView CreateView()
    {
        PackedScene loadedPrefab = ResourceLoader.Load<PackedScene>(_prefab);
        _view = loadedPrefab.Instantiate() as TView;
        _view.Init();
        _parent.AddChild(_view);
        return _view;
    }
}
