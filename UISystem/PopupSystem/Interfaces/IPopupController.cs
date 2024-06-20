using Godot;
using System;
using UISystem.PopupSystem.Enums;

namespace UISystem.PopupSystem.Interfaces;
public interface IPopupController
{

    PopupType PopupType { get; }
    void Init(Node parent);
    void Hide(PopupResult result);
    void Show(string message, Action<PopupResult> onHideAction);

}
