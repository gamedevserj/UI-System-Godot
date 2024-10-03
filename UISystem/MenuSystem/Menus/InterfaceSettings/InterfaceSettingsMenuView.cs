using Godot;
using System;
using UISystem.Common.Elements;
using UISystem.Common.Interfaces;
using UISystem.Common.Transitions;
using UISystem.Common.Transitions.Interfaces;

namespace UISystem.MenuSystem.Views;
public partial class InterfaceSettingsMenuView : SettingsMenuView
{

    private const float PanelDuration = 0.5f;
    private const float ElementsDuration = 3.25f;

    [Export] private DropdownView controllerIconsDropdown;
    [Export] private ButtonView saveSettingsButton;
    [Export] private ButtonView returnButton;
    [Export] private Control panel;

    private IViewTransition _transition;

    public ButtonView SaveSettingsButton => saveSettingsButton;
    public ButtonView ReturnButton => returnButton;
    public DropdownView ControllerIconsDropdown => controllerIconsDropdown;

    protected override void PopulateFocusableElements()
    {
        _focusableElements = new IFocusableControl[] { ReturnButton, ControllerIconsDropdown, SaveSettingsButton, ResetButton };
    }

    public override void Init()
    {
        base.Init();
        _transition = new PanelSizeTransition(this, fadeObjectsContainer, panel,
            new Control[] { ReturnButton.ResizableizeControl, ControllerIconsDropdown.ResizableizeControl,
                SaveSettingsButton.ResizableizeControl, ResetButton.ResizableizeControl },
            PanelDuration, ElementsDuration);
    }

    public override void Hide(Action onHidden, bool instant)
    {
        _transition.Hide(onHidden, instant);
    }

    public override void Show(Action onShown, bool instant)
    {
        _transition.Show(onShown, instant);
    }

}

