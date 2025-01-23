using Godot;
using UISystem.Core.Constants;
using UISystem.Core.Elements.Interfaces;
using UISystem.Core.MenuSystem.Interfaces;
using UISystem.Core.Transitions.Interfaces;
using UISystem.MenuSystem.Constants;
using UISystem.MenuSystem.Views;
using UISystem.Transitions;

namespace UISystem.MenuSystem.Controllers;
internal class InGameMenuController : MenuController<string, InGameMenuView, IMenuModel, Node, IFocusableControl>
{

    public override int Type => MenuType.InGame;

    public InGameMenuController(string prefab, IMenuModel model, IMenusManager menusManager, Node parent) 
        : base(prefab, model, menusManager, parent)
    { }

    public override void ProcessInput(InputEvent key)
    {
        if (key.IsPressed() && key.IsAction(InputsData.PauseButton))
            PauseGame();
    }

    private void PauseGame()
    {
        _menusManager.ShowMenu(MenuType.Pause);
    }

    protected override void SetupElements()
    {

    }

    protected override IViewTransition CreateTransition() => new FadeTransition(_view.FadeObjectsContainer);
}