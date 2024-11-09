using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Barra : MonoBehaviour
{

    public int pos;
    public File file;
    public string Name;

    public TextMeshProUGUI Nombre;



    public void LoadName()
    {
        Nombre.text = Name;
    }


    //remove element de los elegidos de la barra 
    public void Remove()
    {
        Juego.Remove(file);
        GameObject.Find("Escogiendo").GetComponent<Flechas>().barras[pos].GetComponent<Barra>().Name = "";
        GameObject.Find("Escogiendo").GetComponent<Flechas>().barras[pos].GetComponent<Barra>().LoadName();
        GameObject.Find("Escogiendo").GetComponent<Flechas>().barras[pos].SetActive(false);

    }
}
