namespace UISystem.Core.MenuSystem;
public interface ISettingsMenuModel : IMenuModel
{

    bool HasUnappliedSettings { get; }

    void SaveSettings();
    void DiscardChanges();
    void ResetToDefault();

}
