using System;
using UISystem.Core.PopupSystem;
using UISystem.Core.Views;

namespace UISystem.PopupSystem;

/// <summary>
/// A base class to adapt generic controller to Godot's specific parameters,
/// so that there is no need to specify InputEvent for every controller.
/// </summary>
/// <typeparam name="TViewCreator">Type of view creator. Must implement <see cref="IViewCreator{TView}"/>.</typeparam>
/// <typeparam name="TView">Type of view. Must implement <see cref="IPopupView"/>.</typeparam>
internal abstract class PopupControllerBase<TViewCreator, TView> : PopupController<TViewCreator, TView, PopupResult>
    where TViewCreator : IViewCreator<TView>
    where TView : IPopupView
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PopupControllerBase{TViewCreator, TView}"/> class.
    /// </summary>
    /// <param name="viewCreator">View creator.</param>
    /// <param name="popupsManager">Popups manager.</param>
    protected PopupControllerBase(TViewCreator viewCreator, IPopupsManager<PopupResult> popupsManager)
        : base(viewCreator, popupsManager)
    {
    }
}
