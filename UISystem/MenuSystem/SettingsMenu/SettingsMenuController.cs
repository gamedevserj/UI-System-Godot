using AsyncAwaitBestPractices;
using UISystem.Constants;
using UISystem.Core.Elements;
using UISystem.Core.MenuSystem;
using UISystem.Core.PopupSystem;
using UISystem.Core.Views;
using UISystem.PopupSystem.Popups.Views;

namespace UISystem.MenuSystem.SettingsMenu;

/// <summary>
/// Settings menu controller.
/// </summary>
/// <typeparam name="TViewCreator">Type of view creator. Must implement <see cref="IViewCreator{TView}"/>.</typeparam>
/// <typeparam name="TView">Type of view. Must inherit <see cref="SettingsMenuView"/>.</typeparam>
/// <typeparam name="TModel">Type of model. Must implement <see cref="ISettingsMenuModel"/>.</typeparam>
internal abstract class SettingsMenuController<TViewCreator, TView, TModel>
    : MenuController<TViewCreator, TView>
    where TViewCreator : IViewCreator<TView>
    where TView : SettingsMenuView
    where TModel : ISettingsMenuModel
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SettingsMenuController{TViewCreator, TView, TModel}"/> class.
    /// </summary>
    /// <param name="viewCreator">View creator.</param>
    /// <param name="menusManager">Menus manager.</param>
    /// <param name="model">Menu model.</param>
    /// <param name="popupsManager">Popups manager.</param>
    protected SettingsMenuController(
        TViewCreator viewCreator,
        IMenusManager menusManager,
        TModel model,
        IPopupsManager popupsManager)
        : base(viewCreator, menusManager)
    {
        Model = model;
        PopupsManager = popupsManager;
    }

    /// <summary>
    /// Gets the model.
    /// </summary>
    protected TModel Model { get; private set; }

    /// <summary>
    /// Gets the popups manager.
    /// </summary>
    protected IPopupsManager PopupsManager { get; private set; }

    /// <inheritdoc/>
    public override void OnReturnButtonDown()
    {
        if (Model.HasUnappliedSettings)
        {
            View.SetLastSelectedElement(View.ReturnButton);
            CanReceivePhysicalInput = false;
            SwitchInteractability(false);
            PopupsManager
                .ShowPopup<YesNoCancelPopupView>(PopupMessages.SaveChanges, (result) =>
                {
                    OnReturnToPreviousMenuPopupClosed(result);
                    CanReceivePhysicalInput = true;
                })
                .SafeFireAndForget();
        }
        else
        {
            base.OnReturnButtonDown();
        }
    }

    /// <summary>
    /// Resets the view.
    /// </summary>
    protected abstract void UpdateFullView();

    /// <inheritdoc/>
    protected override void SetupElements()
    {
        View.ReturnButton.ButtonDown += OnReturnButtonDown;
        View.ResetButton.ButtonDown += OnResetToDefaultButtonDown;
    }

    private void OnReturnToPreviousMenuPopupClosed(PopupResult result)
    {
        switch (result)
        {
            case PopupResult.No:
                Model.DiscardChanges();
                base.OnReturnButtonDown();
                break;
            case PopupResult.Yes:
                Model.SaveSettings();
                base.OnReturnButtonDown();
                break;
            case PopupResult.Cancel:
                SwitchInteractability(true);
                break;
            default:
                SwitchInteractability(true);
                break;
        }
    }

    private void OnResetToDefaultButtonDown()
    {
        View.SetLastSelectedElement(View.ResetButton);
        SwitchInteractability(false);
        PopupsManager
            .ShowPopup<YesNoPopupView>(PopupMessages.ResetToDefault, (result) =>
                {
                    if (result == PopupResult.Yes)
                    {
                        Model.ResetToDefault();
                        UpdateFullView();
                    }

                    SwitchInteractability(true);
                })
            .SafeFireAndForget();
    }
}
