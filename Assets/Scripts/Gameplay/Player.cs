using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    public GameObject[] m_debugunits;

    [Serializable]
    public class PlayerConfigData
    {
        public int faction;
    }

    [Serializable]
    public class PlayerStateData
    {
        public Dictionary<int, BaseUnit> units;
        public Dictionary<int, BaseUnit> selectedUnits;

        public PlayerStateData()
        {
            units = new Dictionary<int, BaseUnit>();
            selectedUnits = new Dictionary<int, BaseUnit>();
        }
    }

    public class PlayerBookkeepData
    {

    }

    public PlayerConfigData m_configData;
    private PlayerStateData m_stateData;

    // Use this for initialization
    void Start()
    {
        m_stateData = new PlayerStateData();
        //foreach (GameObject obj in m_debugunits)
        //{
        //    m_stateData.units.Add(obj.GetInstanceID(), obj.GetComponent<BaseUnit>());
        //}
        foreach(BaseUnit unit in FindObjectsOfType<BaseUnit>())
        {
            if(unit.m_configData.faction == m_configData.faction)
            {
                m_stateData.units.Add(unit.GetInstanceID(), unit);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void M_MoveOrder(Vector3 position)
    {
        foreach (BaseUnit unit in m_stateData.selectedUnits.Values)
        {
            unit.M_MoveTo(position);
        }
    }
    public void M_AttackOrder(Transform target)
    {
        foreach (BaseUnit unit in m_stateData.selectedUnits.Values)
        {
            unit.M_AttackOrder(target);
        }
    }

    public void M_SelectUnits(List<BaseUnit> units)
    {
        foreach (BaseUnit unit in units)
        {
            m_stateData.selectedUnits[unit.GetInstanceID()] = unit;
        }
    }

    public void M_ClearSelection()
    {
        m_stateData.selectedUnits.Clear();
    }

    public Dictionary<int, BaseUnit> M_GetAllUnits()
    {
        return m_stateData.units;
    }

    public void M_AddUnit(BaseUnit unit)
    {
        m_stateData.units.Add(unit.GetInstanceID(), unit); // Will fail in case of duplicate. TODO handle?
    }

    public void M_RemoveUnit(int instanceId)
    {
        m_stateData.units.Remove(instanceId);
        m_stateData.selectedUnits.Remove(instanceId);
    }
}
