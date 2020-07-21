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
        public float mouseMoveSpeed;
        public float mouseRotationSpeed;
        public float mouseZoomSpeed;
    }

    [Serializable]
    public class MainCameraStateData
    {
        public bool holdingDownForRotation;
        public bool holdingDownForMovement;
        public Vector2 holdingDownFactor;
    }

    public class MainCameraBookkeepData
    {

    }

    public MainCameraConfigData configData;
    private MainCameraStateData stateData;
    private MainCameraBookkeepData bookkeepData;

    // Use this for initialization
    void Start()
    {
        //configData = new MainCameraConfigData();
        stateData = new MainCameraStateData();

        // Key movement
        InputManager.Instance.RegisterInputCallbackDown(KeyCode.A, MoveLeft);
        InputManager.Instance.RegisterInputCallbackDown(KeyCode.D, MoveRight);
        InputManager.Instance.RegisterInputCallbackDown(KeyCode.S, MoveDown);
        InputManager.Instance.RegisterInputCallbackDown(KeyCode.W, MoveUp);

        // Mouse rotation
        InputManager.Instance.RegisterInputCallbackPressed(KeyCode.Mouse1, StartMouseRotate);
        InputManager.Instance.RegisterInputCallbackReleased(KeyCode.Mouse1, StopMouseRotate);

        // Mouse movement
        InputManager.Instance.RegisterInputCallbackPressed(KeyCode.Mouse2, StartMouseMove);
        InputManager.Instance.RegisterInputCallbackReleased(KeyCode.Mouse2, StopMouseMove);

        InputManager.Instance.RegisterScrollInputCallback(MouseZoom);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 currentMousePosFactor = InputManager.Instance.GetScreenspaceMousePos();
        // The further the mouse is from its origin, the faster we rotate
        Vector2 mousePosDiff = currentMousePosFactor - stateData.holdingDownFactor;

        if (stateData.holdingDownForMovement)
        {
            Vector3 movement = new Vector3();//new Vector3(mousePosDiff.x, 0, mousePosDiff.y) * m_mouseMoveSpeed * Time.deltaTime;
            movement += transform.right.normalized * mousePosDiff.x * configData.mouseMoveSpeed * Time.deltaTime;
            movement += new Vector3(transform.forward.x, 0, transform.forward.z) * mousePosDiff.y * configData.mouseMoveSpeed * Time.deltaTime * -1;
            transform.position += movement;

        }
        else if (stateData.holdingDownForRotation)
        {
            Vector2 rotationAngles = configData.mouseRotationSpeed * Time.deltaTime * mousePosDiff;

            transform.Rotate(0, rotationAngles.x, 0, Space.World);
            transform.Rotate(rotationAngles.y, 0, 0, Space.Self);
        }
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

    private void StartMouseMove()
    {
        stateData.holdingDownFactor = InputManager.Instance.GetScreenspaceMousePos();
        stateData.holdingDownForMovement = true;
        stateData.holdingDownForRotation = false;
    }
    private void StopMouseMove()
    {
        stateData.holdingDownForMovement = false;
    }
    private void StartMouseRotate()
    {
        stateData.holdingDownFactor = InputManager.Instance.GetScreenspaceMousePos();
        stateData.holdingDownForRotation = true;
        stateData.holdingDownForMovement = false;
    }
    private void StopMouseRotate()
    {
        stateData.holdingDownForRotation = false;
    }

    private void MouseZoom(float scrollValue)
    {
        // Static scroll speed. Consider using scrollvalue for more dynamic/fluid zooming?
        transform.position += Vector3.up * configData.mouseZoomSpeed * Mathf.Sign(scrollValue) * Time.deltaTime;
    }

}
