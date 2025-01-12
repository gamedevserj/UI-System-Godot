using Godot;
using UISystem.Core.PopupSystem.Controllers;
using UISystem.Core.PopupSystem.Interfaces;
using UISystem.Core.Transitions.Interfaces;
using UISystem.PopupSystem.Constants;
using UISystem.PopupSystem.Views;
using UISystem.Transitions;
using UISystem.Transitions.Interfaces;

namespace UISystem.PopupSystem.Controllers;
internal class YesPopupController : PopupController<YesPopupView>
{

    protected const float PanelDuration = 0.5f;
    protected const float ElementsDuration = 0.25f;

    public override int Type => PopupType.Yes;
    public override int PressedReturnPopupResult => PopupResult.Yes;

    public YesPopupController(string prefab, IPopupsManager popupsManager, Node parent, SceneTree sceneTree)
        : base(prefab, popupsManager, parent, sceneTree)
    {
    }

    protected override void SetupElements()
    {
        _view.YesButton.ButtonDown += () => _popupsManager.HidePopup(PopupResult.Yes);
    }

    protected override IViewTransition CreateTransition()
    {
        return new PanelSizeTransition(_view, _view.FadeObjectsContainer, _view.Panel, 
            new ITweenableMenuElement[] { _view.YesButton, _view.MessageMask }, 
            PanelDuration, ElementsDuration);
    }
}
