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

    public MainCameraConfigData m_configData;
    private MainCameraStateData m_stateData;
    private MainCameraBookkeepData m_bookkeepData;

    // Use this for initialization
    void Start()
    {
        //configData = new MainCameraConfigData();
        m_stateData = new MainCameraStateData();

        // Key movement
        InputManager.Instance.M_RegisterInputCallbackDown(KeyCode.A, MoveLeft);
        InputManager.Instance.M_RegisterInputCallbackDown(KeyCode.D, MoveRight);
        InputManager.Instance.M_RegisterInputCallbackDown(KeyCode.S, MoveDown);
        InputManager.Instance.M_RegisterInputCallbackDown(KeyCode.W, MoveUp);

        // Mouse rotation
        InputManager.Instance.M_RegisterInputCallbackPressed(KeyCode.Mouse1, StartMouseRotate);
        InputManager.Instance.M_RegisterInputCallbackReleased(KeyCode.Mouse1, StopMouseRotate);

        // Mouse movement
        InputManager.Instance.M_RegisterInputCallbackPressed(KeyCode.Mouse2, StartMouseMove);
        InputManager.Instance.M_RegisterInputCallbackReleased(KeyCode.Mouse2, StopMouseMove);

        InputManager.Instance.M_RegisterScrollInputCallback(MouseZoom);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 currentMousePosFactor = InputManager.Instance.M_GetScreenspaceMousePos();
        // The further the mouse is from its origin, the faster we rotate
        Vector2 mousePosDiff = currentMousePosFactor - m_stateData.holdingDownFactor;

        if (m_stateData.holdingDownForMovement)
        {
            Vector3 movement = new Vector3();//new Vector3(mousePosDiff.x, 0, mousePosDiff.y) * m_mouseMoveSpeed * Time.deltaTime;
            movement += transform.right.normalized * mousePosDiff.x * m_configData.mouseMoveSpeed * Time.deltaTime;
            movement += new Vector3(transform.forward.x, 0, transform.forward.z) * mousePosDiff.y * m_configData.mouseMoveSpeed * Time.deltaTime * -1;
            transform.position += movement;

        }
        else if (m_stateData.holdingDownForRotation)
        {
            Vector2 rotationAngles = m_configData.mouseRotationSpeed * Time.deltaTime * mousePosDiff;

            transform.Rotate(0, rotationAngles.x, 0, Space.World);
            transform.Rotate(rotationAngles.y, 0, 0, Space.Self);
        }
    }

    // TODO fix normalized movement vector. Right now, vertical + horizontal movements are faster together
    private void MoveLeft()
    {
        transform.position -= transform.right * m_configData.keyMoveSpeed * Time.deltaTime;
    }
    private void MoveRight()
    {
        transform.position += transform.right * m_configData.keyMoveSpeed * Time.deltaTime;
    }
    private void MoveUp()
    {
        transform.position += new Vector3(transform.forward.x, 0, transform.forward.z) * m_configData.keyMoveSpeed * Time.deltaTime;
    }
    private void MoveDown()
    {
        transform.position += -1 * new Vector3(transform.forward.x, 0, transform.forward.z) * m_configData.keyMoveSpeed * Time.deltaTime;
    }

    private void StartMouseMove()
    {
        m_stateData.holdingDownFactor = InputManager.Instance.M_GetScreenspaceMousePos();
        m_stateData.holdingDownForMovement = true;
        m_stateData.holdingDownForRotation = false;
    }
    private void StopMouseMove()
    {
        m_stateData.holdingDownForMovement = false;
    }
    private void StartMouseRotate()
    {
        m_stateData.holdingDownFactor = InputManager.Instance.M_GetScreenspaceMousePos();
        m_stateData.holdingDownForRotation = true;
        m_stateData.holdingDownForMovement = false;
    }
    private void StopMouseRotate()
    {
        m_stateData.holdingDownForRotation = false;
    }

    private void MouseZoom(float scrollValue)
    {
        // Static scroll speed. Consider using scrollvalue for more dynamic/fluid zooming?
        transform.position += Vector3.up * m_configData.mouseZoomSpeed * Mathf.Sign(scrollValue) * Time.deltaTime;
    }

}
