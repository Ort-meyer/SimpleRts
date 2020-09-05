using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class BaseUnit : MonoBehaviour
{
    [Serializable]
    public class UnitConfigData
    {
        public int faction;
    }

    [Serializable]
    public class UnitStateData
    {

    }

    public UnitConfigData m_configData;
    private UnitStateData m_stateData;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public abstract void M_MoveTo(Vector3 position);
    public abstract void M_AttackOrder(Transform target);
    public abstract void M_InflictDamage(float damage);
}
