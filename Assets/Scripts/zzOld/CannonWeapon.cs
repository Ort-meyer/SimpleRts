//using System.Collections;
//using System.Collections.Generic;
//using System;
//using UnityEngine;
//using Newtonsoft.Json.Linq;
//using Newtonsoft.Json;


//public class CannonWeapon : BaseWeapon
//{
//    public GameObject m_cannonObj;
//    public GameObject m_projectilePrefab;

//    // Configurable
//    public float m_elevationSpeed;
//    public float m_fireVelocity; // Should this be in the projectile? Have here for now
//    public float m_maxFireCooldown;
//    public float m_spread;

//    // Around local x
//    public float m_maxElevation;
//    public float m_minElevation;
//    // Around local y
//    public float m_maxTraverse;

//    // State

//    private float m_currentFireCooldown;
    



//    // Use this for initialization
//    void Start()
//    {

//        m_currentFireCooldown = m_maxFireCooldown;
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        m_currentFireCooldown -= Time.deltaTime;
//        if (m_target)
//        {
//            Vector3 toTarget = m_target.position - m_cannonObj.transform.position;
//            float targetRotationAngle = Helpers.GetAngleToHit(toTarget.magnitude, toTarget.y, m_fireVelocity); // Should be difference in y?

//            Transform cannon = m_cannonObj.transform;
//            Vector3 oldUp = cannon.up; // This looks silly with high traverse. Should have same default up as parent

//            cannon.rotation = Quaternion.LookRotation(toTarget.ZeroY().normalized, Vector3.up);
//            cannon.Rotate(new Vector3(1, 0, 0), -1 * targetRotationAngle, Space.Self);
//            cannon.rotation = Quaternion.LookRotation(cannon.forward, oldUp);

//            //Correct rotation with constraints
//            Vector3 eulerAngles = m_cannonObj.transform.localRotation.eulerAngles;
//            float x = eulerAngles.x;
//            x = (x > 180) ? x - 360 : x;
//            x *= -1;
//            if (x > m_maxElevation)
//            {
//                x = m_maxElevation;
//            }
//            if (x < -1 * m_minElevation)
//            {
//                x = -1 * m_minElevation;
//            }
//            float y = eulerAngles.y;
//            y = (y > 180) ? y - 360 : y;
//            if (y > m_maxTraverse)
//            {
//                y = m_maxTraverse;
//            }
//            if (y < -1 * m_maxTraverse)
//            {
//                y = -1 * m_maxTraverse;
//            }

//            eulerAngles = new Vector3(-1 * x, y, 0);
//            m_cannonObj.transform.localRotation = Quaternion.Euler(eulerAngles);

//            if (m_currentFireCooldown <= 0)
//            {
//                M_FireWeapon();
//                m_currentFireCooldown = m_maxFireCooldown;
//            }
//        }
//    }

//    public void M_SetTarget(Transform target)
//    {
//        m_target = target;
//    }

//    private void M_FireWeapon()
//    {
//        BasicProjectile newProjectile = Instantiate(m_projectilePrefab, m_cannonObj.transform.position, m_cannonObj.transform.rotation).GetComponent<BasicProjectile>();

//        System.Random random = new System.Random();
//        float spreadx = ((float)random.NextDouble() - 0.5f) * m_spread;
//        float spready = ((float)random.NextDouble() - 0.5f) * m_spread;
//        newProjectile.transform.Rotate(new Vector3(spreadx, spready));
//        // Spread it some
//        //float spreadx = Random.Range(-m_weaponSpread, m_weaponSpread);
//        //float spready = Random.Range(-m_weaponSpread, m_weaponSpread);
//        //newProjectile.transform.Rotate(spreadx, spready, 0, Space.Self);
//        newProjectile.GetComponent<Rigidbody>().velocity = newProjectile.transform.forward.normalized * m_fireVelocity;
//        // Add firing unit to the projectile so it doesnt hit itself
//        newProjectile.M_Init();
//        newProjectile.m_firingUnitObject = gameObject;
//        newProjectile.M_FireAtTarget(m_fireVelocity, m_target);

//        //firingUnit.M_AddRecoil(newProjectile.transform.forward * m_recoil);
//        //m_readyToFire = false;
//        //m_currentCooldown = m_maxCooldown;
//    }

//    public JObject M_GetSavedComponent()
//    {
//        JObject savedComponent = new JObject();
//        savedComponent.Add("PosX", m_cannonObj.transform.localPosition.x);
//        savedComponent.Add("PosY", m_cannonObj.transform.localPosition.y);
//        savedComponent.Add("PosZ", m_cannonObj.transform.localPosition.z);
//        savedComponent.Add("RotX", m_cannonObj.transform.localEulerAngles.x);
//        savedComponent.Add("RotY", m_cannonObj.transform.localEulerAngles.y);
//        savedComponent.Add("RotZ", m_cannonObj.transform.localEulerAngles.z);
//        savedComponent.Add("CurrentCooldown", m_currentFireCooldown);

//        return savedComponent;
//    }

//    public void M_CreateFromSavedComponent(JToken component)
//    {
//        m_cannonObj.transform.localPosition = new Vector3(
//            float.Parse(component["PosX"].ToString()),
//            float.Parse(component["PosY"].ToString()),
//            float.Parse(component["PosZ"].ToString()));
//        m_cannonObj.transform.localEulerAngles = new Vector3(
//            float.Parse(component["RotX"].ToString()),
//            float.Parse(component["RotY"].ToString()),
//            float.Parse(component["RotZ"].ToString()));
//        m_currentFireCooldown = float.Parse(component["CurrentCooldown"].ToString());
//    }
//}
