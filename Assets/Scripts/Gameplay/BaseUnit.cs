using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json.Linq;
using UnityEngine.AI;

public class BaseUnit : MonoBehaviour
{
    // Configue

    // State
    public int m_faction;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void M_MoveTo(Vector3 position)
    {
        GetComponent<NavMeshAgent>().SetDestination(position);
    }

    public void M_AttackOrder(Transform target)
    {
        // Instruct all towers and weapons to fire at target(s)
    }
    public void M_InflictDamage(float damage)
    {

    }
    public JObject M_GetSavedUnit()
    {
        // Get hull, turrets and weapon jobjects
        return new JObject();
    }
    public void M_CreateFromUnit(JToken loadedUnitJson)
    {
        // Create in hull, turret and weapon jobjects
    }
}
