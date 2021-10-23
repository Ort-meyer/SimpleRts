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

    private List<BaseTurret> m_turrets = new List<BaseTurret>();
    private List<BaseWeapon> m_weapons = new List<BaseWeapon>();

    public GameObject DEBUGturretObj;

    // Use this for initialization
    void Start()
    {
        m_turrets.AddRange(GetComponentsInChildren<BaseTurret>());
        m_weapons.AddRange(GetComponentsInChildren<BaseWeapon>());
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
        foreach (BaseTurret turret in m_turrets)
        {
            turret.M_SetTarget(target);
        }
        foreach(BaseWeapon weapon in m_weapons)
        {
            weapon.M_SetTarget(target);
        }

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
