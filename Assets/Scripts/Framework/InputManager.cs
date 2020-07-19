using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enum that maps available modifiers to keycodes
/// </summary>
public enum KeyModifier
{
    None = 0, // If no modifier should be held down
    Any = 1, // If any modifier can be held down
    Shift = KeyCode.LeftShift,
    Ctrl = KeyCode.LeftControl,
    Alt = KeyCode.LeftAlt,
};

public class InputManager : Singleton<InputManager>
{
    public delegate void InputMethod();
    
    /// <summary>
    /// Internal callback class 
    /// </summary>
    private class InputCallback
    {
        private InputMethod callback;
        KeyModifier modifier;

        public InputCallback(InputMethod callback, KeyModifier modifier)
        {
            this.callback = callback;
            this.modifier = modifier;
        }

        public void CallIfConditionsFullfilled()
        {
            KeyCode code = (KeyCode)(int)modifier;
            switch (modifier)
            {
                case KeyModifier.None:
                    if (!(Input.GetKey((KeyCode)KeyModifier.Shift)
                        || Input.GetKey((KeyCode)KeyModifier.Ctrl)
                        || Input.GetKey((KeyCode)KeyModifier.Alt)))
                    {
                        callback();
                    }
                    break;

                case KeyModifier.Shift:
                case KeyModifier.Ctrl:
                case KeyModifier.Alt:
                    if (Input.GetKey((KeyCode)modifier))
                    {
                        callback();
                    }
                    break;
                case KeyModifier.Any:
                    callback();
                    break;
            }
        }
    }

    /// Member variables

    // Dictionary of all callbacks to a certain keycode
    private Dictionary<KeyCode, List<InputCallback>> inputCallbacks = new Dictionary<KeyCode, List<InputCallback>>();

    /// Private methods

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        foreach (var kvp in inputCallbacks)
        {
            KeyCode keyPressed = kvp.Key;
            List<InputCallback> callbacks = kvp.Value;
            if (Input.GetKeyUp(keyPressed))
            {
                foreach (InputCallback cb in callbacks)
                {
                    cb.CallIfConditionsFullfilled();
                }
            }
        }
    }

    /// Public Methods

    /// <summary>
    /// Registers a callback method to be called whenever a key is released.
    /// Called regardless of modifier
    /// </summary>
    /// <param name="keyPress">What key should trigger the callback</param>
    /// <param name="callback">Callback method</param>
    public void RegisterInputCallback(KeyCode keyPress, InputMethod callback)
    {
        InputCallback newCb = new InputCallback(callback, KeyModifier.Any);
        this.inputCallbacks.AddToList(keyPress, newCb);
    }

    /// <summary>
    /// Registers a callback method to be called whenever a key is released and
    /// when the specified modifier is held down (including only when no modifier
    /// is held down)
    /// </summary>
    /// <param name="keyPress">What key should trigger the callback</param>
    /// <param name="modifier">Callback method</param>
    /// <param name="callback">What modifier should be held down to trigger callback</param>
    public void RegisterInputCallback(KeyCode keyPress, KeyModifier modifier, InputMethod callback)
    {
        InputCallback newCb = new InputCallback(callback, modifier);
        this.inputCallbacks.AddToList(keyPress, newCb);
    }
}
