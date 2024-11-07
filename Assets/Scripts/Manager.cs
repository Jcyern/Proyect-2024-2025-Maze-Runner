using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    #region  Escogiendo Casa 
    private (string, int) house;

    //casas
    public GameObject[] Houses = new GameObject[4];
    //scenes
    public GameObject Scene_Casas;
    public GameObject Scene_ChooseFiles;
    #endregion

    public GameObject[] Barras = new GameObject[4];




    








    public void ChoosHouse()
    {
        foreach (var item in Houses)
        {
            if (item.GetComponent<House>().Choose == true)
            {
                house = (item.GetComponent<House>().Name, item.GetComponent<House>().Number);

                NextScene(Scene_Casas, Scene_ChooseFiles);
                break;
            }
        }
    }


    public void NextScene(GameObject Scene_1, GameObject Scene_2)
    {
        Scene_1.SetActive(false);
        Scene_2.SetActive(true);
    }
}
