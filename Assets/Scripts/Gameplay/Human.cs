using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour
{

    private Player m_player;
    // Use this for initialization
    void Start()
    {
        m_player = GetComponent<Player>();
        InputManager.Instance.M_RegisterInputCallbackReleased(KeyCode.Mouse1, M_MoveOrder);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void M_MoveOrder()
    {
        RaycastHit[] hits = InputManager.Instance.M_GetMousePointerHits();
        if (hits.Length > 0)
        {
            m_player.M_MoveOrder(hits[0].point);
        }
    }
}
