using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class File : MonoBehaviour
{

    public static List<File> fichas = new List<File>();



    public string Name;
    public string Habilidad;
    public int Enfriamieto;
    public int Velocidad;

    public TextMeshProUGUI nombre;
    public TextMeshProUGUI velocidad;
    public UnityEngine.UI.Image marco;
    public UnityEngine.UI.Image imagen_carta;

    public File(string Name, string Hability, int Enfriamiento, int Velocidad)
    {
        this.Name = Name;
        this.Habilidad = Hability;
        this.Enfriamieto = Enfriamiento;
        this.Velocidad = Velocidad;
    }

    //builder -- when we pass a File Directly
    public File(File file)
    {
        this.Name = file.Name;
        this.Habilidad = file.Habilidad;
        this.Enfriamieto = file.Enfriamieto;
        this.Velocidad = file.Velocidad;
    }
    public void LoadInterface()
    {
        nombre.text = Name;
        velocidad.text = Enfriamieto.ToString();
        marco.sprite = Resources.Load<Sprite>("Imagenes/Cards/" + Manager.house.Item2);
        // Debug.Log($"Imagenes/Photos/{Manager.house.Item1} /{Name}");
        imagen_carta.sprite = Resources.Load<Sprite>("Imagenes/Photos/" + Manager.house.Item1 + "/" + Name);
    }



    #region File  Interface

    public void Click()
    {
        GameObject.Find("Escogiendo").GetComponent<Flechas>().Add(new File(Name, Habilidad, Enfriamieto, Velocidad));
    }



    #endregion




}
