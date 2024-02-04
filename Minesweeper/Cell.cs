using System;
using System.Collections.Generic;
using System.Text;

namespace Minesweeper
{
    internal class Cell
    {
        public bool Revealed { get; set; }
        public bool IsArmed { get; set; }
        public int ArmedNeighbors { get; set; }
        public Cell()
        {
            Revealed = false;
            IsArmed = false;
            ArmedNeighbors = 0;
        }
    }
}