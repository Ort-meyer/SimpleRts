using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuilderManager : Singleton<BuilderManager>
{
    public List<GameObject> m_unitPrefabs;

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
}
