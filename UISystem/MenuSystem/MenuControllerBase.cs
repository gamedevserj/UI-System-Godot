using UISystem.Core.MenuSystem;
using UISystem.Core.Views;
using UISystem.Elements;

namespace UISystem.MenuSystem;
// just a base class to adapt generic controller to Godot's specific parameters
// so that there is no need to specify IMenuModel, InputEvent, IFocusableControl for every controller
internal abstract class MenuControllerBase<TViewCreator, TView>
    : MenuController<TViewCreator, TView, IMenuModel, IFocusableControl, MenuType>
    where TViewCreator : IViewCreator<TView>
    where TView : IMenuView<IFocusableControl>
{
    protected MenuControllerBase(TViewCreator viewCreator, IMenuModel model, IMenusManager<MenuType> menusManager) : base(viewCreator, model, menusManager)
    {
    }
}
