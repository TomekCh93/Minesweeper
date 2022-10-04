using System;
using System.Threading;

namespace Minesweeper
{
    class Program
    {
        static ConsoleKeyInfo endKey = Console.ReadKey();
        static void Main(string[] args)
        {

            do
            {
                (int, int) set = Processing.GetGameSettings();
                var board = new Board(set.Item1, set.Item2);
                var winChecker = new WinChecker(board);

                while (winChecker.GameEnd != true && board.Undiscovered != 0)
                {
                    Processing.Render(board, winChecker.GameEnd);
                    (int, int) userInput = Processing.GetUserInput();
                    if (board.IsWrongMove(userInput.Item1,userInput.Item2))
                    {
                        Console.WriteLine("Wrong move.");
                        Thread.Sleep(1000);
                        continue;
                    }
                    if (winChecker.ItsABomb(userInput.Item1,userInput.Item2,board))
                    {
                        break;
                    }
                    
                }
                Processing.Render(board, winChecker.GameEnd);
                if (winChecker.GameEnd == true || board.Undiscovered != 0)
                {
                    Console.WriteLine("You lose.");
                }
                else if (winChecker.GameEnd == false && board.Undiscovered == 0)
                {
                    Console.WriteLine("Congratulations. You win this game.");
                }

                Console.WriteLine("To exit press Escape, to play again press any key.");
                endKey = Console.ReadKey();
            } while (endKey.Key != ConsoleKey.Escape);

        }
      
    }
}
