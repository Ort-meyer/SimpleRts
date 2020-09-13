using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

public class TankMovement : MonoBehaviour
{
    // Configurable
    // Forward movement
    public float m_forwardAcceleration;
    public float m_breakAcceleration;
    public float m_maxMovementSpeed;
    public float m_stopDistance;
    // Rotation
    public float m_rotationSpeed;

    // State
    public float m_movementSpeed;
    //public float currentRotation;

    public Vector3 m_destination;

    
    // Use this for initialization
    void Start()
    {
        m_destination = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 toDest = m_stateData.destination - transform.position;
        //toDest.y = 0;
        //// Accelerate if we haven't reached stop distance from destination
        //if (toDest.magnitude > m_configData.stopDistance)
        //{
        //    m_stateData.movementSpeed += m_configData.forwardAcceleration * Time.deltaTime;
        //    m_stateData.movementSpeed = m_stateData.movementSpeed.LimitWithSign(m_configData.maxMovementSpeed);

        //}
        //else // Break
        //{
        //    m_stateData.movementSpeed -= m_configData.breakAcceleration* Time.deltaTime;
        //    m_stateData.movementSpeed = m_stateData.movementSpeed.LimitWithSign(0);

        //}
        //transform.position += transform.forward.normalized * m_stateData.movementSpeed * Time.deltaTime;

        //// Rotate towards destination
        //if (toDest.magnitude > m_configData.stopDistance)
        //{
        //    float angleToDest = transform.forward.GetDiffAngle2D(toDest);
        //    float angleToTurn = angleToDest.Sign() * Mathf.Min(angleToDest.Abs(), m_configData.rotationSpeed * Time.deltaTime);
        //    transform.Rotate(Vector3.up, angleToTurn);
        //}
    }

    public void M_StartMoveTo(Vector3 destination)
    {
        m_destination = destination;
        GetComponent<NavMeshAgent>().SetDestination(destination);
    }

    public void M_StopMoving()
    {
        m_destination = transform.position; // TODO improve. This is stupid ugly way of stopping
    }

    public string M_GetSavedComponent()
    {
        JObject savedComponent = new JObject();
        //savedComponent.Add("StateData", JsonConvert.SerializeObject(()));

        return savedComponent.ToString();
    }

    public void M_CreateFromSavedComponent(JObject component)
    {
        //m_stateData = component["stateData"];
    }
}
