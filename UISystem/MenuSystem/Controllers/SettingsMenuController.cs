using UISystem.MenuSystem.Interfaces;
using UISystem.MenuSystem.Views;

namespace UISystem.MenuSystem.Controllers;
public abstract class SettingsMenuController<TView, TModel> : MenuController<TView, TModel> where TView : MenuView where TModel : IMenuModel
{
    protected SettingsMenuController(string prefab, TModel model, MenusManager menusManager) : base(prefab, model, menusManager)
    {
    }

    protected abstract void ResetViewToDefault();
}
