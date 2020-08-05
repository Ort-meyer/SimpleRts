using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankUnit : BaseUnit
{

    private TankMovement m_tankMovement;
    private TankTurret m_tankTurret;
    private CannonWeapon m_cannon;


    // Use this for initialization
    void Start()
    {
        m_tankMovement = GetComponent<TankMovement>();
        m_tankTurret = GetComponent<TankTurret>();
        m_cannon = GetComponent<CannonWeapon>();
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
        m_tankTurret.M_SetTarget(target);
        m_cannon.M_SetTarget(target);
    }
}