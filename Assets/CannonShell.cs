using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CannonShell : MonoBehaviour
{
    public GameObject m_impactEffectPrefab;

    [Serializable]
    public class CannonShellConfigData
    {
        public float damage;
    }

    [Serializable]
    public class CannonShellStateData
    {
        public Transform target;
        public float speed;
    }

    [Serializable]
    public class CannonShellBookkeepingData
    {
        public GameObject firingUnitObject;
    }

    public CannonShellConfigData m_configData;
    private CannonShellStateData m_stateData;
    private CannonShellBookkeepingData m_bookData;

    // Setters and getters
    public GameObject m_firingUnitObject
    {
        get { return m_bookData.firingUnitObject; }
        set { m_bookData.firingUnitObject = value; }
    }

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
        m_stateData = new CannonShellStateData();
        m_bookData = new CannonShellBookkeepingData();
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
            if (firingUnitObject == m_bookData.firingUnitObject)
            {
                return;
            }
            unitHit.M_InflictDamage(m_configData.damage);
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
