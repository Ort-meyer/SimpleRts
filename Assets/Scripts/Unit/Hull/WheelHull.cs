using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WheelHull : BaseHull
{
    // Config
    // Wheels that will rotate to movement direction
    public List<GameObject> m_steeringWheels;
    // Wheels that do not rotate and just follows
    public List<GameObject> m_followingWheels;

    public float m_maxSteeringAngle;
    // Used to scale wheel rotation with movement speed
    public float m_wheelSize;

    // Use this for initialization
    void Start()
    {
        // Following wheels should be all wheels, for purpose of rotation
        m_followingWheels.AddRange(m_steeringWheels);
    }

    // Update is called once per frame
    void Update()
    {
        NavMeshAgent agent = GetComponentInParent<NavMeshAgent>();

        float angleToTarget = 0;
        if (agent.remainingDistance > 0.1)
        {
            angleToTarget = transform.forward.GetDiffAngle2D(agent.steeringTarget - transform.position);
            angleToTarget = angleToTarget.LimitWithSign(m_maxSteeringAngle);
        }
        foreach(GameObject steeringWheelObj in m_steeringWheels)
        {
            steeringWheelObj.transform.localEulerAngles = steeringWheelObj.transform.localEulerAngles.SetY(angleToTarget);
        }
        foreach(GameObject wheelObj in m_followingWheels)
        {
            float moveSpeed = agent.velocity.magnitude * m_wheelSize; // Replace with actual move speed. This is just to see the wheels turn
            wheelObj.transform.Rotate(new Vector3(1, 0, 0), moveSpeed * Time.deltaTime, Space.Self);
        }
    }
}
