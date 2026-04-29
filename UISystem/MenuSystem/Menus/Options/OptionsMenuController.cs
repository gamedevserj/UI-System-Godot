using UISystem.Core.MenuSystem;
using UISystem.Core.Views;
using UISystem.MenuSystem.Views;

namespace UISystem.MenuSystem.Controllers;
internal class OptionsMenuController : MenuControllerBase<IViewCreator<OptionsMenuView>, OptionsMenuView>
{

    public OptionsMenuController(IViewCreator<OptionsMenuView> viewCreator, IMenusManager menusManager) 
        : base(viewCreator, menusManager)
    { }   

    protected override void SetupElements()
    {
        View.ReturnButton.ButtonDown += OnReturnButtonDown;
        View.AudioSettingsButton.ButtonDown += OnAudioSettingsButtonDown;
        View.VideoSettingsButton.ButtonDown += OnVideoSettingsButtonDown;
        View.RebindKeysButton.ButtonDown += OnRebindKeysButtonDown;
        View.InterfaceSettingsButton.ButtonDown += OnInterfaceSettingsButtonDown;
    }

    private void OnAudioSettingsButtonDown()
    {
        View.SetLastSelectedElement(View.AudioSettingsButton);
        MenusManager.ShowMenu(typeof(AudioSettingsMenuView));
    }

    private void OnVideoSettingsButtonDown()
    {
        View.SetLastSelectedElement(View.VideoSettingsButton);
        MenusManager.ShowMenu(typeof(VideoSettingsMenuView));
    }

    private void OnRebindKeysButtonDown()
    {
        View.SetLastSelectedElement(View.RebindKeysButton);
        MenusManager.ShowMenu(typeof(RebindKeysMenuView));
    }

    private void OnInterfaceSettingsButtonDown()
    {
        View.SetLastSelectedElement(View.InterfaceSettingsButton);
        MenusManager.ShowMenu(typeof(InterfaceSettingsMenuView));
    }
}
