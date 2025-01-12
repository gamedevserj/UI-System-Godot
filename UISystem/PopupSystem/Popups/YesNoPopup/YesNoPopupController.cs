using Godot;
using UISystem.Core.PopupSystem.Controllers;
using UISystem.Core.PopupSystem.Interfaces;
using UISystem.Core.Transitions.Interfaces;
using UISystem.PopupSystem.Constants;
using UISystem.PopupSystem.Views;
using UISystem.Transitions.Interfaces;
using UISystem.Transitions;

namespace UISystem.PopupSystem.Controllers;
internal class YesNoPopupController : PopupController<YesNoPopupView>
{

    protected const float PanelDuration = 0.5f;
    protected const float ElementsDuration = 0.25f;

    public override int Type => PopupType.YesNo;
    public override int PressedReturnPopupResult => PopupResult.No;

    public YesNoPopupController(string prefab, IPopupsManager popupsManager, Node parent, SceneTree sceneTree) 
        : base(prefab, popupsManager, parent, sceneTree)
    { }

    protected override void SetupElements()
    {
        _view.YesButton.ButtonDown += () => _popupsManager.HidePopup(PopupResult.Yes);
        _view.NoButton.ButtonDown += () => _popupsManager.HidePopup(PopupResult.No);
    }

    protected override IViewTransition CreateTransition()
    {
        return new PanelSizeTransition(_view, _view.FadeObjectsContainer, _view.Panel,
            new ITweenableMenuElement[] { _view.YesButton, _view.NoButton, _view.MessageMask },
            PanelDuration, ElementsDuration);
    }
}
