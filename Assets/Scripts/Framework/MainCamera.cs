using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


public class MainCamera : MonoBehaviour
{
    // Configurable
    public float m_keyMoveSpeed;
    public float m_mouseMoveSpeed;
    public float m_mouseRotationSpeed;
    public float m_mouseZoomSpeed;

    // State
    private bool m_holdingDownForRotation;
    private bool m_holdingDownForMovement;
    private Vector2 m_holdingDownFactor;

    // Use this for initialization
    void Start()
    {
        //configData = new MainCameraConfigData();

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
        Vector2 mousePosDiff = currentMousePosFactor - m_holdingDownFactor;

        if (m_holdingDownForMovement)
        {
            Vector3 movement = new Vector3();//new Vector3(mousePosDiff.x, 0, mousePosDiff.y) * m_mouseMoveSpeed * Time.deltaTime;
            movement += transform.right.normalized * mousePosDiff.x * m_mouseMoveSpeed * Time.deltaTime;
            movement += new Vector3(transform.forward.x, 0, transform.forward.z) * mousePosDiff.y * m_mouseMoveSpeed * Time.deltaTime * -1;
            transform.position += movement;

        }
        else if (m_holdingDownForRotation)
        {
            Vector2 rotationAngles = m_mouseRotationSpeed * Time.deltaTime * mousePosDiff;

            transform.Rotate(0, rotationAngles.x, 0, Space.World);
            transform.Rotate(rotationAngles.y, 0, 0, Space.Self);
        }
    }

    // TODO fix normalized movement vector. Right now, vertical + horizontal movements are faster together
    private void MoveLeft()
    {
        transform.position -= transform.right * m_keyMoveSpeed * Time.deltaTime;
    }
    private void MoveRight()
    {
        transform.position += transform.right * m_keyMoveSpeed * Time.deltaTime;
    }
    private void MoveUp()
    {
        transform.position += new Vector3(transform.forward.x, 0, transform.forward.z) * m_keyMoveSpeed * Time.deltaTime;
    }
    private void MoveDown()
    {
        transform.position += -1 * new Vector3(transform.forward.x, 0, transform.forward.z) * m_keyMoveSpeed * Time.deltaTime;
    }

    private void StartMouseMove()
    {
        m_holdingDownFactor = InputManager.Instance.M_GetScreenspaceMousePos();
        m_holdingDownForMovement = true;
        m_holdingDownForRotation = false;
    }
    private void StopMouseMove()
    {
        m_holdingDownForMovement = false;
    }
    private void StartMouseRotate()
    {
        m_holdingDownFactor = InputManager.Instance.M_GetScreenspaceMousePos();
        m_holdingDownForRotation = true;
        m_holdingDownForMovement = false;
    }
    private void StopMouseRotate()
    {
        m_holdingDownForRotation = false;
    }

    private void MouseZoom(float scrollValue)
    {
        // Static scroll speed. Consider using scrollvalue for more dynamic/fluid zooming?
        transform.position += Vector3.up * m_mouseZoomSpeed * Mathf.Sign(scrollValue) * Time.deltaTime;
    }

}
