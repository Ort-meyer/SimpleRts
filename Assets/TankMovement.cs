using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class TankMovement : MonoBehaviour
{
    [Serializable]
    public class TankMovementConfigData
    {
        // Forward movement
        public float forwardAcceleration;
        public float breakAcceleration;
        public float maxMovementSpeed;
        public float stopDistance;

        // Rotation
        public float rotationSpeed;
    }

    [Serializable]
    public class TankMovementStateData
    {
        public float movementSpeed;
        //public float currentRotation;

        public Vector3 destination;
    }

    public class TankMovementBookkeepData
    {

    }

    public TankMovementConfigData m_configData;
    private TankMovementStateData m_stateData;

    // Use this for initialization
    void Start()
    {
        m_stateData = new TankMovementStateData();
        m_stateData.destination = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 toDest = m_stateData.destination - transform.position;
        toDest.y = 0;
        // Accelerate if we haven't reached stop distance from destination
        if (toDest.magnitude > m_configData.stopDistance)
        {
            m_stateData.movementSpeed += m_configData.forwardAcceleration * Time.deltaTime;
            m_stateData.movementSpeed = m_stateData.movementSpeed.LimitWithSign(m_configData.maxMovementSpeed);

        }
        else // Break
        {
            m_stateData.movementSpeed -= m_configData.breakAcceleration* Time.deltaTime;
            m_stateData.movementSpeed = m_stateData.movementSpeed.LimitWithSign(0);

        }
        transform.position += transform.forward.normalized * m_stateData.movementSpeed * Time.deltaTime;

        // Rotate towards destination
        if (toDest.magnitude > m_configData.stopDistance)
        {
            float angleToDest = transform.forward.GetDiffAngle2D(toDest);
            transform.Rotate(Vector3.up, angleToDest);
        }
    }

    public void M_StartMoveTo(Vector3 destination)
    {
        m_stateData.destination = destination;
    }
}
