using Godot;
using UISystem.Core.PopupSystem;
using UISystem.Core.Views;

namespace UISystem.PopupSystem;
// just a base class to adapt generic controller to Godot's specific parameters
// so that there is no need to specify InputEvent for every controller
internal abstract class PopupControllerBase<TViewCreator, TView> : PopupController<TViewCreator, InputEvent, TView, PopupType, PopupResult>
    where TViewCreator : IViewCreator<TView>
    where TView : IPopupView
{
    protected PopupControllerBase(TViewCreator viewCreator, IPopupsManager<InputEvent, PopupType, PopupResult> popupsManager) : base(viewCreator, popupsManager)
    {
    }
}
