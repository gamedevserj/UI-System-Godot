using Godot;
using UISystem.Core.Transitions;
using UISystem.Core.Views;
using UISystem.MenuSystem.Views;
using UISystem.Transitions;

namespace UISystem.MenuSystem.ViewHandlers;
internal class MainMenuViewHandler<TView> : MenuViewHandler<MainMenuView>, IViewHandler<MainMenuView>
{
    public MainMenuViewHandler(string prefab, Node parent) : base(prefab, parent)
    { }

    public override IViewTransition CreateTransition()
    {
        return new MainElementDropTransition(_view, _view.FadeObjectsContainer, _view.PlayButton,
            new[] { _view.OptionsButton, _view.QuitButton });
    }
}
