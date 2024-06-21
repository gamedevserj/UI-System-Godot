using Godot;
using UISystem.Common.Enums;
using UISystem.Constants;
using UISystem.MenuSystem.Interfaces;

namespace UISystem.MenuSystem.Models;
public class InterfaceSettingsMenuModel : IMenuModel
{

    private ControllerIconsType _iconsType;
    private ControllerIconsType _tempIconsType;
    private readonly ConfigFile _config;
    private readonly GameSettings _settings;

    public bool HasUnappliedSettings => _iconsType != _tempIconsType;
    public ControllerIconsType CurrentControllerIconsType => _tempIconsType;

    private static string ConfigLocation => ConfigData.ConfigLocation;
    private static string SectionName => ConfigData.InterfaceSectionName;
    private static string ControllerIconsKey => ConfigData.ControllerIconsKey;
    private static ControllerIconsType DefaultIconsType => ConfigData.DefaultControllerIconsType;

    public InterfaceSettingsMenuModel(ConfigFile config, GameSettings settings)
    {
        _config = config;
        _settings = settings;
        LoadSettings();
    }

    public void SelectIconType(int index)
    {
        _tempIconsType = (ControllerIconsType)index;
        _settings.SetControllerIconsType(_tempIconsType);
    }

    public void SaveSettings()
    {
        _iconsType = _tempIconsType;
        SaveToConfig();
    }

    public void DiscardChanges()
    {
        _tempIconsType = _iconsType;
    }

    public void ResetToDefault()
    {
        _iconsType = _tempIconsType = DefaultIconsType;
        _settings.SetControllerIconsType(_tempIconsType);
        SaveSettings();
    }

    private void LoadSettings()
    {
        _iconsType = _tempIconsType = 
            (ControllerIconsType)(int)_config.GetValue(SectionName, ControllerIconsKey, (int)DefaultIconsType);
    }

    private void SaveToConfig()
    {
        _config.SetValue(SectionName, ControllerIconsKey, (int)_iconsType);
        _config.Save(ConfigLocation);
    }

}

