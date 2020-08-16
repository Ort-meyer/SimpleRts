using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CannonWeapon : MonoBehaviour
{
    public GameObject m_cannonObj;
    public GameObject m_projectilePrefab;

    [Serializable]
    public class CannonWeaponConfigData
    {
        public float elevationSpeed;
        public float fireVelocity; // Should this be in the projectile? Have here for now
        public float maxFireCooldown;
        // Around local x
        public float maxElevation;
        public float minElevation;
        // Around local y
        public float maxTraverse;
    }

    [Serializable]
    public class CannonWeaponStateData
    {
        public Transform target;
        public float currentFireCooldown;
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

        m_stateData.currentFireCooldown = m_configData.maxFireCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        m_stateData.currentFireCooldown -= Time.deltaTime;
        if (m_stateData.target)
        {
            Vector3 toTarget = m_stateData.target.position - m_cannonObj.transform.position;
            float targetRotationAngle = Helpers.GetAngleToHit(toTarget.magnitude, toTarget.y, m_configData.fireVelocity); // Should be difference in y?

            Transform cannon = m_cannonObj.transform;
            Vector3 oldUp = cannon.up; // This looks silly with high traverse. Should have same default up as parent

            cannon.rotation = Quaternion.LookRotation(toTarget.ZeroY().normalized, Vector3.up);
            cannon.Rotate(new Vector3(1, 0, 0), -1 * targetRotationAngle, Space.Self);
            cannon.rotation = Quaternion.LookRotation(cannon.forward, oldUp);

            //Correct rotation with constraints
            Vector3 eulerAngles = m_cannonObj.transform.localRotation.eulerAngles;
            float x = eulerAngles.x;
            x = (x > 180) ? x - 360 : x;
            x *= -1;
            if (x > m_configData.maxElevation)
            {
                x = m_configData.maxElevation;
            }
            if (x < -1 * m_configData.minElevation)
            {
                x = -1 * m_configData.minElevation;
            }
            float y = eulerAngles.y;
            y = (y > 180) ? y - 360 : y;
            if (y > m_configData.maxTraverse)
            {
                y = m_configData.maxTraverse;
            }
            if (y < -1 * m_configData.maxTraverse)
            {
                y = -1 * m_configData.maxTraverse;
            }

            eulerAngles = new Vector3(-1 * x, y, 0);
            m_cannonObj.transform.localRotation = Quaternion.Euler(eulerAngles);

            if (m_stateData.currentFireCooldown <= 0)
            {
                M_FireWeapon();
                m_stateData.currentFireCooldown = m_configData.maxFireCooldown;
            }
        }
    }

    public void M_SetTarget(Transform target)
    {
        m_stateData.target = target;
    }

    private void M_FireWeapon()
    {
        CannonShell newProjectile = Instantiate(m_projectilePrefab, m_cannonObj.transform.position, m_cannonObj.transform.rotation).GetComponent<CannonShell>();
        // Spread it some
        //float spreadx = Random.Range(-m_weaponSpread, m_weaponSpread);
        //float spready = Random.Range(-m_weaponSpread, m_weaponSpread);
        //newProjectile.transform.Rotate(spreadx, spready, 0, Space.Self);
        newProjectile.GetComponent<Rigidbody>().velocity = m_cannonObj.transform.forward.normalized * m_configData.fireVelocity;
        // Add firing unit to the projectile so it doesnt hit itself
        newProjectile.M_Init();
        newProjectile.m_firingUnitObject = this.gameObject;
        newProjectile.M_FireAtTarget(m_configData.fireVelocity, m_stateData.target);

        //firingUnit.M_AddRecoil(newProjectile.transform.forward * m_recoil);
        //m_readyToFire = false;
        //m_currentCooldown = m_maxCooldown;
    }
}
