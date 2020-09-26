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
        //m_tankMovement = GetComponent<TankMovement>();
        //m_tankTurret = GetComponent<TankTurret>();
        //m_cannon = GetComponent<CannonWeapon>();

        //m_currentHp = m_maxHp;
    }

    private void Awake()
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

    public void M_InitComponentConnection()
    {
        //m_tankMovement = GetComponent<TankMovement>();
        //m_tankTurret = GetComponent<TankTurret>();
        //m_cannon = GetComponent<CannonWeapon>();
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
            GameManager.Instance.m_players[m_faction].M_RemoveUnit(gameObject.GetInstanceID()); // TODO assert that this is successfully removed?
            Destroy(this.gameObject);
        }
    }

    public override JObject M_GetSavedUnit()
    {
        JObject savedUnit = new JObject();
        // This unit
        savedUnit.Add("PrefabIndex", m_prefabIndex);
        savedUnit.Add("Faction", m_faction);
        savedUnit.Add("CurrentHP", m_currentHp);
        savedUnit.Add("PosX", transform.localPosition.x);
        savedUnit.Add("PosY", transform.localPosition.y);
        savedUnit.Add("PosZ", transform.localPosition.z);
        savedUnit.Add("RotX", transform.localEulerAngles.x);
        savedUnit.Add("RotY", transform.localEulerAngles.y);
        savedUnit.Add("RotZ", transform.localEulerAngles.z);

        savedUnit.Add("Movement", m_tankMovement.M_GetSavedComponent());
        savedUnit.Add("Cannon", m_cannon.M_GetSavedComponent());
        savedUnit.Add("Turret", m_tankTurret.M_GetSavedComponent());

        //savedUnit.Add("StateData", JsonConvert.SerializeObject(m_tankStateData));
        //savedUnit.Add("Movement", JsonConvert.SerializeObject(m_tankMovement.M_GetSavedComponent()));
        //savedUnit.Add("Cannon", JsonConvert.SerializeObject(m_cannon.M_GetSavedComponent()));
        //savedUnit.Add("Turret", JsonConvert.SerializeObject(m_tankTurret.M_GetSavedComponent()));

        return savedUnit;
    }

    public override void M_CreateFromUnit(JToken loadedUnitJson)
    {
        m_currentHp = float.Parse(loadedUnitJson["CurrentHP"].ToString());
        m_faction = Int32.Parse(loadedUnitJson["Faction"].ToString());
        transform.localPosition = new Vector3(
            float.Parse(loadedUnitJson["PosX"].ToString()),
            float.Parse(loadedUnitJson["PosY"].ToString()),
            float.Parse(loadedUnitJson["PosZ"].ToString()));
        transform.localEulerAngles = new Vector3(
            float.Parse(loadedUnitJson["RotX"].ToString()),
            float.Parse(loadedUnitJson["RotY"].ToString()),
            float.Parse(loadedUnitJson["RotZ"].ToString()));
        var d = loadedUnitJson["Movement"];
        m_tankMovement.M_CreateFromSavedComponent(loadedUnitJson["Movement"]);
        m_cannon.M_CreateFromSavedComponent(loadedUnitJson["Cannon"]);
        m_tankTurret.M_CreateFromSavedComponent(loadedUnitJson["Turret"]);

    }
}