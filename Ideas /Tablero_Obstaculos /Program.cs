// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using Tablero_Obstaculos;

public class Program
{
    static void Main(string[] args)
    {
        Tablero table = new Tablero(10);

        List<int> pos = new List<int>();

        for (int i = 0; i < table.matriz.GetLength(0); i++)
        {
            pos.Add(i);
        }
        Debug.Print(string.Join(",", pos));


        table.Pared(pos);

        table.Print();
    }
}