using Godot;
using UISystem.Core.Views;
using UISystem.Extensions;

namespace UISystem.Views;

/// <summary>
/// View creator.
/// </summary>
/// <typeparam name="TView">The type of view to create. Must derive from <see cref="ViewBase"/>.</typeparam>
internal class ViewCreator<TView> : ViewCreator<string, TView, Node>
    where TView : ViewBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ViewCreator{TView}"/> class.
    /// </summary>
    /// <param name="prefab">View prefab.</param>
    /// <param name="parent">View parent.</param>
    public ViewCreator(string prefab, Node parent)
        : base(prefab, parent)
    {
    }

    /// <inheritdoc/>
    public override bool IsViewValid => View.IsValid();

    /// <inheritdoc/>
    public override void DestroyView() => View.SafeQueueFree();

    /// <inheritdoc/>
    public override TView CreateView()
    {
        PackedScene loadedPrefab = ResourceLoader.Load<PackedScene>(Prefab);
        View = loadedPrefab.Instantiate() as TView;
        View.Init();
        Parent.AddChild(View);
        return View;
    }
}
