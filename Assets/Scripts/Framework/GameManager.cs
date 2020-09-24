﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

public class GameManager : Singleton<GameManager>
{
    public GameObject debugTankPrefab;
    public List<Player> m_players;

    // Use this for initialization
    void Start()
    {
        m_players = new List<Player>(FindObjectsOfType<Player>());
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.L))
        {
            M_LoadWorld();
        }
    }

    public Player M_GetPlayer(int faction)
    {
        foreach (Player player in m_players)
        {
            if (player.m_faction == faction)
            {
                return player;
            }
        }
        return null;
    }

    private void M_SaveWorld()
    {
        JObject savedWorld = new JObject();
        JArray units = new JArray();
        // Save all units
        foreach (Player player in m_players)
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
            GameObject newTank = Instantiate(debugTankPrefab);
            newTank.GetComponent<BaseUnit>().M_CreateFromUnit(o);
        }
    }
}
