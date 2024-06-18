using Godot;
using System;
using UISystem.Common.Enums;
using UISystem.Constants;
using UISystem.MenuSystem.Interfaces;

namespace UISystem.MenuSystem.Models;
public class InterfaceSettingsMenuModel : IMenuModel
{

    public static event Action<ControllerIconsType> OnControllerIconsTypeChange;

    private ControllerIconsType _iconsType;
    private ControllerIconsType _tempIconsType;
    private readonly ConfigFile _config;

    public bool HasUnappliedSettings => _iconsType != _tempIconsType;
    public ControllerIconsType CurrentControllerIconsType => _tempIconsType;

    private static string ConfigLocation => ConfigData.ConfigLocation;
    private static string SectionName => ConfigData.InterfaceSectionName;
    private static string ControllerIconsKey => ConfigData.ControllerIconsKey;
    private static ControllerIconsType DefaultIconsType => ConfigData.DefaultControllerIconsType;

    public InterfaceSettingsMenuModel(ConfigFile config)
    {
        _config = config;
        LoadSettings();
    }

    public void SelectIconType(int index)
    {
        _tempIconsType = (ControllerIconsType)index;
        // invoking here in case you have elements in the InterfaceMenuView that show controller buttons
        // if you don't have such elements, you can just invoke it in SaveSettings() and remove it from ResetToDefault()
        OnControllerIconsTypeChange?.Invoke(_tempIconsType); 
    }

    public void SaveSettings()
    {
        _iconsType = _tempIconsType;
        SaveToConfig();
    }

    public void ResetToDefault()
    {
        _iconsType = _tempIconsType = DefaultIconsType;
        OnControllerIconsTypeChange?.Invoke(_tempIconsType);
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

