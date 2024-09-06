using Godot;
using System;
using UISystem.MenuSystem.Interfaces;
using UISystem.PopupSystem.Enums;

namespace UISystem.PopupSystem.Interfaces;
public interface IPopupController
{

    PopupType PopupType { get; }
    void Init(Node parent);
    void HandleInputPressedWhenActive(InputEvent key);
    void Hide(PopupResult result);
    void Show(IMenuController caller, string message, Action<PopupResult> onHideAction);

}
