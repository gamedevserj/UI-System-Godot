using Godot;
using UISystem.Common.ElementViews;
using UISystem.Common.Transitions;
using UISystem.Core.Elements.Interfaces;
using UISystem.Core.MenuSystem.Views;

namespace UISystem.MenuSystem.Views;
public partial class VideoSettingsMenuView : SettingsMenuView
{

    private const float PanelDuration = 0.5f;
    private const float ElementsDuration = 0.25f;

    [Export] private DropdownView windowModeDropdown;
    [Export] private DropdownView resolutionDropdown;
    [Export] private ButtonView saveSettingsButton;
    [Export] private ButtonView returnButton;
    [Export] private Control panel;

    public DropdownView WindowModeDropdown => windowModeDropdown;
    public DropdownView ResolutionDropdown => resolutionDropdown;
    public ButtonView SaveSettingsButton => saveSettingsButton;
    public ButtonView ReturnButton => returnButton;

    protected override void PopulateFocusableElements()
    {
        _focusableElements = new IFocusableControl[]
        { WindowModeDropdown, ResolutionDropdown, SaveSettingsButton, ResetButton, ReturnButton };
    }

    public override void Init()
    {
        base.Init();
        _transition = new PanelSizeTransition(this, fadeObjectsContainer, panel,
            new Control[] { ReturnButton.ResizableControl,
                ResolutionDropdown.ResizableControl, WindowModeDropdown.ResizableControl,
                SaveSettingsButton.ResizableControl, ResetButton.ResizableControl },
            PanelDuration, ElementsDuration);
    }

}
