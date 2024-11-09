using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    #region  Escogiendo Casa 
    public static (string, int) house;

    //casas
    public GameObject[] Houses = new GameObject[4];
    //scenes
    public GameObject Scene_Casas;
    public GameObject Scene_ChooseFiles;

    public GameObject Scene_Game;
    #endregion

    public GameObject[] Barras = new GameObject[4];



    void Start()
    {
        File.fichas.Clear();
        
    }









    public void ChoosHouse()
    {
        foreach (var item in Houses)
        {
            if (item.GetComponent<House>().Choose == true)
            {
                house = (item.GetComponent<House>().Name, item.GetComponent<House>().Number);
                Debug.Log($"La casa escogida es {Manager.house.Item1}  {Manager.house.Item2}");
                NextScene(Scene_Casas, Scene_ChooseFiles);
                break;
            }
        }


        //Llamar para crear las fichas de la casa escogida 
        GameObject.Find("Canvas").GetComponent<SQLITE>().AddFiles();
    }


    public void NextScene(GameObject Scene_1, GameObject Scene_2)
    {
        Scene_1.SetActive(false);
        Scene_2.SetActive(true);
    }
}
