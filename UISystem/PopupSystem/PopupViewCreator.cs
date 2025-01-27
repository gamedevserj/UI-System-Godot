using Godot;
using UISystem.Core.Extensions;
using UISystem.Core.Views;

namespace UISystem.PopupSystem;
internal abstract class PopupViewCreator<TView> : ViewCreator<string, TView, Node> where TView : PopupView
{

    public override bool IsViewValid => _view != null && _view.IsValid;

    public PopupViewCreator(string prefab, Node parent) : base(prefab, parent)
    { }

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
