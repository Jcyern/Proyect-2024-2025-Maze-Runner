using System;
using System.ComponentModel.Design.Serialization;
using System.Diagnostics;

namespace Tablero_Obstaculos;

public class Tablero
{

    public Casilla[,] matriz;


    public Tablero(int n)
    {
        matriz = new Casilla[n, n];

        for (int i = 0; i < matriz.GetLength(0); i++)
        {
            for (int j = 0; j < matriz.GetLength(1); j++)
            {
                matriz[i, j] = new Casilla(i, j);
            }
        }
    }

    public void Print()
    {
        for (int i = 0; i < matriz.GetLength(0); i++)
        {
            for (int j = 0; j < matriz.GetLength(1); j++)
            {
                if (matriz[i, j].Pared == true)
                {
                    System.Console.Write("0" + "\t");
                }
                else 
                {
                    System.Console.Write("1" + "\t");
                }

            }
            System.Console.WriteLine();
        }
    }
    public void Pared(List<int> Pos)
    {
        Random random = new Random();

        List<int> pos = Pos;

        //avanzando por las columnas 
        for (int i = 0; i < matriz.GetLength(0); i++)
        {
            int row = RandomHashset.Random(pos.ToArray());
            if (matriz[row, i].IsFree)  //si esta free podemos poner pared 
            {
                matriz[row, i].IsFree = false;
                matriz[row, i].Pared = true;
                //metoda para  para q no pueda poner pared en la diagonal, columna , o fila en q haya una pared 
                Invalid(matriz[row, i].Position);
            }

            else
            {
                pos.Remove(row);

                while (pos.Count == 0)
                {
                    // ir removienodo posibilidades de pos random hasta q se agote 
                    row = RandomHashset.Random(pos.ToArray());
                    if (!matriz[row, i].IsFree)
                    {
                        pos.Remove(row);
                    }
                    else
                    { //si encuentra una libre para y sigue  de columna 
                        matriz[row, i].Pared = true; ;
                        matriz[row, i].IsFree = false;
                        Invalid(matriz[row, i].Position);
                        pos = Pos;
                        break;
                    }
                }


                pos = Pos;

            }

        }

    }

    public void Invalid((int, int) Position)
    {
        //eliminaar la posibilidad de poner pared en la diagonales 
        var row = Position.Item1;
        var column = Position.Item2;



        //diagonal 1 
        while (row - 1 >= 0 && row - 1 < matriz.GetLength(0) && column + 1 >= 0 && column + 1 < matriz.GetLength(0))
        {
            row--;
            column++;
            matriz[row, column].IsFree = false;
        }
        row = Position.Item1;
        column = Position.Item2;

        //diagonal 2 

        while (row - 1 >= 0 && row - 1 < matriz.GetLength(0) && column - 1 >= 0 && column - 1 < matriz.GetLength(0))
        {
            row--;
            column--;
            matriz[row, column].IsFree = false;
        }

        row = Position.Item1;
        column = Position.Item2;
        // avanzar todo a la derecha 
        while (column + 1 < matriz.GetLength(0))
        {
            column++;
            matriz[row, column].IsFree = false;

        }
        column = Position.Item2;
        //avanzar todo a la izquiera 
        while (column - 1 >= 0)
        {
            column--;
            matriz[row, column].IsFree = false;
        }

        column = Position.Item2;

        //avanzar todo hacia arriba 
        while (row + 1 < matriz.GetLength(0))
        {
            row++;
            matriz[row, column].IsFree = false;
        }
        row = Position.Item1;
        // avanzar todo hacia abajo 
        while (row - 1 >= 0)
        {
            row--;
            matriz[row, column].IsFree = false;
        }
        row = Position.Item1;


    }

}

public class Casilla
{
    public (int, int) Position { get; set; }
    public bool Pared { get; set; }
    public int F => Position.Item1;
    public int C => Position.Item2;

    public bool IsFree { get; set; }


    public Casilla(int i, int j)
    {
        Position = (i, j);
        IsFree = true;
    }
}



public class RandomHashset
{
    public static int Random(int[] array)
    {
        Random random = new Random();

        int number = random.Next(0, array.Length);
        return array[number];
    }
}

