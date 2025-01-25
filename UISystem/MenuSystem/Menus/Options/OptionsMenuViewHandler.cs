using Godot;
using UISystem.Core.Transitions.Interfaces;
using UISystem.Core.Views.Interfaces;
using UISystem.MenuSystem.Views;
using UISystem.Transitions;

namespace UISystem.MenuSystem.ViewHandlers;
internal class OptionsMenuViewHandler<TView> : MenuViewHandler<OptionsMenuView>, IViewHandler<OptionsMenuView>
{
    public OptionsMenuViewHandler(string prefab, Node parent) : base(prefab, parent)
    { }

    public override IViewTransition CreateTransition()
    {
        return new MainElementDropTransition(_view, _view.FadeObjectsContainer, _view.InterfaceSettingsButton,
            new[] { _view.ReturnButton, _view.AudioSettingsButton, _view.VideoSettingsButton, _view.RebindKeysButton });
    }
}
