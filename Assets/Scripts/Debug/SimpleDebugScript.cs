using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDebugScript : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        InputManager.Instance.M_RegisterInputCallbackDown(KeyCode.Mouse1, M_RightClick);
        InputManager.Instance.M_RegisterInputCallbackDown(KeyCode.Mouse1, M_RightClick2);
        InputManager.Instance.M_RegisterInputCallbackDown(KeyCode.Mouse1, KeyModifier.Shift, M_ShiftRightClick);
        InputManager.Instance.M_RegisterInputCallbackDown(KeyCode.Mouse1, KeyModifier.None, M_ExclusiveRightClick);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void M_RightClick()
    {
        Debug.Log("Right click");
    }
    private void M_RightClick2()
    {
        Debug.Log("Right click2");
    }
    private void M_ShiftRightClick()
    {
        Debug.Log("Shift right click");
    }
    private void M_ExclusiveRightClick()
    {
        Debug.Log("Exclusive right click");
    }
}
