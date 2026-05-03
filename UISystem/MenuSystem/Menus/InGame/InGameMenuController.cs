using System.Threading.Tasks;
using AsyncAwaitBestPractices;
using UISystem.Core.MenuSystem;
using UISystem.Core.Views;
using UISystem.MenuSystem.Views;

namespace UISystem.MenuSystem.Controllers;

/// <summary>
/// In-game menu controller.
/// </summary>
internal class InGameMenuController : MenuController<IViewCreator<InGameMenuView>, InGameMenuView>
{
    private readonly MenuBackgroundController _menuBackgroundController;

    /// <summary>
    /// Initializes a new instance of the <see cref="InGameMenuController"/> class.
    /// </summary>
    /// <param name="viewCreator">View creator.</param>
    /// <param name="menusManager">Menus manager.</param>
    /// <param name="menuBackgroundController">Menu background controller.</param>
    public InGameMenuController(
        IViewCreator<InGameMenuView> viewCreator,
        IMenusManager menusManager,
        MenuBackgroundController menuBackgroundController)
        : base(viewCreator, menusManager)
    {
        _menuBackgroundController = menuBackgroundController;
    }

    /// <inheritdoc/>
    public override void OnPauseButtonDown()
    {
        MenusManager.ShowMenu<PauseMenuView>().SafeFireAndForget();
    }

    /// <inheritdoc/>
    public override Task Show(bool instant = false)
    {
        _menuBackgroundController.HideBackground(instant).SafeFireAndForget();
        return base.Show(instant);
    }

    /// <inheritdoc/>
    protected override void SetupElements()
    {
    }
}
