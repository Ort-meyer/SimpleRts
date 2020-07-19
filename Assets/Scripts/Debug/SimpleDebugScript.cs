using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDebugScript : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        InputManager.Instance.RegisterInputCallback(KeyCode.Mouse1, RightClick);
        InputManager.Instance.RegisterInputCallback(KeyCode.Mouse1, RightClick2);
        InputManager.Instance.RegisterInputCallback(KeyCode.Mouse1, KeyModifier.Shift, ShiftRightClick);
        InputManager.Instance.RegisterInputCallback(KeyCode.Mouse1, KeyModifier.None, ExclusiveRightClick);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void RightClick()
    {
        Debug.Log("Right click");
    }
    private void RightClick2()
    {
        Debug.Log("Right click2");
    }
    private void ShiftRightClick()
    {
        Debug.Log("Shift right click");
    }
    private void ExclusiveRightClick()
    {
        Debug.Log("Exclusive right click");
    }
}
