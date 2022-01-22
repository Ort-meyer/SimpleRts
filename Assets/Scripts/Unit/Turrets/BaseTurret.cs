using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTurret : MonoBehaviour
{
    // Configure
    public int m_turretIndex;

    // State
    protected Transform m_target;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void M_SetTarget(Transform target)
    {
        m_target = target;
    }
}
