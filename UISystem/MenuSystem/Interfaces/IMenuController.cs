using Godot;
using MenuSystem.Enums;
using System;

namespace MenuSystem.Interfaces;
public interface IMenuController
{

    MenuType MenuType { get; }
    bool CanReturnToPreviousMenu { get; set; }

    void Init(Node parent);
    void Hide(MenuStackBehaviourEnum stackBehaviour, Action onComplete = null, bool instant = false);
    void Show(Action onComplete = null, bool instant = false);

    void HandleInputPressedWhenActive(InputEvent key);
    void DestroyView();

}
