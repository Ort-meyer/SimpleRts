using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugUnitSpawner : MonoBehaviour
{
    public List<GameObject> m_playerSpawnPositions;
    public List<GameObject> m_enemySpawnPositions;

    public Transform m_debugMoveToPos;

    public GameObject debugUnit;

    // Use this for initialization
    void Start()
    {
        foreach (GameObject spawnPos in m_playerSpawnPositions)
        {
            GameObject thisObject = BuilderManager.Instance.M_BuildUnit(0);
            GameManager.Instance.m_players[1].m_units.Add(thisObject.GetInstanceID(), thisObject.GetComponent<BaseUnit>());
            thisObject.transform.position = spawnPos.transform.position;
        }

        foreach (GameObject spawnPos in m_enemySpawnPositions)
        {
            GameObject thisObject = BuilderManager.Instance.M_BuildUnit(0);
            GameManager.Instance.m_players[2].m_units.Add(thisObject.GetInstanceID(), thisObject.GetComponent<BaseUnit>());
            thisObject.transform.position = spawnPos.transform.position;
            Debug.Log("derp");
        }

        // Make them move
        foreach (var kvp in GameManager.Instance.m_players[2].M_GetAllUnits())
        {
            kvp.Value.M_MoveTo(m_debugMoveToPos.position);
        }
        Debug.Log("done");

        debugUnit.GetComponent<BaseUnit>().M_AttackOrder(m_debugMoveToPos);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
