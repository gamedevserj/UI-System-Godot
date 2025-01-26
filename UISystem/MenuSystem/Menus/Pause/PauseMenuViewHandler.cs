using Godot;
using UISystem.Core.Transitions;
using UISystem.Core.Views.Interfaces;
using UISystem.MenuSystem.Views;
using UISystem.Transitions;

namespace UISystem.MenuSystem.ViewHandlers;
internal class PauseMenuViewHandler<TView> : MenuViewHandler<PauseMenuView>, IViewHandler<PauseMenuView>
{
    public PauseMenuViewHandler(string prefab, Node parent) : base(prefab, parent)
    { }

    public override IViewTransition CreateTransition()
    {
        return new MainElementDropTransition(_view, _view.FadeObjectsContainer, _view.ResumeGameButton,
            new[] { _view.OptionsButton, _view.ReturnToMainMenuButton });
    }
}
