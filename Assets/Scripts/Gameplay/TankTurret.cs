using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

public class TankTurret : MonoBehaviour
{
    // Configurable
    public GameObject m_turretObj;
    public float m_rotationSpeed;
    
    // State
    private Transform m_target;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (m_target)
        {
            Vector3 toTarget = m_target.position - m_turretObj.transform.position;
            float angleToTarget = m_turretObj.transform.forward.GetDiffAngle2D(toTarget);
            float angleToTurn = angleToTarget.Sign() * Mathf.Min(angleToTarget.Abs(), m_rotationSpeed * Time.deltaTime);
            m_turretObj.transform.Rotate(Vector3.up, angleToTurn);
        }
    }

    public void M_SetTarget(Transform target)
    {
        m_target = target;
    }

    public string M_GetSavedComponent()
    {
        JObject savedComponent = new JObject();
        //savedComponent.Add("StateData", JsonConvert.SerializeObject((m_stateData)));

        return savedComponent.ToString();
    }
}
