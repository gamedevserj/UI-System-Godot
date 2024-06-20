using Godot;
using System;

namespace UISystem.MenuSystem.Interfaces;
public interface IMenuView
{

    void Show(Action onShown, bool instant = false);
    void Hide(Action onHidden, bool instant = false);

}
