using System.Diagnostics.SymbolStore;
using System.Numerics;
using System.Runtime.InteropServices.Marshalling;
using Microsoft.VisualBasic;
using Spectre.Console;
using Spectre.Console.Rendering;



//para usar Spectre.Console se necesita instalar los paquetes necesario de
// comando :  dotnet add package Spectre.Console   ojo \


using Tablero_idea_2;
public class Program
{
    public static int[,] matriz = new int[,]{
        {0,1,0,1,1},
        {1,1,1,1,0},
        {1,0,1,1,0}

     };


    static void Main(string[] args)
    {


        // var Green = new Style(Color.Green);
        // var Red = new Style(Color.Red);

        // for (int i = 0; i < matriz.GetLength(0); i++)
        // {
        //     for (int j = 0; j < matriz.GetLength(1); j++)
        //     {
        //         if (matriz[i, j] == 1)
        //         {
        //             AnsiConsole.Markup($"[green]{matriz[i, j]} \t [/]");
        //             El metodo Markup se usa para agregar texto con colores a la consola 
        //             sintaxis : Ainsi.Console.Markup( [color] .......text... [/]) siempre se cierra con [/] y el color enclausurado entre []corchetes

        //         }
        //         else
        //             AnsiConsole.Markup($"[red] {matriz[i, j]} \t [/]");

        //     }
        //     AnsiConsole.WriteLine();
        // }

        Laberinto lab = new Laberinto(10);
        lab.CreateObstacles();
        lab.Print();
        var visit = new bool[lab.maze.GetLength(0), lab.maze.GetLength(0)];
        System.Console.WriteLine(lab.obstacles.Count());
        foreach (var obs in lab.obstacles)
        {
            visit[obs.Item1, obs.Item2] = true;
        }
        var cola = new Queue<(int, int)>();
        cola.Enqueue((0, 1));

        (int, int) actual = (0, 0);

        lab.IsInvalid(visit, actual, cola);


    }


}