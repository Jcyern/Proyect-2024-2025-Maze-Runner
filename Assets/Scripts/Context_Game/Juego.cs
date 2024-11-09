using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Juego : MonoBehaviour
{
     public static List<File> fichas = new List<File>();

     void Start()
     {
          if (fichas.Count >0)
               fichas.Clear();
     }
     public static void Remove(File file)
     {
          for (int i = 0; i < fichas.Count; i++)
          {
               if (fichas[i].Name == file.Name)
               {
                    fichas.RemoveAt(i);
               }
          }
          Debug.Log($"la cantidad de element en fichas {fichas.Count}");
     }

}
