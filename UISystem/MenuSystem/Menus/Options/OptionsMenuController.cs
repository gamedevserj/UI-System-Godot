using Godot;
using UISystem.Core.MenuSystem;
using UISystem.Core.Views;
using UISystem.MenuSystem.Views;

namespace UISystem.MenuSystem.Controllers;
internal class OptionsMenuController : MenuControllerBase<IViewCreator<OptionsMenuView>, OptionsMenuView>
{
    public override MenuType Type => MenuType.Options;
    public OptionsMenuController(IViewCreator<OptionsMenuView> viewCreator, IMenuModel model, IMenusManager<InputEvent, MenuType> menusManager) : base(viewCreator, model, menusManager)
    { }   

    protected override void SetupElements()
    {
        _view.ReturnButton.ButtonDown += OnReturnButtonDown;
        _view.AudioSettingsButton.ButtonDown += OnAudioSettingsButtonDown;
        _view.VideoSettingsButton.ButtonDown += OnVideoSettingsButtonDown;
        _view.RebindKeysButton.ButtonDown += OnRebindKeysButtonDown;
        _view.InterfaceSettingsButton.ButtonDown += OnInterfaceSettingsButtonDown;
    }

    private void OnAudioSettingsButtonDown()
    {
        _view.SetLastSelectedElement(_view.AudioSettingsButton);
        _menusManager.ShowMenu(MenuType.AudioSettings);
    }

    private void OnVideoSettingsButtonDown()
    {
        _view.SetLastSelectedElement(_view.VideoSettingsButton);
        _menusManager.ShowMenu(MenuType.VideoSettings);
    }

    private void OnRebindKeysButtonDown()
    {
        _view.SetLastSelectedElement(_view.RebindKeysButton);
        _menusManager.ShowMenu(MenuType.RebindKeys);
    }

    private void OnInterfaceSettingsButtonDown()
    {
        _view.SetLastSelectedElement(_view.InterfaceSettingsButton);
        _menusManager.ShowMenu(MenuType.InterfaceSettings);
    }
}
