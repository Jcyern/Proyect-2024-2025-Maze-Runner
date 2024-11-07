using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    public string Name;

    //asociar el number en unity 
    public int Number;

    public bool Choose;

    public void Click()
    {
        Choose = true;
        GameObject.Find("Canvas").GetComponent<Manager>().ChoosHouse();
    }


}
