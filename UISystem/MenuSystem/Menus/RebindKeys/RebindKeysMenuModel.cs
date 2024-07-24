using Godot;
using Godot.Collections;
using System;
using UISystem.Common.Constants;
using UISystem.Constants;
using UISystem.MenuSystem.Interfaces;
using UISystem.PopupSystem.Enums;

namespace UISystem.MenuSystem.Models;
public class RebindKeysMenuModel : IMenuModel
{

    private bool _isRebinding;
    private string _currentlyRebindingAction;
    private int _currentlyRebindingEventIndex; // 0 - for keyboard, 1 - for joystick
    private Action _onFinishedRebinding;
    private readonly ConfigFile _config;

    public bool IsRebinding => _isRebinding;

    private static string ConfigLocation => ConfigData.ConfigLocation;
    private static string SectionName => ConfigData.KeysSectionName;

    public RebindKeysMenuModel(ConfigFile config)
    {
        _config = config;
        LoadInputMap();
    }    

    public void ResetToDefault(PopupResult result)
    {
        if (result == PopupResult.Yes)
        {
            InputMap.LoadFromProjectSettings();
            SaveInputMap();
        }
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
        if (!IsInputFromCorrectDevice(key) || IsEventDefinedInDifferentIndex(key, currentEvents))
        {
            return;
        }

        currentEvents[_currentlyRebindingEventIndex] = key;

        InputMap.ActionEraseEvents(_currentlyRebindingAction);
        foreach (var item in currentEvents)
        {
            InputMap.ActionAddEvent(_currentlyRebindingAction, item);
        }

        SaveInputMap();
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

    private void SaveInputMap()
    {
        for (var i = 0; i < InputsData.RebindableActions.Length; i++)
        {
            var events = InputMap.ActionGetEvents(InputsData.RebindableActions[i]);
            _config.SetValue(SectionName, InputsData.RebindableActions[i], events);
        }
        _config.Save(ConfigLocation);
    }

    private void LoadInputMap()
    {
        Error err = _config.Load(ConfigLocation);

        if (err != Error.Ok || !_config.HasSection(SectionName))
        {
            InputMap.LoadFromProjectSettings();
            SaveInputMap();
            return;
        }

        for (int i = 0; i < _config.GetSectionKeys(SectionName).Length; i++)
        {
            var action = _config.GetSectionKeys(SectionName)[i];
            Array<InputEvent> events = (Array<InputEvent>)_config.GetValue(SectionName, action);

            InputMap.ActionEraseEvents(action);
            for (int k = 0; k < events.Count; k++)
            {
                InputMap.ActionAddEvent(action, events[k]);
            }
        }
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

}
