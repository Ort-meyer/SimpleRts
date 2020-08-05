using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CannonWeapon : MonoBehaviour
{
    public GameObject m_cannonObj;

    [Serializable]
    public class CannonWeaponConfigData
    {
        public float elevationSpeed;
        public float fireSpeed; // Should this be in the projectile? Have here for now
    }

    [Serializable]
    public class CannonWeaponStateData
    {
        public Transform target;
    }

    public class CannonWeaponBookkeepData
    {

    }

    public CannonWeaponConfigData m_configData;
    private CannonWeaponStateData m_stateData;
    private CannonWeaponBookkeepData m_bookkeepData;

    // Use this for initialization
    void Start()
    {
        m_stateData = new CannonWeaponStateData();
        m_bookkeepData = new CannonWeaponBookkeepData();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_stateData.target)
        {
            Vector3 toTarget = m_stateData.target.position - m_cannonObj.transform.position;
            // Where the cannon currently points relative toTarget
            Vector3 toTargetCurrent = toTarget.normalized;
            toTargetCurrent.y = m_cannonObj.transform.forward.y;
            float rotationAngle = Vector3.SignedAngle(toTargetCurrent, toTarget, m_cannonObj.transform.right);
            rotationAngle = rotationAngle.Sign() * Mathf.Min(rotationAngle.Abs(), m_configData.elevationSpeed * Time.deltaTime);
            m_cannonObj.transform.Rotate(new Vector3(rotationAngle, 0, 0), Space.Self);
        }
    }

    public void M_SetTarget(Transform target)
    {
        m_stateData.target = target;
    }
}
