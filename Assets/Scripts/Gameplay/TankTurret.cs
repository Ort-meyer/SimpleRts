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

    public JObject M_GetSavedComponent()
    {
        JObject savedComponent = new JObject();
        savedComponent.Add("PosX", m_turretObj.transform.localPosition.x);
        savedComponent.Add("PosY", m_turretObj.transform.localPosition.y);
        savedComponent.Add("PosZ", m_turretObj.transform.localPosition.z);
        savedComponent.Add("RotX", m_turretObj.transform.localEulerAngles.x);
        savedComponent.Add("RotY", m_turretObj.transform.localEulerAngles.y);
        savedComponent.Add("RotZ", m_turretObj.transform.localEulerAngles.z);

        return savedComponent;
    }

    public void M_CreateFromSavedComponent(JToken component)
    {
        m_turretObj.transform.localPosition = new Vector3(
            float.Parse(component["PosX"].ToString()),
            float.Parse(component["PosY"].ToString()),
            float.Parse(component["PosZ"].ToString()));
        m_turretObj.transform.localEulerAngles = new Vector3(
            float.Parse(component["RotX"].ToString()),
            float.Parse(component["RotY"].ToString()),
            float.Parse(component["RotZ"].ToString()));
    }
}
