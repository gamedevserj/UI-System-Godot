using Godot;
using UISystem.Core.Transitions;
using UISystem.Core.Views;
using UISystem.MenuSystem.Views;
using UISystem.Transitions;
using UISystem.Transitions.Interfaces;

namespace UISystem.MenuSystem.ViewHandlers;
internal class VideoSettingsMenuViewHandler<TView> : MenuViewHandler<VideoSettingsMenuView>, IViewHandler<VideoSettingsMenuView>
{
    public VideoSettingsMenuViewHandler(string prefab, Node parent) : base(prefab, parent)
    { }

    public override IViewTransition CreateTransition()
    {
        return new PanelSizeTransition(_view, _view.FadeObjectsContainer, _view.Panel,
            new ITweenableMenuElement[] { _view.ReturnButton, _view.ResolutionDropdown, _view.WindowModeDropdown,
                _view.SaveSettingsButton, _view.ResetButton });
    }
}
