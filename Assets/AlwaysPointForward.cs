using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysPointForward : MonoBehaviour
{
    Rigidbody m_rigidBody;

    // Use this for initialization
    void Start()
    {
        m_rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(m_rigidBody.velocity);
    }
}
