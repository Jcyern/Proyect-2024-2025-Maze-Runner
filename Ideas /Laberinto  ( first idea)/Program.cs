
using Laberinto_;

public class Program
{
    static void Main(string[] args)
    {
        Laberinto maze = new Laberinto(6);

        maze.Paredes(6*2);
        maze.Print();
    }
}
