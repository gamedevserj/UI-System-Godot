using Godot;
using System;

namespace UISystem.Core.MenuSystem.Interfaces;
public interface IMenuController
{

    int Type { get; }
    bool CanReturnToPreviousMenu { get; set; }

    void Init(Node parent);
    void Hide(int menuChangeType, Action onComplete = null, bool instant = false);
    void Show(Action onComplete = null, bool instant = false);

    void HandleInputPressedWhenActive(InputEvent key);
    void DestroyView();

}
