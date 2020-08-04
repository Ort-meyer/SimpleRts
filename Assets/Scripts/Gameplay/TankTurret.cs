using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class TankTurret : MonoBehaviour
{
    public GameObject m_turretObj;

    [Serializable]
    public class TankTurretConfigData
    {
        public float rotationSpeed;
    }

    [Serializable]
    public class TankTurretStateData
    {
        public Transform target;
    }

    public class TankTurretBookkeepData
    {

    }

    public TankTurretConfigData m_configData;
    private TankTurretStateData m_stateData;
    private TankTurretBookkeepData m_bookkeepData;

    // Use this for initialization
    void Start()
    {
        m_stateData = new TankTurretStateData();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_stateData.target)
        {
            Vector3 toTarget = m_stateData.target.position - m_turretObj.transform.position;
            float angleToTarget = m_turretObj.transform.forward.GetDiffAngle2D(toTarget);
            float angleToTurn = angleToTarget.Sign() * Mathf.Min(angleToTarget.Abs(), m_configData.rotationSpeed * Time.deltaTime);
            m_turretObj.transform.Rotate(Vector3.up, angleToTurn);
        }
    }

    public void M_SetTarget(Transform target)
    {
        m_stateData.target = target;
    }
}
