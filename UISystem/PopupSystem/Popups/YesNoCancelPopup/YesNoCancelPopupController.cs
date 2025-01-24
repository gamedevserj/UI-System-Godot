using Godot;
using UISystem.Core.PopupSystem.Controllers;
using UISystem.Core.PopupSystem.Interfaces;
using UISystem.Core.Transitions.Interfaces;
using UISystem.PopupSystem.Constants;
using UISystem.PopupSystem.Views;
using UISystem.Transitions.Interfaces;
using UISystem.Transitions;
using UISystem.Core.PhysicalInput;

namespace UISystem.PopupSystem.Controllers;
internal class YesNoCancelPopupController : PopupController<YesNoCancelPopupView, InputEvent>
{

    protected const float PanelDuration = 0.5f;
    protected const float ElementsDuration = 0.25f;

    public override int Type => PopupType.YesNoCancel;
    public override int PressedReturnPopupResult => PopupResult.Cancel;

    public YesNoCancelPopupController(string prefab, IPopupsManager<InputEvent> popupsManager, Node parent, IInputProcessor<InputEvent> inputProcessor, SceneTree sceneTree) 
        : base(prefab, popupsManager, parent, inputProcessor, sceneTree)
    { }

    protected override void SetupElements()
    {
        _view.YesButton.ButtonDown += () => _popupsManager.HidePopup(PopupResult.Yes);
        _view.NoButton.ButtonDown += () => _popupsManager.HidePopup(PopupResult.No);
        _view.CancelButton.ButtonDown += () => _popupsManager.HidePopup(PopupResult.Cancel);
    }

    protected override IViewTransition CreateTransition()
    {
        return new PanelSizeTransition(_view, _view.FadeObjectsContainer, _view.Panel,
            new ITweenableMenuElement[] { _view.YesButton, _view.NoButton, _view.CancelButton, _view.MessageMask },
            PanelDuration, ElementsDuration);
    }
}
