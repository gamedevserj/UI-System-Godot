using Godot;
using System;

namespace PopupSystem.Interfaces;
public interface IPopupController
{

    void Init(Node parent);
    void Hide(bool result);
    void Show(string message, Action<bool> onHideAction);

}
