using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    // State
    public int m_faction;
    
    public Dictionary<int, BaseUnit> m_units = new Dictionary<int, BaseUnit>();
    public Dictionary<int, BaseUnit> m_selectedUnits = new Dictionary<int, BaseUnit>();
    
    // Use this for initialization
    void Start()
    {
        foreach(BaseUnit unit in FindObjectsOfType<BaseUnit>())
        {
            if(unit.m_faction == m_faction)
            {
                m_units.Add(unit.GetInstanceID(), unit);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void M_MoveOrder(Vector3 position)
    {
        foreach (BaseUnit unit in m_selectedUnits.Values)
        {
            unit.M_MoveTo(position);
        }
    }
    public void M_AttackOrder(Transform target)
    {
        foreach (BaseUnit unit in m_selectedUnits.Values)
        {
            unit.M_AttackOrder(target);
        }
    }

    public void M_SelectUnits(List<BaseUnit> units)
    {
        foreach (BaseUnit unit in units)
        {
            m_selectedUnits[unit.GetInstanceID()] = unit;
        }
    }

    public void M_ClearSelection()
    {
        m_selectedUnits.Clear();
    }

    public Dictionary<int, BaseUnit> M_GetAllUnits()
    {
        return m_units;
    }

    public void M_AddUnit(BaseUnit unit)
    {
        m_units.Add(unit.GetInstanceID(), unit); // Will fail in case of duplicate. TODO handle?
    }

    public void M_RemoveUnit(int instanceId)
    {
        m_units.Remove(instanceId);
        m_selectedUnits.Remove(instanceId);
    }
}
