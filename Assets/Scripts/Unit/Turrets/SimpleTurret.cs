using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTurret : BaseTurret
{
    // Configurable
    public float m_rotationSpeed;

    // State

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (m_target)
        {
            Vector3 toTarget = m_target.position - transform.position;
            float angleToTarget = transform.forward.GetDiffAngle2D(toTarget);
            float angleToTurn = angleToTarget.Sign() * Mathf.Min(angleToTarget.Abs(), m_rotationSpeed * Time.deltaTime);
            transform.Rotate(Vector3.up, angleToTurn);
        }
    }

    public override void M_SetTarget(Transform target)
    {
        m_target = target;
    }
}