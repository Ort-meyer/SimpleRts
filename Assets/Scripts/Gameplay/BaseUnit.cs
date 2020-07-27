using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnit : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void M_MoveTo(Vector3 position)
    {
        Debug.LogError("BaseUnit.M_MoveTo called: this should be virtual");
    }
}
