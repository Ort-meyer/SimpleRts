using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDebugScript : MonoBehaviour
{

    public GameObject unitToSave;


    private string savedObject;

    //abstract class Saveable
    //{
    //    public abstract void M_Save();
    //    public abstract GameObject M_Load();
    //}

    //class TestClass:Saveable
    //{
    //    public TestClass()
    //    {

    //    }

    //    public override void M_Save()
    //    {

    //    }

    //    public override GameObject M_Load()
    //    {

    //    }
    //}


    // Use this for initialization
    void Start()
    {
        //InputManager.Instance.M_RegisterInputCallbackDown(KeyCode.Mouse1, M_RightClick);
        //InputManager.Instance.M_RegisterInputCallbackDown(KeyCode.Mouse1, M_RightClick2);
        //InputManager.Instance.M_RegisterInputCallbackDown(KeyCode.Mouse1, KeyModifier.Shift, M_ShiftRightClick);
        //InputManager.Instance.M_RegisterInputCallbackDown(KeyCode.Mouse1, KeyModifier.None, M_ExclusiveRightClick);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.H))
        {
            savedObject = unitToSave.GetComponent<TankUnit>().M_GetSavedUnit().ToString();
            
            
            //Debug.Log(jsonStr);
        }
        else if(Input.GetKey(KeyCode.J))
        {
            unitToSave.GetComponent<TankUnit>().M_CreateFromUnit(savedObject);
        }

    }

    private void M_RightClick()
    {
        Debug.Log("Right click");
    }
    private void M_RightClick2()
    {
        Debug.Log("Right click2");
    }
    private void M_ShiftRightClick()
    {
        Debug.Log("Shift right click");
    }
    private void M_ExclusiveRightClick()
    {
        Debug.Log("Exclusive right click");
    }
}
