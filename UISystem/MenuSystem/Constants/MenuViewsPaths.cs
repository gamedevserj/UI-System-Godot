using System;
using System.Collections.Generic;
using UISystem.MenuSystem.Views;

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

    public static readonly Dictionary<Type, string> Paths = new()
    {
        { typeof(MainMenuView), Main },
        { typeof(InGameMenuView), InGame },
        { typeof(PauseMenuView), Pause },
        { typeof(OptionsMenuView), Options },
        { typeof(AudioSettingsMenuView), AudioSettings },
        { typeof(VideoSettingsMenuView), VideoSettings },
        { typeof(RebindKeysMenuView), RebindKeys },
        { typeof(InterfaceSettingsMenuView), InterfaceSettings },
    };

}
