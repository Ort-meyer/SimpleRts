using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankUnit : BaseUnit
{

    private TankMovement m_tankMovement;


    // Use this for initialization
    void Start()
    {
        m_tankMovement = GetComponent<TankMovement>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void M_MoveTo(Vector3 position)
    {
        m_tankMovement.M_StartMoveTo(position);
    }
}