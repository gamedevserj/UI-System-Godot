using Godot;
using UISystem.Core.MenuSystem;
using UISystem.Core.Views;
using UISystem.Elements;

namespace UISystem.MenuSystem;
// just a base class to adapt generic controller to Godot's specific parameters
// so that there is no need to specify IMenuModel, InputEvent, IFocusableControl for every controller
internal abstract class MenuControllerBase<TViewHandler, TView>
    : MenuController<TViewHandler, TView, IMenuModel, InputEvent, IFocusableControl>
    where TViewHandler : IViewHandler<TView>
    where TView : IMenuView<IFocusableControl>
{
    protected MenuControllerBase(TViewHandler viewHandler, IMenuModel model, IMenusManager<InputEvent> menusManager) : base(viewHandler, model, menusManager)
    {
    }
}
