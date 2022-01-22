using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuilderManager : Singleton<BuilderManager>
{
    public List<GameObject> m_unitPrefabs;

    public List<GameObject> m_hullPrefabs;
    public List<GameObject> m_turretPrefabs;
    public List<GameObject> m_weaponPrefabs;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject M_BuildUnit(int prefabIndex)
    {
        return Instantiate(m_unitPrefabs[prefabIndex]);
    }

    private GameObject M_BuildHull(int hullIndex)
    {
        return Instantiate(m_hullPrefabs[hullIndex]);
    }
}
