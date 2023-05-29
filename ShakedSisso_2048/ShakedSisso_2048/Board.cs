using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShakedSisso_2048
{
    class Board
    {
        private int boardSize;
        private Random rnd;
        private int cellAddValue;
        public Tiles[,] gameBoard;
        
        public Board()
        {
            this.boardSize = 4;
            this.rnd = new Random();
            this.cellAddValue = 0;
            this.gameBoard = new Tiles[this.boardSize, this.boardSize];
            ResetBoard();
        }
        public int GetBoardSize()
        {
            return this.boardSize;
        }
        public void ResetBoard()
        {
            this.cellAddValue = 0;
            for (int i = 0; i < this.boardSize; i++)
            {
                for (int j = 0; j < this.boardSize; j++)
                {
                    this.gameBoard[i, j] = new Tiles();
                }
            }
            NewTile();
            NewTile();
        }
        public void NewTile()
        {
            int r, c, value;
            bool invalid = true;
            while (invalid)
            {
                r = this.rnd.Next(this.boardSize);
                c = this.rnd.Next(this.boardSize);
                if (this.gameBoard[r, c].IsZeroValue())
                {
                    if (this.rnd.Next(10) < 8)
                        value = 2;
                    else
                        value = 4;
                    this.gameBoard[r, c].SetValue(value);
                    invalid = false;
                }
            }
        }
        public int FindBiggestTile()
        {
            int max = 0;
            for (int i = 0; i < this.boardSize; i++)
            {
                for (int j = 0; j < this.boardSize; j++)
                {
                    if (this.gameBoard[i, j].GetValue() > max)
                        max = this.gameBoard[i, j].GetValue();
                }
            }
            return max;
        }
        public bool IsGameOver()
        {
            return IsBoardFull() && !IsMovePossible();
        }
        private bool IsBoardFull()
        {
            for (int r = 0; r < this.boardSize; r++)
            {
                for (int c = 0; c < this.boardSize; c++)
                {
                    if (this.gameBoard[r, c].IsZeroValue())
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        private bool IsMovePossible()
        {
            return CheckHorizontalMovePossible() || CheckVerticalMovePossible();
        }
        private bool CheckVerticalMovePossible()
        {
            for (int r = 0; r < this.boardSize; r++)
            {
                for (int c = 0; c < (this.boardSize - 1); c++)
                {
                    if (this.gameBoard[r, c].GetValue() == this.gameBoard[r, c + 1].GetValue())
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private bool CheckHorizontalMovePossible()
        {
            for (int c = 0; c < this.boardSize; c++)
            {
                for (int r = 0; r < (this.boardSize - 1); r++)
                {
                    if (this.gameBoard[r, c].GetValue() == this.gameBoard[r + 1, c].GetValue())
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public int GetScoreValue()
        {
            return this.cellAddValue;
        }
        private bool Combine(int x1, int y1, int x2, int y2, bool isOccupied)
        {
            if (!this.gameBoard[x1, y1].IsZeroValue())
            {
                int value1 = this.gameBoard[x1, y1].GetValue();
                int value2 = this.gameBoard[x2, y2].GetValue();
                if (value1 == value2)
                {
                    this.gameBoard[x2, y2].DoubleValue();
                    this.gameBoard[x1, y1].SetZeroValue();
                    this.cellAddValue += (value1 + value2);
                    isOccupied = true;
                }
            }
            return isOccupied;
        }
        private bool MoveTile(int x1, int y1, int x2, int y2)
        {
            bool isOccupied = false;
            if (!this.gameBoard[x1, y1].IsZeroValue() && this.gameBoard[x2, y2].IsZeroValue())
            {
                int value = this.gameBoard[x1, y1].GetValue();
                this.gameBoard[x2, y2].SetValue(value);
                this.gameBoard[x1, y1].SetValue(0);
                isOccupied = true;
            }
            return isOccupied;
        }
        public bool MoveUp()
        {
            bool isOccupied = MoveUpLoop();
            for (int r = 0; r < this.boardSize; r++)
            {
                for (int c = 0; c < (this.boardSize - 1); c++)
                {
                    isOccupied = Combine(r, c+1, r, c, isOccupied);
                }
            }
            if (MoveUpLoop())
                isOccupied = true;
            return isOccupied;
        }
        private bool MoveUpLoop()
        {
            bool isOccupied = false, columnOccupied = false;
            for (int r = 0; r < this.boardSize; r++)
            {
                do
                {
                    columnOccupied = false;
                    for (int c = 0; c < (this.boardSize - 1); c++)
                    {
                        if (MoveTile(r, c+1, r, c))
                        {
                            columnOccupied = true;
                            isOccupied = true;
                        }
                    }
                } while (columnOccupied);
            }
            return isOccupied;
        }
        public bool MoveDown()
        {
            bool isOccupied = MoveDownLoop();
            for (int r = 0; r < this.boardSize; r++)
            {
                for (int c = this.boardSize - 1; c > 0; c--)
                {
                    isOccupied = Combine(r, c - 1, r, c, isOccupied);
                }
            }
            if (MoveDownLoop()) 
                isOccupied = true;
            return isOccupied;
        }
        private bool MoveDownLoop()
        {
            bool isOccupied = false, columnisOccupied = false;
            for (int r = 0; r < this.boardSize; r++)
            {
                do
                {
                    columnisOccupied = false;
                    for (int c = this.boardSize - 1; c > 0; c--)
                    {
                        if (MoveTile(r, c - 1, r, c))
                        {
                            columnisOccupied = true;
                            isOccupied = true;
                        }
                    }
                } while (columnisOccupied);
            }
            return isOccupied;
        }
        public bool MoveLeft()
        {
            bool isOccupied = MoveLeftLoop();
            for (int c = 0; c < this.boardSize; c++)
            {
                for (int r = 0; r < (this.boardSize - 1); r++)
                {
                    isOccupied = Combine(r + 1, c, r, c, isOccupied);
                }
            }
            if (MoveLeftLoop())
                isOccupied = true;
            return isOccupied;
        }
        private bool MoveLeftLoop()
        {
            bool isOccupied = false, rowisOccupied = false;
            for (int c = 0; c < this.boardSize; c++)
            {
                do
                {
                    rowisOccupied = false;
                    for (int r = 0; r < (this.boardSize - 1); r++)
                    {
                        if (MoveTile(r+1, c, r, c))
                        {
                            rowisOccupied = true;
                            isOccupied = true;
                        }
                    }
                } while (rowisOccupied);
            }
            return isOccupied;
        }
        public bool MoveRight()
        {
            bool isOccupied = MoveRightLoop();
            for (int c = 0; c < this.boardSize; c++)
            {
                for (int r = (this.boardSize - 1); r > 0; r--)
                {
                    isOccupied = Combine(r-1, c, r, c, isOccupied);
                }
            }
            if (MoveRightLoop())
                isOccupied = true;
            return isOccupied;
        }
        private bool MoveRightLoop()
        {
            bool isOccupied = false, rowisOccupied = true;
            for (int c = 0; c < this.boardSize; c++)
            {
                do
                {
                    rowisOccupied = false;
                    for (int r = (this.boardSize - 1); r > 0; r--)
                    {
                        if (MoveTile(r - 1, c, r, c))
                        {
                            rowisOccupied = true;
                            isOccupied = true;
                        }
                    }
                } while (rowisOccupied);
            }
            return isOccupied;
        }
    }
}
