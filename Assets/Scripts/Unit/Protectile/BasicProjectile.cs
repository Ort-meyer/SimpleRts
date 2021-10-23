using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class BasicProjectile : MonoBehaviour
{
    public GameObject m_impactEffectPrefab;

    // Config
    public float m_damage;

    // State
    private Transform m_target;
    //private float m_speed;
    public GameObject m_firingUnitObject;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void M_Init()
    {

    }

    public void M_FireAtTarget(float fireSpeed, Transform target)
    {
        m_target = target;
        //m_speed = fireSpeed;
    }

    void OnTriggerEnter(Collider other)
    {
        BaseUnit unitHit = other.gameObject.GetComponentInParent<BaseUnit>();
        if (unitHit)
        {
            GameObject firingUnitObject = unitHit.gameObject;
            if (firingUnitObject == m_firingUnitObject)
            {
                return;
            }
            unitHit.M_InflictDamage(m_damage);
        }
        M_SpawnImpactEffect();
        Destroy(this.gameObject);
        // Possibly helpfull code from some example somewhere


    }

    private void M_SpawnImpactEffect()
    {
        GameObject newEffect = Instantiate(m_impactEffectPrefab);
        newEffect.transform.parent = ParticleManager.Instance.transform;
        newEffect.transform.position = transform.position;
        newEffect.SetActive(true);
        ParticleSystem ps = newEffect.GetComponent<ParticleSystem>();

        if (ps != null)
        {
            var main = ps.main;
            if (main.loop)
            {
                ps.gameObject.AddComponent<CFX_AutoStopLoopedEffect>();
                ps.gameObject.AddComponent<CFX_AutoDestructShuriken>();
            }
        }

        ParticleManager.Instance.M_AddParticle(newEffect);
    }
}
