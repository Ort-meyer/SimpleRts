using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TankUnit : BaseUnit
{
    [Serializable]
    public class TankConfigData
    {
        public float maxHp;
    }

    [Serializable]
    public class TankStateData
    {
        public float currentHp;
    }

    [Serializable]
    public class TankBookkeepData
    {

    }

    public TankConfigData m_tankConfigData;
    private TankStateData m_tankStateData;
    private TankBookkeepData m_tankBookData;


    private TankMovement m_tankMovement;
    private TankTurret m_tankTurret;
    private CannonWeapon m_cannon;


    // Use this for initialization
    void Start()
    {
        m_tankMovement = GetComponent<TankMovement>();
        m_tankTurret = GetComponent<TankTurret>();
        m_cannon = GetComponent<CannonWeapon>();

        m_tankStateData = new TankStateData();
        m_tankBookData = new TankBookkeepData();

        m_tankStateData.currentHp = m_tankConfigData.maxHp;
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
        m_tankStateData.currentHp -= damage;
        if(m_tankStateData.currentHp <= 0)
        {
            GameManager.Instance.M_GetPlayer(m_configData.faction).M_RemoveUnit(gameObject.GetInstanceID()); // TODO assert that this is successfully removed?
            Destroy(this.gameObject);
        }
    }
}