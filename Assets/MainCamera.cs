using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


public class MainCamera : MonoBehaviour
{
    [Serializable]
    public class MainCameraConfigData
    {
        public float keyMoveSpeed;
    }

    [Serializable]
    public class MainCameraStateData
    {

    }

    public MainCameraConfigData configData;
    private MainCameraStateData stateData;

    // Use this for initialization
    void Start()
    {
        //configData = new MainCameraConfigData();
        stateData = new MainCameraStateData();

        InputManager.Instance.RegisterInputCallbackDown(KeyCode.A, MoveLeft);
        InputManager.Instance.RegisterInputCallbackDown(KeyCode.D, MoveRight);
        InputManager.Instance.RegisterInputCallbackDown(KeyCode.S, MoveDown);
        InputManager.Instance.RegisterInputCallbackDown(KeyCode.W, MoveUp);
    }

    // Update is called once per frame
    void Update()
    {

    }

    // TODO fix normalized movement vector. Right now, vertical + horizontal movements are faster together
    private void MoveLeft()
    {
        transform.position -= transform.right * configData.keyMoveSpeed * Time.deltaTime;
    }
    private void MoveRight()
    {
        transform.position += transform.right * configData.keyMoveSpeed * Time.deltaTime;
    }
    private void MoveUp()
    {
        transform.position += new Vector3(transform.forward.x, 0, transform.forward.z) * configData.keyMoveSpeed * Time.deltaTime;
    }
    private void MoveDown()
    {
        transform.position += -1 * new Vector3(transform.forward.x, 0, transform.forward.z) * configData.keyMoveSpeed * Time.deltaTime;
    }
}
