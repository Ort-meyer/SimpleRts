using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json.Linq;

public abstract class BaseUnit : MonoBehaviour
{

    public int m_faction;

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
    public abstract string M_GetSavedUnit();
    public abstract void M_CreateFromUnit(string loadedUnitStr);

}
