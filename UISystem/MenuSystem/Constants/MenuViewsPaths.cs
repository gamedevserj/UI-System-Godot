using System.Collections.Generic;

namespace UISystem.MenuSystem.Constants;
internal partial class MenuViewsPaths
{

    private const string Folder = "res://UISystem/MenuSystem/Prefabs/";

    private const string Main = Folder + "MainMenuView.tscn";
    private const string InGame = Folder + "InGameMenuView.tscn";
    private const string Pause = Folder + "PauseMenuView.tscn";
    private const string Options = Folder + "OptionsMenuView.tscn";
    private const string AudioSettings = Folder + "AudioSettingsMenuView.tscn";
    private const string VideoSettings = Folder + "VideoSettingsMenuView.tscn";
    private const string RebindKeys = Folder + "RebindKeysMenuView.tscn";
    private const string InterfaceSettings = Folder + "InterfaceSettingsMenuView.tscn";

    public static readonly Dictionary<int, string> Paths = new()
    {
        { MenuType.Main, Main },
        { MenuType.InGame, InGame },
        { MenuType.Pause, Pause },
        { MenuType.Options, Options },
        { MenuType.AudioSettings, AudioSettings },
        { MenuType.VideoSettings, VideoSettings },
        { MenuType.RebindKeys, RebindKeys },
        { MenuType.InterfaceSettings, InterfaceSettings },
    };

}
