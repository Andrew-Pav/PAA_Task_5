using System;
using System.IO;

namespace PAA_Task_05
{
    class Program
    { 
        static char[,] ReadMap(string mapName, out int performerX, out int performerY)
        {
            performerX = 0;
            performerY = 0;

            string[] newFile = File.ReadAllLines($"map/{mapName}.txt");
            char[,] map = new char[newFile.Length, newFile[0].Length];

            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    map[i, j] = newFile[i][j];

                    if (map[i, j] == 'A')
                    {
                        map[i, j] = ' ';
                        performerX = i;
                        performerY = j;
                    }
                }
            }
            return map;
        }


        static void DrawMap(char[,] map, int sum, int HP)
        {
            Console.SetCursorPosition(20, 0);
            Console.Write("Количество монет: ");
            Console.Write(sum);
            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    Console.Write(map[i, j]);
                }
                Console.WriteLine();
            }
            Console.SetCursorPosition(20, 9);
            Console.Write("Ваши HP");
            Console.SetCursorPosition(20, 10);
            int notFullHp = HP / 10;
            Console.Write("[");
            for (int i = 1; i <= notFullHp; i++)
            {
                Console.Write("#");
            }
            for (int i = notFullHp; i < 10; i++)
            {
                Console.Write("_");
            }
            Console.WriteLine("]");
        }

        static void Main(string[] args)
        {
            int sum = 0;
            int HP = 100;
            Console.CursorVisible = false;
            int performerX, performerY;
            char[,] map = ReadMap("level01", out performerX, out performerY);
            //DrawMap(map);
            Console.SetCursorPosition(0, 15);
            Console.WriteLine("(A) - это вы. Движение осуществляется на стрелочки");
            Console.WriteLine("Соберите все монеты (0). Не задевайте врагов (%), они отнимают 20 HP");
            Console.WriteLine("------------------------------------------------");
            Console.SetCursorPosition(0, 0);
            int userX = 3, userY = 3;

            while (true)
            {
                DrawMap(map, sum, HP);

                Console.SetCursorPosition(userX, userY);
                Console.Write('A');

                ConsoleKeyInfo charKey = Console.ReadKey();

                switch (charKey.Key)
                {
                    case ConsoleKey.LeftArrow:
                        if (map[userY, userX - 1] != '#')
                            userX--;
                        break;
                    case ConsoleKey.RightArrow:
                        if (map[userY, userX + 1] != '#')
                            userX++;
                        break;
                    case ConsoleKey.UpArrow:
                        if (map[userY - 1, userX] != '#')
                            userY--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (map[userY + 1, userX] != '#')
                            userY++;
                        break;
                }
                    
                if (map[userY, userX] == '%')
                {
                    map[userY, userX] = ' ';
                    HP -= 20;
                }

                if (map[userY, userX] == '0')
                {
                    map[userY, userX] = ' ';

                    sum += 1;

                    if (sum == 10)
                    {
                        Console.Clear();
                        Console.SetCursorPosition(0, 0);
                        Console.WriteLine("Победа! Вы собрали все монеты.");
                        Console.ReadKey();
                        break;
                    }
                }
                else if (HP <= 0)
                {
                    Console.Clear();
                    Console.SetCursorPosition(0, 0);
                    Console.WriteLine("Вы проиграли(");
                    Console.ReadKey();
                    break;
                }
            }
        }
    }
}
