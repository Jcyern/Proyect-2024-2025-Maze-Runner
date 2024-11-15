using System;
using System.Diagnostics;
using System.Runtime.Versioning;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text.RegularExpressions;
using Spectre.Console;

namespace Tablero_idea_2;

public class Laberinto
{
    public HashSet<(int, int)> obstacles { get; set; }

    public Casilla[,] maze;


    //builder 
    public Laberinto(int n)
    {


        maze = new Casilla[n, n];
        for (int i = 0; i < maze.GetLength(0); i++)
        {
            for (int j = 0; j < maze.GetLength(1); j++)
            {
                maze[i, j] = new Casilla(i, j);
            }
        }
        this.obstacles = [];
    }


    public void Print()
    {
        for (int i = 0; i < maze.GetLength(0); i++)
        {
            for (int j = 0; j < maze.GetLength(1); j++)
            {
                if (!maze[i, j].IsPared)
                {
                    AnsiConsole.Markup($"[green]1 \t[/]");

                }
                else
                    AnsiConsole.Markup($"[red]0 \t[/]");


            }
            AnsiConsole.WriteLine();
        }
    }



    //Metodo de agregar paredes al azar
    public void CreateObstacles()
    {


        //creando los bordes del maze de obstaculos
        for (int i = 0; i < maze.GetLength(0); i++)
        {

            if (maze[0, i].Pos != (0, 1))
            {
                maze[0, i].IsPared = true;
                obstacles.Add((0, i));
            }


            maze[i, 0].IsPared = true;
            obstacles.Add((i, 0));

            if (maze[maze.GetLength(0) - 1, i].Pos != (maze.GetLength(0) - 1, maze.GetLength(0) - 2))
            {
                maze[maze.GetLength(0) - 1, i].IsPared = true;
                obstacles.Add((maze.GetLength(0) - 1, i));
            }
            maze[i, maze.GetLength(0) - 1].IsPared = true;
            obstacles.Add((i, maze.GetLength(0) - 1));
            Debug.Print(" obstaculos " + obstacles.Count);

        }
        Debug.Print("after for " + obstacles.Count);
        RandomObstacles();



        //hora de crear random 
        void RandomObstacles()
        {
            int cant = maze.GetLength(0)*2;
            Debug.Print(cant.ToString());

            while (cant != 0)
            {
                Random random = new Random();
                // para q no ponga obtaculos en los bordes q se supone que ya tienen obstaculos
                var row = random.Next(1, maze.GetLength(0) - 1);
                var column = random.Next(1, maze.GetLength(0) - 1);

                //random obstacle 
                if (maze[row, column].IsPared == true)
                {
                    cant++;
                }
                maze[row, column].IsPared = true;
                obstacles.Add((row, column));

                cant--;
            }
        }
    }






    // Metodo para saber cuand un laberinto es invalido , en este caso cuando se le crean islas
    // osea queda una casilla en true sin acceso rodeada de false 

    public void IsInvalid(bool[,] visit, (int, int) actual, Queue<(int, int)> cola)
    {
        if (cola.Count == 0)
        {
            //cola vacia , termino el recorrido

            bool condicion = true;
            for (int i = 0; i < visit.GetLength(0); i++)
            {
                if (condicion)
                {
                    for (int j = 0; j < visit.GetLength(0); j++)
                    {
                        if (!visit[i, j])
                        {   
                            condicion = false ;
                            Debug.Print("tablero invalido ");
                            System.Console.WriteLine("Is not Validdddddddd");
                            break;

                        }
                    }
                }
                else
                    break;
            }

            if (condicion)
                {Debug.Print("IsValid");
                  System.Console.WriteLine("Is Validd :0");
                }
            return;
        }
        else
        {
            actual = cola.Dequeue();
            visit[actual.Item1, actual.Item2] = true;
            Debug.Print("actual es :" + actual.Item1 + actual.Item2);

            // utilizamos un array direccionall para ver q rango de mov tiene para moverse 
            var lab = Direccion(actual);

            foreach (var item in lab)
            {
                if (!cola.Contains(item))
                {  //sino se encuentra  en la cola y el elemento esta en true ya 
                    if (visit[item.Item1, item.Item2] == false)
                        cola.Enqueue(item);
                }
            }
            IsInvalid(visit, actual, cola);
        }
    }


    public List<(int, int)> Direccion((int, int) Pos)
    {
        List<(int, int)> direct = new List<(int, int)>();
        // abajo
        if (Pos.Item1 + 1 < maze.GetLength(0) && !maze[Pos.Item1 + 1, Pos.Item2].IsPared)
        {
            direct.Add((Pos.Item1 + 1, Pos.Item2));
        }
        //arriba 
        if (Pos.Item1 - 1 >= 0 && !maze[Pos.Item1 - 1, Pos.Item2].IsPared)
        {
            direct.Add((Pos.Item1 - 1, Pos.Item2));
        }
        //dereha 
        if (Pos.Item2 + 1 < maze.GetLength(0) && !maze[Pos.Item1, Pos.Item2 + 1].IsPared)
        {
            direct.Add((Pos.Item1, Pos.Item2 + 1));
        }
        if (Pos.Item2 - 1 >= 0 && !maze[Pos.Item1, Pos.Item2 - 1].IsPared)
        {
            direct.Add((Pos.Item1, Pos.Item2 - 1));
        }


        return direct;
    }





}

public class Casilla
{
    (int, int) Position;
    public bool IsPared { get; set; }

    //Propiedades de accedo a Columna y Fila 
    public int Fila => Position.Item1;
    public int Columns => Position.Item2;
    public (int, int) Pos => Position;



    public Casilla(int i, int j)
    {
        Position = (i, j);

        IsPared = false;
    }


}
