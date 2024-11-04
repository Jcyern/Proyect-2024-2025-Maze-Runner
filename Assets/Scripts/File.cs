using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class File : MonoBehaviour
{
    public string Name;
    public string Habilidad;
    public int Enfriamieto;
    public int Velocidad;

    public TextMeshProUGUI nombre;
    public TextMeshProUGUI habilidad;

    public TextMeshProUGUI enfriamiento;


    public void LoadInterface()
    {
        nombre.text = Name;
        habilidad.text = Habilidad;
        enfriamiento.text = Enfriamieto.ToString();
    }

}
