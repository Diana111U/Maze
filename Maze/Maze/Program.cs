using System;
using System.IO;

public class Game
{
    public char[,] Map { get; set; }
    public int ExitX { get; set; }
    public int ExitY { get; set; }
    public int PlayerX { get; set; }
    public int PlayerY { get; set; }

    public Game(string filePath)
    {
        LoadMap(filePath);
    }

    private void LoadMap(string filePath)
    {
        var lines = File.ReadAllLines(filePath);
        Map=new char[lines.Length, lines[0].Length];//Кол-во строк, длина строк

        for (int i = 0; i < lines.Length; i++)//Проход по строкам
        {
            for (int j = 0; j < lines[i].Length; j++)//Проход по столбцам
            {
                Map[i, j] = lines[i][j];

                //Если мы нашли выход, сохраняем его координаты
                if (Map[i, j] == 'X')
                {
                    ExitX = i;
                    ExitY = j;
                }
            }
        }

        //Найдем начальную позицию игрока
        for (int i = 0; i < lines.Length; i++)
        {
            for (int j = 0; j < lines[i].Length; j++)
            {
                if (Map[i, j] == 'P') //P - игрок
                {
                    PlayerX = i;
                    PlayerY = j;
                    return;
                }
            }
        }
    }

    public void DrawMap()
    {
        Console.Clear();
        for (int i = 0; i < Map.GetLength(0); i++)//Строки
        {
            for (int j = 0; j < Map.GetLength(1); j++)//Столбцы
            {
                if (i == PlayerX && j == PlayerY)
                    Console.Write("P "); //Игрок
                else
                    Console.Write(Map[i, j] + " ");
            }
            Console.WriteLine();
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        //Путь к файлу карты
        string projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
        string filePath = Path.Combine(projectDirectory, "map.txt");

        Game game = new Game(filePath);
        game.DrawMap();

        Console.ReadKey();
    }
}

