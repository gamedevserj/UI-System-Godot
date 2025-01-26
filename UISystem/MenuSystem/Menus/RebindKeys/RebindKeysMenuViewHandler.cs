using Godot;
using UISystem.Core.Transitions;
using UISystem.Core.Views;
using UISystem.MenuSystem.Views;
using UISystem.Transitions;
using UISystem.Transitions.Interfaces;

namespace UISystem.MenuSystem.ViewHandlers;
internal class RebindKeysMenuViewHandler<TView> : MenuViewHandler<RebindKeysMenuView>, IViewHandler<RebindKeysMenuView>
{
    public RebindKeysMenuViewHandler(string prefab, Node parent) : base(prefab, parent)
    { }

    public override IViewTransition CreateTransition()
    {
        return new PanelSizeTransition(_view, _view.FadeObjectsContainer, _view.Panel,
            new ITweenableMenuElement[] { _view.ReturnButton, _view.ResetButton,
                _view.MoveLeft, _view.MoveLeftJoystick, _view.MoveRight, _view.MoveRightJoystick, _view.Jump, _view.JumpJoystick,
                _view.MoveLeftLabelResizableControl, _view.MoveRightLabelResizableControl, _view.JumpLabelResizableControl});
    }
}
