using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

public class GameManager : Singleton<GameManager>
{
    public GameObject debugTankPrefab;
    public Dictionary<int, Player> m_players;

    // Use this for initialization
    void Start()
    {
        m_players = new Dictionary<int, Player>();
        foreach(Player player in FindObjectsOfType<Player>())
        {
            m_players[player.m_faction] = player;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.M))
        {
            M_SaveWorld();
        }
        if (Input.GetKeyUp(KeyCode.L))
        {
            M_LoadWorld();
        }
    }

    //public Player M_GetPlayer(int faction)
    //{
    //    foreach (Player player in m_players.Values)
    //    {
    //        if (player.m_faction == faction)
    //        {
    //            return player;
    //        }
    //    }
    //    return null;
    //}

    private void M_SaveWorld()
    {
        JObject savedWorld = new JObject();
        JArray units = new JArray();
        // Save all units
        foreach (Player player in m_players.Values)
        {
            foreach (BaseUnit unit in player.m_units.Values)
            {
                units.Add(unit.M_GetSavedUnit());
            }
        }
        savedWorld.Add("Units", units);
        System.IO.File.WriteAllText("test.txt", savedWorld.ToString());
    }

    private void M_LoadWorld()
    {
        string worldString = System.IO.File.ReadAllText("test.txt");
        JObject loadedWorld = JObject.Parse(worldString);
        foreach(var o in loadedWorld["Units"])
        {
            int prefabIndex = Int32.Parse(o["PrefabIndex"].ToString());
            //BaseUnit newUnit = Instantiate(debugTankPrefab).GetComponent<BaseUnit>();
            BaseUnit newUnit = BuilderManager.Instance.M_BuildUnit(prefabIndex).GetComponent<BaseUnit>();
            newUnit.M_CreateFromUnit(o);
            m_players[newUnit.m_faction].M_AddUnit(newUnit); // This is risky. Crashes if for some reason faction doesn't exist
        }
    }
}
