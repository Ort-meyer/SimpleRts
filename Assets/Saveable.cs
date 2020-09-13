using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saveable : MonoBehaviour
{
    // Every unit that is loadable somehow has to register these callbacks
    // Add to baseunit interface?? Is everything that's loadable also a unit?
    // Maybe have it in multiple interfaces as necessary?
    public delegate void SaveMethod();
    public delegate GameObject LoadMethod();

    private SaveMethod m_saveMethod;
    private LoadMethod m_loadMethod;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
