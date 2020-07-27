using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject m_debugunit;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void M_MoveOrder(Vector3 position)
    {
        m_debugunit.GetComponent<BaseUnit>().M_MoveTo(position);
    }
}
