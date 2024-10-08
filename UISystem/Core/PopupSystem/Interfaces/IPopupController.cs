using Godot;
using System;
using UISystem.Core.PopupSystem.Enums;
using UISystem.Core.MenuSystem.Interfaces;

namespace UISystem.Core.PopupSystem.Interfaces;
public interface IPopupController
{

    PopupType PopupType { get; }
    void Init(Node parent);
    void HandleInputPressedWhenActive(InputEvent key);
    void Hide(PopupResult result, bool instant = false);
    void Show(IMenuController caller, string message, Action<PopupResult> onHideAction, bool instant);

}
