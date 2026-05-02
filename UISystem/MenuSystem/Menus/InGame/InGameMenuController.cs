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
    /// <summary>
    /// Initializes a new instance of the <see cref="InGameMenuController"/> class.
    /// </summary>
    /// <param name="viewCreator">View creator.</param>
    /// <param name="menusManager">Menus manager.</param>
    public InGameMenuController(IViewCreator<InGameMenuView> viewCreator, IMenusManager menusManager)
        : base(viewCreator, menusManager)
    {
    }

    /// <inheritdoc/>
    public override void OnPauseButtonDown()
    {
        MenusManager.ShowMenu(typeof(PauseMenuView)).SafeFireAndForget();
    }

    /// <inheritdoc/>
    protected override void SetupElements()
    {
    }
}
