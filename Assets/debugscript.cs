using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class debugscript : MonoBehaviour
{

    public GameObject m_playerTestUnit;
    public GameObject m_debugTestTarget;

    // Use this for initialization
    void Start()
    {
        Invoke("M_EngageTarget", 1.0f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void M_EngageTarget()
    {
        m_playerTestUnit.GetComponent<BaseUnit>().M_AttackOrder(m_debugTestTarget.transform);
    }
}
