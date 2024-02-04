using System;
using System.Collections.Generic;
using System.Text;


namespace Minesweeper
{
    /// <summary>
    /// Size - one side of a square board
    /// Diff - difficulty level from 1 to 100
    /// </summary>
    class Board
    {
        public int Size;
        private Cell[][] _grid;
        public int Undiscovered { get; private set; }
        public int NumOfBombs { get; private set; }
        public int AmoutOfFields { get; private set; }

        private double _difficulty;

        public double Difficulty
        {
            get
            {
                return _difficulty;
            }
            set
            {
                _difficulty = value <= 1 || value > 99 ? 1 : value;
            }
        }

        public Board(int size, int diff)
        {
            Difficulty = diff; //1 - 100 percentage of armed cells
            Size = size;
            _grid = new Cell[this.Size][];
            AmoutOfFields = (this.Size * this.Size);
            NumOfBombs = Convert.ToInt32((this.Difficulty / 100) * (this.AmoutOfFields));
            Undiscovered = this.AmoutOfFields - this.NumOfBombs;
            CellInitialization();
            CellArm();
            CalculateArmedNeighbors();
        }

        public bool IsWrongMove(int row, int col)
        {
            if (row < 0 || row >= this.Size || col < 0 || col >= this.Size || this._grid[row][col].Revealed == true)
            {
                return true;
            }


            if (_grid[row][col].IsArmed != true)
            {
                MarkEmptyFields(row, col);
            }
            return false;
        }

        private void MarkEmptyFields(int row, int col)
        {
            if (row < 0 || row >= this.Size || col < 0 || col >= this.Size || this._grid[row][col].Revealed == true)
            {
                return;
            }

            _grid[row][col].Revealed = true;
            Undiscovered--;
            if (_grid[row][col].ArmedNeighbors != 0)
            {
                return;
            }
            MarkEmptyFields(row + 1, col);
            MarkEmptyFields(row - 1, col);
            MarkEmptyFields(row, col + 1);
            MarkEmptyFields(row, col - 1);
            MarkEmptyFields(row + 1, col + 1);
            MarkEmptyFields(row - 1, col - 1);
            MarkEmptyFields(row - 1, col + 1);
            MarkEmptyFields(row - 1, col + 1);

        }

        public Cell ReturnCell(int row, int col)
        {
            return this._grid[row][col];
        }
        public int ReturnAmoutOfUndiscovered()
        {
            return this.Undiscovered;
        }
        private void CalculateArmedNeighbors()
        {
            for (int row = 0; row < this.Size; row++)

            {

                for (int col = 0; col < this.Size; col++)
                {
                    if (this._grid[row][col].IsArmed != true)
                    {
                        this._grid[row][col].ArmedNeighbors = GetSurroundingBombs(row, col);
                    }

                }
            }
        }

        private int GetSurroundingBombs(int row, int col)
        {
            var res = 0;
            for (int i = Math.Max(0, row - 1); i <= Math.Min(this.Size - 1, row + 1); i++)
            {
                for (int j = Math.Max(0, col - 1); j <= Math.Min(this.Size - 1, col + 1); j++)
                {
                    if ((i != row || j != col) && this._grid[i][j].IsArmed == true)
                    {
                        res++;
                    }

                }
            }
            return res;

        }

        private void CellArm()
        {
            var rnd = new Random();
            var plantedBombs = this.NumOfBombs;

            while (plantedBombs > 0)
            {
                var adr = rnd.Next(0, this.AmoutOfFields - 1);
                var row = adr / this.Size;
                var col = adr % this.Size;

                if (this._grid[row][col] == null || this._grid[row][col].IsArmed == true)
                {
                    continue;
                }
                else
                {
                    this._grid[row][col].IsArmed = true;
                    plantedBombs--;
                }
            }
        }

        private void CellInitialization()
        {
            for (int i = 0; i < this.Size; i++)
            {

                var tmp = new Cell[this.Size];
                this._grid[i] = tmp;
                for (int j = 0; j < this.Size; j++)
                {
                    this._grid[i][j] = new Cell();
                }
            }
        }
    }
}