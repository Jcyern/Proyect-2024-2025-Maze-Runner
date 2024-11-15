using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace Laberinto_;

public class Laberinto
{
    //fila* columnas  // simetrica 

    public Casilla[,] matriz;

    public bool Exit;
    public bool Enter;

    public (int, int) begin;
    public (int, int) end;




    //builder 
    public Laberinto(int n)
    {
        matriz = new Casilla[n, n];

        for (int i = 0; i < matriz.GetLength(0); i++)
        {
            for (int j = 0; j < matriz.GetLength(1); j++)
            {
                //se inicializam las casillas con la posicion y pared false 
                matriz[i, j] = new Casilla(i, j);
                matriz[i, j].Pared = false;
            }
        }
    }


    public void CreateSalida()
    {
        for (int i = matriz.GetLength(0) - 1; i > 0; i--)
        {
            if (Exit == false)
            {
                for (int j = matriz.GetLength(0) - 1; j >= 0; j--)
                {
                    if (matriz[j, i].Pared == false)
                    {
                        matriz[j, i].Salida = true;
                        Exit = true;
                        end = (j, i);


                        Debug.Print(" salida: " + i + j);
                        break;
                    }

                }
            }
            else
                break;

        }
    }

    public void Print()
    {
        for (int i = 0; i < matriz.GetLength(0); i++)
        {
            for (int j = 0; j < matriz.GetLength(1); j++)
            {
                if (matriz[i, j].Entrada == true)
                {
                    Console.Write(":" + "\t");
                }
                if (matriz[i, j].Salida == true)
                {
                    Console.Write("=" + "\t");
                }
                else if (matriz[i, j].Pared == false)
                {
                    System.Console.Write("1" + "\t");
                }
                else if (matriz[i, j].Pared == true)
                {
                    System.Console.Write("0" + "\t");
                }

            }
            System.Console.WriteLine();
        }
    }

    public void Conexiones()
    {
        for (int i = 0; i < matriz.GetLength(0); i++)
        {
            for (int j = 0; j < matriz.GetLength(1); j++)
            {
                Vecinas(matriz[i, j]);
            }
        }
    }

    public void CreateEntrada()
    {
        for (int i = 0; i < matriz.GetLength(0); i++)
        {
            if (Enter == false)
            {
                for (int j = 0; j < matriz.GetLength(1); j++)
                {
                    if (matriz[j, i].Pared == false)
                    {
                        matriz[j, i].Entrada = true;
                        Enter = true;
                        begin = (j, i);
                        break;
                    }



                }
            }
            else
                break;
        }
    }

    public void Paredes(int count)
    {

        Random random = new Random();

        for (int i = 0; i < count; i++)
        {
            int fila = random.Next(0, matriz.GetLength(0));
            int columna = random.Next(0, matriz.GetLength(0));

            if (matriz[fila, columna].Pared == false)
            {

                matriz[fila, columna].Pared = true;
            }

        }

        var list = FindAccesibleCells();


        for (int i = 0; i < matriz.GetLength(0); i++)
        {
            for (int j = 0; j < matriz.GetLength(1); j++)
            {

                if (list.Contains((matriz[i, j].F, matriz[i, j].C)) == false && matriz[i, j].Pared == false)
                {
                    matriz[i, j].Pared = true;
                }
                Vecinas(matriz[i, j]);
                if (matriz[i, j].Conexiones.Count == 1 && matriz[i, j].Salida == true)
                {
                    matriz[i, j].Pared = true;
                }
                if (matriz[i, j].AnyVecinas() == false)
                {
                    matriz[i, j].Pared = true;
                }

            }
        }

        CreateEntrada();
        CreateSalida();


    }

    public void Vecinas(Casilla casilla)
    {
        #region   Metodo  

        // Vecina de  == { def: es vecina sino es pared y pertence a al a;gina de la pos izq ,derecha , superior , inferior => todo con repecto a la casilla tomada como referencia }
        // Verifica las vecinas izq y derecha , si esas pos existen
        //lo mismo  con las pos superiores e inferiores
        #endregion


        //superior
        if (casilla.F - 1 >= 0 && matriz[casilla.F - 1, casilla.C].Pared == false)
        {
            casilla.Add(matriz[casilla.F - 1, casilla.C]);
        }
        //inferior 
        if (casilla.F + 1 < matriz.GetLength(0) && matriz[casilla.F + 1, casilla.C].Pared == false)
        {
            casilla.Add(matriz[casilla.F, casilla.C]);
        }


        //izq 
        if (casilla.C - 1 >= 0 && matriz[casilla.F, casilla.C - 1].Pared == false)
        {
            casilla.Add(matriz[casilla.F, casilla.C - 1]);
        }


        //der
        if (casilla.C + 1 < matriz.GetLength(0) && matriz[casilla.F, casilla.C + 1].Pared == false)
        {
            casilla.Add(matriz[casilla.F, casilla.C + 1]);
        }




    }




    public List<(int, int)> FindAccesibleCells()
    {
        List<(int, int)> accesibles = new();
        var cola = new Queue<(int, int)>();

        matriz[begin.Item1, begin.Item2].Visitada = true;
        cola.Enqueue((begin.Item1, begin.Item2));


        while (cola.Count > 0)
        {
            var casilla = cola.Dequeue();
            var row = casilla.Item1;
            var column = casilla.Item2;

            accesibles.Add((row, column));

            foreach ((int fila, int columna) in new[] { (-1, 0), (1, 0), (0, -1), (0, 1) })
            {
                int newRow = row + fila;
                int newColumn = column + columna;

                if (newRow >= 0 && newRow < matriz.GetLength(0) &&
                    newColumn >= 0 && newColumn < matriz.GetLength(0) &&
                    !matriz[newRow, newColumn].Visitada)
                {
                    matriz[newRow, newColumn].Visitada = true;
                    cola.Enqueue((newRow, newColumn));
                }
            }
        }
        return accesibles;
    }
}


public class Casilla
{
    public (int, int) Position { get; set; }
    public bool Pared { get; set; }

    public bool Trampa { get; set; }

    public bool Salida { get; set; }
    public bool Entrada { get; set; }

    public bool Visitada { get; set; }
    public List<Casilla> Conexiones = new List<Casilla>();

    public int F => Position.Item1;
    public int C => Position.Item2;

    public Casilla(int i, int j)
    {
        Position = (i, j);
    }


    public void Add(Casilla item)
    {
        Conexiones.Add(item);
    }



    public bool AnyVecinas()
    {
        if (Conexiones.Count == 0)
        {
            return false;
        }
        return true;
    }


}
