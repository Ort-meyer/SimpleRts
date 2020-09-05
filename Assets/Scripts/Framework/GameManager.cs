using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    public List<Player> m_players;

    // Use this for initialization
    void Start()
    {
        m_players = new List<Player>(FindObjectsOfType<Player>());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public Player M_GetPlayer(int faction)
    {
        foreach (Player player in m_players)
        {
            if (player.m_configData.faction == faction)
            {
                return player;
            }
        }
        return null;
    }
}
