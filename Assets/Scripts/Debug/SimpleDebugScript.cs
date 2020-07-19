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
}
