using Godot;
using UISystem.Core.MenuSystem.Controllers;
using UISystem.Core.MenuSystem.Interfaces;
using UISystem.Core.Transitions.Interfaces;
using UISystem.MenuSystem.Constants;
using UISystem.MenuSystem.Models;
using UISystem.MenuSystem.Views;
using UISystem.Transitions;

namespace UISystem.MenuSystem.Controllers;
internal class OptionsMenuController : MenuController<OptionsMenuView, OptionsMenuModel>
{

    private const float MainElementAnimationDuration = 0.25f;
    private const float SecondaryElementAnimationDuration = 0.5f;

    public override int Type => MenuType.Options;

    public OptionsMenuController(string prefab, OptionsMenuModel model, IMenusManager menusManager, Node parent)
        : base(prefab, model, menusManager, parent)
    { }

    protected override void SetupElements()
    {
        _view.ReturnButton.ButtonDown += OnReturnButtonDown;
        _view.AudioSettingsButton.ButtonDown += OnAudioSettingsButtonDown;
        _view.VideoSettingsButton.ButtonDown += OnVideoSettingsButtonDown;
        _view.RebindKeysButton.ButtonDown += OnRebindKeysButtonDown;
        _view.InterfaceSettingsButton.ButtonDown += OnInterfaceSettingsButtonDown;
        DefaultSelectedElement = _view.InterfaceSettingsButton;
    }

    private void OnReturnButtonDown()
    {
        OnReturnToPreviousMenuButtonDown();
    }

    private void OnAudioSettingsButtonDown()
    {
        _lastSelectedElement = _view.AudioSettingsButton;
        _menusManager.ShowMenu(MenuType.AudioSettings);
    }

    private void OnVideoSettingsButtonDown()
    {
        _lastSelectedElement = _view.VideoSettingsButton;
        _menusManager.ShowMenu(MenuType.VideoSettings);
    }

    private void OnRebindKeysButtonDown()
    {
        _lastSelectedElement = _view.RebindKeysButton;
        _menusManager.ShowMenu(MenuType.RebindKeys);
    }

    private void OnInterfaceSettingsButtonDown()
    {
        _lastSelectedElement = _view.InterfaceSettingsButton;
        _menusManager.ShowMenu(MenuType.InterfaceSettings);
    }

    protected override IViewTransition CreateTransition()
    {
        return new MainElementDropTransition(_view, _view.FadeObjectsContainer, _view.InterfaceSettingsButton,
            new[] { _view.ReturnButton, _view.AudioSettingsButton, _view.VideoSettingsButton, _view.RebindKeysButton },
            MainElementAnimationDuration,
            SecondaryElementAnimationDuration);
    }
}
