using Godot;
using UISystem.Core.Transitions.Interfaces;
using UISystem.Core.Views.Interfaces;
using UISystem.MenuSystem.Views;
using UISystem.Transitions;
using UISystem.Transitions.Interfaces;

namespace UISystem.MenuSystem.ViewHandlers;
internal class InterfaceSettingsMenuViewHandler<TView> : MenuViewHandler<InterfaceSettingsMenuView>, IViewHandler<InterfaceSettingsMenuView>
{
    public InterfaceSettingsMenuViewHandler(string prefab, Node parent) : base(prefab, parent)
    { }

    public override IViewTransition CreateTransition()
    {
        return new PanelSizeTransition(_view, _view.FadeObjectsContainer, _view.Panel,
            new ITweenableMenuElement[] { _view.ReturnButton, _view.ControllerIconsDropdown, _view.SaveSettingsButton, _view.ResetButton });
    }
}
