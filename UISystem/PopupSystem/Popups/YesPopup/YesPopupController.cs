using Godot;
using UISystem.Core.PopupSystem;
using UISystem.PopupSystem.Popups.Views;
using UISystem.Views;

namespace UISystem.PopupSystem.Popups.Controllers;
internal class YesPopupController<TViewCreator, TInputEvent> : PopupControllerBase<ViewCreator<YesPopupView>, YesPopupView>
{

    public override PopupType Type => PopupType.Yes;
    public override PopupResult PressedReturnPopupResult => PopupResult.Yes;

    public YesPopupController(ViewCreator<YesPopupView> viewCreator, IPopupsManager<InputEvent, PopupType, PopupResult> popupsManager) : base(viewCreator, popupsManager)
    { }

    protected override void SetupElements()
    {
        _view.YesButton.ButtonDown += () => _popupsManager.HidePopup(PopupResult.Yes);
    }

}
