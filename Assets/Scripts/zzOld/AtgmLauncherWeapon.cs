using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtgmLauncherWeapon : MonoBehaviour
{

    public GameObject m_launcherObject;
    public GameObject m_projectilePrefab;

    // Configurable
    public float m_maxFireCooldown;

    // State
    private Transform m_target;
    private float m_currentFireCooldown;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
