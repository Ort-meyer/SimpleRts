using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;


public class TankUnit : BaseUnit
{
    // Configurable
    public float m_maxHp;
    
    // State
    private float m_currentHp;


    private TankMovement m_tankMovement;
    private TankTurret m_tankTurret;
    private CannonWeapon m_cannon;


    // Use this for initialization
    void Start()
    {
        m_tankMovement = GetComponent<TankMovement>();
        m_tankTurret = GetComponent<TankTurret>();
        m_cannon = GetComponent<CannonWeapon>();

        m_currentHp = m_maxHp;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void M_MoveTo(Vector3 position)
    {
        m_tankMovement.M_StartMoveTo(position);
    }

    public override void M_AttackOrder(Transform target)
    {
        m_tankMovement.M_StopMoving();
        m_tankTurret.M_SetTarget(target);
        m_cannon.M_SetTarget(target);
    }

    public override void M_InflictDamage(float damage)
    {
        m_currentHp -= damage;
        if(m_currentHp <= 0)
        {
            GameManager.Instance.M_GetPlayer(m_faction).M_RemoveUnit(gameObject.GetInstanceID()); // TODO assert that this is successfully removed?
            Destroy(this.gameObject);
        }
    }

    public override string M_GetSavedUnit()
    {
        JObject savedUnit = new JObject();
        //savedUnit.Add("StateData", JsonConvert.SerializeObject(m_tankStateData));
        //savedUnit.Add("Movement", JsonConvert.SerializeObject(m_tankMovement.M_GetSavedComponent()));
        //savedUnit.Add("Cannon", JsonConvert.SerializeObject(m_cannon.M_GetSavedComponent()));
        //savedUnit.Add("Turret", JsonConvert.SerializeObject(m_tankTurret.M_GetSavedComponent()));

        return savedUnit.ToString();
    }

    public override void M_CreateFromUnit(JObject loadedUnit)
    {

    }
}