using UISystem.Common.Enums;
using UISystem.Constants;
using UISystem.Core.MenuSystem;

namespace UISystem.MenuSystem.Models;

/// <summary>
/// Interface settings menu model.
/// </summary>
public class InterfaceSettingsMenuModel : ISettingsMenuModel
{
    private readonly GameSettings _settings;

    private ControllerIconsType _lastIconsType;

    /// <summary>
    /// Initializes a new instance of the <see cref="InterfaceSettingsMenuModel"/> class.
    /// </summary>
    /// <param name="settings">Game settings.</param>
    public InterfaceSettingsMenuModel(GameSettings settings)
    {
        _settings = settings;
        RememberLastSavedSettings();
    }

    /// <inheritdoc/>
    public bool HasUnappliedSettings => _settings.ControllerIconsType != _lastIconsType;

    /// <summary>
    /// Gets or sets the type of controller icons to display.
    /// </summary>
    public ControllerIconsType ControllerIconsType
    {
        get => _settings.ControllerIconsType;
        set => _settings.ControllerIconsType = value;
    }

    /// <summary>
    /// Selects the type of controller icons.
    /// </summary>
    /// <param name="index">Index of the controller icon type.</param>
    public void SelectIconType(int index)
    {
        ControllerIconsType = (ControllerIconsType)index;
    }

    /// <inheritdoc/>
    public void SaveSettings()
    {
        RememberLastSavedSettings();
        _settings.SaveInterfaceSettings();
    }

    /// <inheritdoc/>
    public void DiscardChanges()
    {
        ControllerIconsType = _lastIconsType;
    }

    /// <inheritdoc/>
    public void ResetToDefault()
    {
        ControllerIconsType = ConfigData.DefaultControllerIconsType;
        SaveSettings();
    }

    private void RememberLastSavedSettings()
    {
        _lastIconsType = ControllerIconsType;
    }
}
