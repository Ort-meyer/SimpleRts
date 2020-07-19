using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    public delegate void InputMethod();

    public class InputCallback
    {
        //private List<KeyCode> keysDown;
        //private List<KeyCode> keysNotHeldDown;
        private InputMethod callback;

        public InputCallback(InputMethod callback)
        {
            this.callback = callback;
        }

        public void CallIfConditionsFullfilled()
        {
            callback();
        }
    }

    
    private Dictionary<KeyCode, List<InputCallback>> inputCallbacks = new Dictionary<KeyCode, List<InputCallback>>();
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
        foreach(var kvp in inputCallbacks)
        {
            KeyCode keyPressed = kvp.Key;
            List<InputCallback> callbacks = kvp.Value;
            if(Input.GetKeyUp(keyPressed))
            {
                foreach(InputCallback cb in callbacks)
                {
                    cb.CallIfConditionsFullfilled();
                }
            }
        }
    }

    public void RegisterInputCallback(KeyCode keyPress, InputMethod callback)
    {
        InputCallback newCb = new InputCallback(callback);
        this.inputCallbacks.AddToList(keyPress, newCb);
    }
}
