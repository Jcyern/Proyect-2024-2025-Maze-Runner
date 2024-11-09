using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.iOS;

public class Flechas : MonoBehaviour
{
    public GameObject izq;
    public GameObject der;

    //para movernos en la lista de fichas 
    int current = 0;

    public GameObject[] cards = new GameObject[6];
    public List<GameObject> barras = new();
    List<File> fichas = File.fichas;





    Stack<int> Pila = new Stack<int>();

    void Start()
    {
        foreach (var barra in barras)
        {
            barra.SetActive(false);
        }
        Show();
    }

    public void Show()
    {
        for (int i = 0; i < cards.Length; i++)
        {
            Debug.Log(current);
            if (current != fichas.Count)
            {



                if (cards[i].activeSelf == false)
                {
                    cards[i].SetActive(true);
                }
                cards[i].GetComponent<File>().Name = fichas[current].Name;
                cards[i].GetComponent<File>().Habilidad = fichas[current].Habilidad;
                cards[i].GetComponent<File>().Enfriamieto = fichas[current].Enfriamieto;
                cards[i].GetComponent<File>().Velocidad = fichas[current].Velocidad;
                cards[i].GetComponent<File>().LoadInterface();
                current++;
            }
            else
            {
                Debug.Log("apaga ");
                if (i == cards.Length - 1)
                {
                    der.SetActive(false);
                }
                cards[i].SetActive(false);
            }


        }
        if (current == fichas.Count)
        {
            der.SetActive(false);
        }

    }
    public void Avanzar()
    {
        if (izq.activeSelf == false)
        {
            izq.SetActive(true);
        }


        Pila.Push(current);


        Show();


    }

    public void Back()
    {

        var item = Pila.Pop();
        Debug.Log("item: " + item);
        current = item - 6;
        Debug.Log(current);
        for (int i = 0; i < cards.Length; i++)
        {
            if (cards[i].activeSelf == false)
            {
                cards[i].SetActive(true);
            }
            cards[i].GetComponent<File>().Name = fichas[current].Name;
            cards[i].GetComponent<File>().Habilidad = fichas[current].Habilidad;
            cards[i].GetComponent<File>().Enfriamieto = fichas[current].Enfriamieto;
            cards[i].GetComponent<File>().Velocidad = fichas[current].Velocidad;
            current++;

            cards[i].GetComponent<File>().LoadInterface();


        }

        if (Pila.Any() == false)
        {
            izq.SetActive(false);
        }
        if (der.activeSelf == false)
        {
            der.SetActive(true);
        }
    }




    public void Add(File file)
    {
        Debug.Log("se va a ejecutar add");
        for (int i = 0; i < barras.Count; i++)
        {
            if (barras[i].GetComponent<Barra>().Name == "")
            {   //add a las cartas seleccionadas
                Juego.fichas.Add(file);
                Debug.Log(file.Name);
                Debug.Log(Juego.fichas.Count);


                barras[i].GetComponent<Barra>().file = file;
                barras[i].GetComponent<Barra>().pos = i;
                barras[i].GetComponent<Barra>().Name = file.Name;

                barras[i].GetComponent<Barra>().LoadName();
                barras[i].SetActive(true);

                break;
            }
        }
    }



}

