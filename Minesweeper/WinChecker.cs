using System;
using System.Collections.Generic;
using System.Text;

namespace Minesweeper
{
    class WinChecker
    {
        public bool GameEnd { get; set; }
        public WinChecker(Board board)
        {
            GameEnd = false;
        }

        public bool ItsABomb(int row, int col, Board board)
        {
            if (board.ReturnCell(row, col).IsArmed == true)
            {
                GameEnd = true;
                return true;
            }

            return false;
        }
    }
}