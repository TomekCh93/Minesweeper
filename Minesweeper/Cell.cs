using System;
using System.Collections.Generic;
using System.Text;

namespace Minesweeper
{
    internal class Cell
    {
        public bool Revealed { get; set; }

        public bool IsArmed { get; set; }
        public int ArmedNeighbors { get; set; }  // 0 to 8 armed cells in the neighborhood
        public Cell()
        {
            Revealed = false;
            IsArmed = false;
            ArmedNeighbors = 0;
        }
    }
}
