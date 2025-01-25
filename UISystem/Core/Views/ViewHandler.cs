using UISystem.Core.Transitions.Interfaces;
using UISystem.Core.Views.Interfaces;

namespace UISystem.Core.Views;
internal abstract class ViewHandler<TPrefab, TView, TParent> : IViewHandler<TView>
{

    protected TView _view;
    protected readonly TPrefab _prefab;
    protected readonly TParent _parent;

    protected abstract bool IsViewValid { get; }

    public ViewHandler(TPrefab prefab, TParent parent)
    {
        _prefab = prefab;
        _parent = parent;
    }

    public TView GetOrCreateView()
    {
        if (!IsViewValid)
            _view = CreateView();
        return _view;
    }

    public abstract IViewTransition CreateTransition();

    public abstract void DestroyView();

    public abstract void SwitchFocusAvailability(bool enable);

    protected abstract TView CreateView();
}
