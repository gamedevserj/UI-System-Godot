using Godot;
using UISystem.Core.Transitions;
using UISystem.Core.Views.Interfaces;
using UISystem.MenuSystem.Views;
using UISystem.Transitions;
using UISystem.Transitions.Interfaces;

namespace UISystem.MenuSystem.ViewHandlers;
internal class AudioSettingsMenuViewHandler<TView> : MenuViewHandler<AudioSettingsMenuView>, IViewHandler<AudioSettingsMenuView>
{
    public AudioSettingsMenuViewHandler(string prefab, Node parent) : base(prefab, parent)
    { }

    public override IViewTransition CreateTransition()
    {
        return new PanelSizeTransition(_view, _view.FadeObjectsContainer, _view.Panel,
            new ITweenableMenuElement[] { _view.ReturnButton, _view.SaveSettingsButton, _view.ResetButton,
            _view.MusicSlider, _view.SfxSlider, _view.ResizableControlMusic, _view.ResizableControlSfx });
    }
}
