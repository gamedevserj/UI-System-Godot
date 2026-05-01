using UISystem.Core.MenuSystem;
using UISystem.Core.Views;
using UISystem.Elements;

namespace UISystem.MenuSystem;

/// <summary>
/// A base class to adapt generic controller to Godot's specific parameters,
/// so that there is no need to specify <see cref="IMenuView{IFocusableUiElement}"/> for every controller.
/// </summary>
/// <typeparam name="TViewCreator">Type of view creator. Must implement <see cref="IViewCreator{TView}"/>.</typeparam>
/// <typeparam name="TView">Type of view. Must implement <see cref="IMenuView{IFocusableUiElement}"/>.</typeparam>
internal abstract class MenuControllerBase<TViewCreator, TView>
    : MenuController<TViewCreator, TView, IFocusableUiElement>
    where TViewCreator : IViewCreator<TView>
    where TView : IMenuView<IFocusableUiElement>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MenuControllerBase{TViewCreator, TView}"/> class.
    /// </summary>
    /// <param name="viewCreator">View creator.</param>
    /// <param name="menusManager">Menus manager.</param>
    protected MenuControllerBase(TViewCreator viewCreator, IMenusManager menusManager)
        : base(viewCreator, menusManager)
    {
    }
}
