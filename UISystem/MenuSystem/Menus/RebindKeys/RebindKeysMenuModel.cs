using Godot;
using Godot.Collections;
using System;
using UISystem.Core.Constants;
using UISystem.Core.MenuSystem;

namespace UISystem.MenuSystem.Models;
public class RebindKeysMenuModel : ISettingsMenuModel
{

    private bool _isRebinding;
    private string _currentlyRebindingAction;
    private int _currentlyRebindingEventIndex; // 0 - for keyboard, 1 - for joystick
    private Action _onFinishedRebinding;
    private readonly GameSettings _settings;

    public bool IsRebinding => _isRebinding;

    public bool HasUnappliedSettings => false;

    public RebindKeysMenuModel(GameSettings settings)
    {
        _settings = settings;
    }

    public void ResetToDefault()
    {
        _settings.ResetInputMapToDefault();
    }

    /// <summary>
    /// Starts the process of rebinding a key
    /// </summary>
    /// <param name="action">Action to rebind</param>
    /// <param name="index">0 - keyboard, 1 - joystick</param>
    /// <param name="onFinishedRebinding"></param>
    public void StartRebinding(string action, int index = 0, Action onFinishedRebinding = null)
    {
        _onFinishedRebinding = onFinishedRebinding;
        _currentlyRebindingEventIndex = index;
        _currentlyRebindingAction = action;
        _isRebinding = true;
    }

    public void RebindKey(InputEvent key)
    {
        if (IsCancellingRebinding(key))
        {
            EndRebinding();
            return;
        }

        Array<InputEvent> currentEvents = InputMap.ActionGetEvents(_currentlyRebindingAction);
        // rebinding only if input comes from corresponding device and it is not the same event as existing events
        // in case there are alternative events (like wasd/arrows)
        // TODO: maybe switch the current existing main/alt events when it is defined in different index
        if (!IsInputFromCorrectDevice(key) || IsEventDefinedInDifferentIndex(key, currentEvents))
        {
            return;
        }

        currentEvents[_currentlyRebindingEventIndex] = key;
        _settings.SaveInputActionKey(_currentlyRebindingAction, currentEvents);

        EndRebinding();
    }

    private static bool IsCancellingRebinding(InputEvent key)
    {
        bool cancel = false;
        if (key is InputEventKey kbPress)
        {
            if (kbPress.Keycode == Key.Escape)
                cancel = true;
        }
        else if (key is InputEventJoypadButton button)
        {
            if (button.ButtonIndex == JoyButton.Start)
            {
                cancel = true;
            }
        }
        return cancel;
    }

    private void EndRebinding()
    {
        _currentlyRebindingAction = "";
        _isRebinding = false;
        _onFinishedRebinding?.Invoke();
    }

    private bool IsInputFromCorrectDevice(InputEvent key)
    {
        return IsInputFromKeyboardMouse(key) || IsInputFromJoystick(key);
    }

    private bool IsInputFromKeyboardMouse(InputEvent key)
    {
        return (key is InputEventKey || key is InputEventMouseButton)
            && _currentlyRebindingEventIndex == InputsData.KeyboardEventIndex;
    }

    private bool IsInputFromJoystick(InputEvent key)
    {
        return (key is InputEventJoypadButton || key is InputEventJoypadMotion) // motion included because L2 and R2 are considered motion
            && _currentlyRebindingEventIndex == InputsData.JoystickEventIndex;
    }

    private bool IsEventDefinedInDifferentIndex(InputEvent key, Array<InputEvent> currentEvents)
    {
        bool result = true;
        var eventToCheck = currentEvents[_currentlyRebindingEventIndex];

        for (int i = 0; i < currentEvents.Count; i++)
        {
            if (key.IsMatch(eventToCheck)) // the same key that was before rebinding
            {
                result = false;
                break;
            }
            else if (key.IsMatch(currentEvents[i]))
            {
                break;
            }
            else if (!key.IsMatch(currentEvents[i]) && i == currentEvents.Count - 1)
            {
                result = false;
            }
        }

        return result;
    }

    public void SaveSettings()
    {
        // is not implemented in this setup
        // saving happens when player presses a key
        // if you want to change it - store the actions that player tried to rebind and save/discard them when the button is pressed
    }

    public void DiscardChanges()
    {
        // is not implemented in this setup
        // saving happens when player presses a key
        // if you want to change it - store the actions that player tried to rebind and save/discard them when the button is pressed
    }
}
