using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Begin : MonoBehaviour
{
    public GameObject comenzar;
    // Update is called once per frame
    public  void Comenzar()
    {
        if (Juego.fichas.Count == 5)
        {
            comenzar.SetActive(true);

        }
        else if (Juego.fichas.Count < 5)
        {
            comenzar.SetActive(false);
        }

    }

    //Cambio_de_escena 
    public void Click()
    {
        GameObject.Find("Canvas").GetComponent<Manager>().NextScene(GameObject.Find("Canvas").GetComponent<Manager>().Scene_ChooseFiles, GameObject.Find("Canvas").GetComponent<Manager>().Scene_Game);
    }
}
