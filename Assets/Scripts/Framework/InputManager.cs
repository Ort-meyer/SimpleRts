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
    public delegate void ScrollInputMethod(float scrollValue);


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
            //KeyCode code = (KeyCode)(int)modifier;
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
    private Dictionary<KeyCode, List<InputCallback>> m_inputPressedCallbacks = new Dictionary<KeyCode, List<InputCallback>>();
    private Dictionary<KeyCode, List<InputCallback>> m_inputUpCallbacks = new Dictionary<KeyCode, List<InputCallback>>();

    private Dictionary<KeyCode, List<InputCallback>> m_inputDownCallbacks = new Dictionary<KeyCode, List<InputCallback>>();

    private List<ScrollInputMethod> m_scrollCallbacks = new List<ScrollInputMethod>();
    private RaycastHit[] m_mouseRaycastHits;

    /// Private methods



    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        M_DoRaycast();
        foreach (var kvp in m_inputPressedCallbacks)
        {
            KeyCode keyPressed = kvp.Key;
            List<InputCallback> callbacks = kvp.Value;
            if (Input.GetKeyDown(keyPressed))
            {
                foreach (InputCallback cb in callbacks)
                {
                    cb.CallIfConditionsFullfilled();
                }
            }
        }
        foreach (var kvp in m_inputUpCallbacks)
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
        foreach (var kvp in m_inputDownCallbacks)
        {
            KeyCode keyPressed = kvp.Key;
            List<InputCallback> callbacks = kvp.Value;
            if (Input.GetKey(keyPressed))
            {
                foreach (InputCallback cb in callbacks)
                {
                    cb.CallIfConditionsFullfilled();
                }
            }
        }

        float scrollVal = Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Abs(scrollVal) > 0)
        {
            foreach (ScrollInputMethod cb in m_scrollCallbacks)
            {
                cb(scrollVal);
            }
        }
    }

    private void M_DoRaycast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        m_mouseRaycastHits = Physics.RaycastAll(ray);
    }

    /// Public Methods

    /// <summary>
    /// Registers a callback method to be called whenever a key is pressed.
    /// Called regardless of modifier
    /// </summary>
    /// <param name="keyPress">What key should trigger the callback</param>
    /// <param name="callback">Callback method</param>
    public void M_RegisterInputCallbackPressed(KeyCode keyPress, InputMethod callback)
    {
        InputCallback newCb = new InputCallback(callback, KeyModifier.Any);
        this.m_inputPressedCallbacks.AddToList(keyPress, newCb);
    }

    /// <summary>
    /// Registers a callback method to be called whenever a key is pressed and
    /// when the specified modifier is held down (including only when no modifier
    /// is held down)
    /// </summary>
    /// <param name="keyPress">What key should trigger the callback</param>
    /// <param name="modifier">Callback method</param>
    /// <param name="callback">What modifier should be held down to trigger callback</param>
    public void M_RegisterInputCallbackPressed(KeyCode keyPress, KeyModifier modifier, InputMethod callback)
    {
        InputCallback newCb = new InputCallback(callback, modifier);
        this.m_inputPressedCallbacks.AddToList(keyPress, newCb);
    }

    /// <summary>
    /// Registers a callback method to be called whenever a key is released.
    /// Called regardless of modifier
    /// </summary>
    /// <param name="keyPress">What key should trigger the callback</param>
    /// <param name="callback">Callback method</param>
    public void M_RegisterInputCallbackReleased(KeyCode keyPress, InputMethod callback)
    {
        InputCallback newCb = new InputCallback(callback, KeyModifier.Any);
        this.m_inputUpCallbacks.AddToList(keyPress, newCb);
    }

    /// <summary>
    /// Registers a callback method to be called whenever a key is released and
    /// when the specified modifier is held down (including only when no modifier
    /// is held down)
    /// </summary>
    /// <param name="keyPress">What key should trigger the callback</param>
    /// <param name="modifier">Callback method</param>
    /// <param name="callback">What modifier should be held down to trigger callback</param>
    public void M_RegisterInputCallbackReleased(KeyCode keyPress, KeyModifier modifier, InputMethod callback)
    {
        InputCallback newCb = new InputCallback(callback, modifier);
        this.m_inputUpCallbacks.AddToList(keyPress, newCb);
    }

    /// <summary>
    /// Registers a callback method to be called whenever a key is held down.
    /// Called regardless of modifier
    /// </summary>
    /// <param name="keyPress">What key should trigger the callback</param>
    /// <param name="callback">Callback method</param>
    public void M_RegisterInputCallbackDown(KeyCode keyPress, InputMethod callback)
    {
        InputCallback newCb = new InputCallback(callback, KeyModifier.Any);
        m_inputDownCallbacks.AddToList(keyPress, newCb);
    }

    /// <summary>
    /// Registers a callback method to be called whenever a key is held down and
    /// when the specified modifier is held down (including only when no modifier
    /// is held down)
    /// </summary>
    /// <param name="keyPress">What key should trigger the callback</param>
    /// <param name="modifier">Callback method</param>
    /// <param name="callback">What modifier should be held down to trigger callback</param>
    public void M_RegisterInputCallbackDown(KeyCode keyPress, KeyModifier modifier, InputMethod callback)
    {
        InputCallback newCb = new InputCallback(callback, modifier);
        m_inputDownCallbacks.AddToList(keyPress, newCb);
    }

    /// <summary>
    ///  Registers a callback method to be called whenever the scroll whell is rotated
    /// </summary>
    /// <param name="callback"></param>
    public void M_RegisterScrollInputCallback(ScrollInputMethod callback)
    {
        m_scrollCallbacks.Add(callback);
    }

    /// <summary>
    /// Returns vector2 with current mouse position in screenspace
    /// </summary>
    /// <returns></returns>
    public Vector2 M_GetScreenspaceMousePos()
    {
        float xFactor = (Input.mousePosition.x - Screen.width / 2) / Screen.width;
        float yFactor = (Input.mousePosition.y - Screen.height / 2) / Screen.height * -1;
        return new Vector2(xFactor, yFactor);
    }

    /// <summary>
    /// Returns array of all hits from a ray cast from mouse pointer
    /// </summary>
    /// <returns></returns>
    public RaycastHit[] M_GetMousePointerHits()
    {
        return m_mouseRaycastHits;
    }
}
