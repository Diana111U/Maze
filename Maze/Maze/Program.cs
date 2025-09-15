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

    public void MovePlayer(ConsoleKey key)
    {
        int newX = PlayerX;
        int newY = PlayerY;

        switch (key)
        {
            case ConsoleKey.UpArrow: newX--; break; //Вверх
            case ConsoleKey.DownArrow: newX++; break; //Вниз
            case ConsoleKey.LeftArrow: newY--; break; //Влево
            case ConsoleKey.RightArrow: newY++; break; //Вправо
        }

        if (newX >= 0 && newY >= 0 && newX < Map.GetLength(0) && newY < Map.GetLength(1))
        {
            if (Map[newX, newY] != '#') //Если не стена
            {
                //Очистим старую позицию игрока
                Map[PlayerX, PlayerY] = '.'; 

                //Перемещаем игрока на новое место
                PlayerX = newX;
                PlayerY = newY;

                //Отметим новую позицию игрока
                Map[PlayerX, PlayerY] = 'P'; 
            }
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        //Путь к файлу карты
        string filePath = "map.txt";

        Game game = new Game(filePath);
        game.DrawMap();

        while (true)
        {
            var key = Console.ReadKey(true).Key;
            game.MovePlayer(key);
            game.DrawMap();


            //Проверка на достижение выхода (символ 'X')
            if (game.PlayerX == game.ExitX && game.PlayerY == game.ExitY)
            {
                Console.WriteLine("Вы дошли до выхода!");
                break;  //Выход из игры при достижении 'X'
            }

        }

        Console.ReadKey();
    }
}

