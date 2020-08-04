using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerPlayer : MonoBehaviour
{

    private Player m_player;
    public Transform m_debugMoveToPosition;
    public Transform m_debugTarget;
    // Use this for initialization
    void Start()
    {
        m_player = GetComponent<Player>();
        StartCoroutine("M_StartMove");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator M_StartMove()
    {
        yield return new WaitForSeconds(2.0f);
        m_player.M_MoveOrder(m_debugMoveToPosition.position);
        m_player.M_AttackOrder(m_debugTarget);
    }
}
