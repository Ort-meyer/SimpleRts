using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CannonShell : MonoBehaviour
{
    [Serializable]
    public class CannonShellStateData
    {
        public GameObject firingUnitObject;
        public Transform target;
        public float speed;
    }

    private CannonShellStateData m_stateData;

    public GameObject m_firingUnitObject
    {
        get { return m_stateData.firingUnitObject; }
        set { m_stateData.firingUnitObject = value; }
    }

    // Use this for initialization
    void Start()
    {
        //m_stateData = new CannonWeaponStateData();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 toTarget = m_stateData.target.position - transform.position;
        Vector3 movement = m_stateData.speed * Time.deltaTime * toTarget.normalized;
        transform.position += movement;
        transform.forward = movement.normalized;
    }

    public void M_Init()
    {
        m_stateData = new CannonShellStateData();
    }

    public void M_FireAtTarget(float fireSpeed, Transform target)
    {
        m_stateData.target = target;
        m_stateData.speed = fireSpeed;
    }

    void OnTriggerEnter(Collider other)
    {
        BaseUnit unitHit = other.gameObject.GetComponentInParent<BaseUnit>();
        if (unitHit)
        {
            GameObject firingUnitObject = unitHit.gameObject;
            if (firingUnitObject == m_stateData.firingUnitObject)
            {
                return;
            }
        }
        //M_SpawnImpactEffect();
        Destroy(this.gameObject);
        // Possibly helpfull code from some example somewhere
        //foreach (ContactPoint contact in collision.contacts)
        //{
        //    Debug.DrawRay(contact.point, contact.normal, Color.white);
        //}

    }
}
