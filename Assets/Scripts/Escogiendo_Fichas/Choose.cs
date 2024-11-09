using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Choose : MonoBehaviour
{
    public GameObject Mazo;


    public List<GameObject> barras = new List<GameObject>();




    public void Active()
    {
        if (Mazo.activeSelf == true)
        {
            Mazo.SetActive(false);
        }
        else
        {
            Mazo.SetActive(true);
        }
    }






}



