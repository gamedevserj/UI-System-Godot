namespace UISystem.Core.Views;
internal abstract class ViewCreator<TPrefab, TView, TParent> : IViewCreator<TView>
{

    protected TView _view;
    protected readonly TPrefab _prefab;
    protected readonly TParent _parent;

    public abstract bool IsViewValid { get; }

    public ViewCreator(TPrefab prefab, TParent parent)
    {
        _prefab = prefab;
        _parent = parent;
    }

    public abstract TView CreateView();

    public abstract void DestroyView();

    public abstract void SwitchFocusAvailability(bool enable);

}
