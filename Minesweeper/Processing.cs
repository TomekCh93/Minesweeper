using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Minesweeper
{
    static class Processing
    {

        public static (int, int) GetUserInput()
        {
            Console.WriteLine("Enter a row and column separated by a space.");
            var input = Console.ReadLine().Split(' ');
            while (input.Length != 2)
            {
                Console.WriteLine("Wrong values.");
                Thread.Sleep(1000);
                input = Console.ReadLine().Split(' ');
            }
            var row = -1;
            var col = -1;
            return int.TryParse(input[0], out row) && int.TryParse(input[1], out col) == true ? (row, col) : (-1, -1);
        }
        public static (int, int) GetGameSettings()
        {
            Console.WriteLine("Welcome to Minesweeper game!");
            Console.WriteLine("Enter the size of game grid.");

            int gridSizeInp = inputEvaluate();

            Console.WriteLine("Select difficulty level. (1 - 100)");

            int diffInp = inputEvaluate();

            return (gridSizeInp, diffInp);


        }

        private static int inputEvaluate()
        {
            int userInput = -1;
            bool check = int.TryParse(Console.ReadLine().Trim(), out userInput);
            while (true)
            {
                if (userInput > 1 && userInput < 100)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("You selected wrong value!");
                    check = int.TryParse(Console.ReadLine().Trim(), out userInput);
                }
            }

            return userInput;
        }

        public static void Render(Board board, bool gameEnd)
        {
            Console.Clear();
            for (int i = 0; i <= board.Size; i++)
            {

                if (i == 0)
                {
                    Console.Write("   |");
                }
                else if (i <= 10)
                {
                    Console.Write($" {i - 1} |");
                }
                else
                {
                    Console.Write($"{i - 1} |");
                }

            }
            Console.Write("\n");
            for (int i = 0; i < board.Size * 4.5; i++)
            {
                Console.Write("¯");
            }
            Console.Write("\n");
            for (int i = 0; i < board.Size; i++)
            {

                if (i < 10)
                {
                    Console.Write($"{i}: | ");
                }
                else
                {
                    Console.Write($"{i}:| ");
                }

                for (int j = 0; j < board.Size; j++)
                {
                    if (board.ReturnCell(i, j).Revealed == true || gameEnd)
                    {
                        if (board.ReturnCell(i, j).IsArmed == true)
                        {
                            Console.BackgroundColor = ConsoleColor.Red;
                            Console.Write("!");
                            Console.BackgroundColor = ConsoleColor.Black;
                        }
                        else if (board.ReturnCell(i, j).ArmedNeighbors == 0)
                        {
                            Console.Write(' ');
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            Console.Write(board.ReturnCell(i, j).ArmedNeighbors);
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                    }
                    else
                    {
                        Console.Write("X");
                    }
                    Console.Write(" | ");
                }

                Console.WriteLine();
            }
        }

    }
}
