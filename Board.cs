using SharpDX.Direct3D11;
using System;
using System.Collections.Generic;
using MilestoneConsoleApp;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using SharpDX.DXGI;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace MilestoneConsoleApp
{
    public class Board
    {
        public int boardSize;
        private Cell[,] boardGrid;
        public int difficulty;
        //private int bombCount;
        private int numMines;

        public Board(int boardSize, int numMines)
        {
            this.boardSize = boardSize;
            this.boardGrid = new Cell[boardSize, boardSize];
            this.numMines = numMines;

            for (int r = 0; r < boardSize; r++)
            {
                for (int c = 0; c < boardSize; c++)
                {
                    boardGrid[r, c] = new Cell(r, c);
                }
            }
            this.setMines();
            this.difficulty = 10;
        }

        public void setMines()
        {
            Random mineGenerator = new Random();
            

            for (int i = 0; i < numMines; i++)
            {
                int mineIndex = mineGenerator.Next(0, (boardSize * boardSize) - 1);
                Console.WriteLine("mine index", mineIndex);
                int row = mineIndex / boardSize; 
                int col = mineIndex % boardSize;

                Cell myCell = boardGrid[row, col];
                myCell.isBomb = true;  
            }
           
        }

        public void setFlaggedCell(int r, int c)
        {
            // Check if the cell is within the bounds of the board
            if (r >= 0 && r < boardSize && c >= 0 && c < boardSize) 
            {
                // Toggle the flagged status of the cell
                if (!boardGrid[r, c].IsRevealed)
                {
                    boardGrid[r, c].IsFlagged = !boardGrid[r, c].IsFlagged;
                } 
            }

            // Randomly set some other cells as flagged for testing purposes
            Random rand = new Random();
            int numFlagged = 0;
            while (numFlagged < 5)
            {
                int randRow = rand.Next(0, boardSize);
                int randCol = rand.Next(0, boardSize);
                if (!boardGrid[randRow, randCol].IsRevealed && !boardGrid[randRow, randRow].IsBomb && !boardGrid[randRow, randCol].IsFlagged)
                {
                    boardGrid[randRow, randCol].IsFlagged = true;
                    numFlagged++;
                }
            }
        }

        public int Boardsize
        {  
            get { return boardSize; } 
            set { boardSize = value; }
        }

        public Cell[,] BoardGrid
        {
            get { return boardGrid; }
            set { boardGrid = value; }
        }

        public int Difficulty
        {
            get { return difficulty; } 
            set { difficulty = value; } 
        }

        public void SetupLiveNeighbors()
        {
            Random random = new Random();
            int liveCount = (int)Math.Round((double)(boardSize * boardSize * (difficulty / 100)));

            while (liveCount > 0)
            {
                int r = random.Next(0, boardSize);
                int c = random.Next(0, boardSize);
                if (!boardGrid[r, c].live)
                {
                    boardGrid[r, c].live = true;
                    liveCount--;
                }
            }
        }

        public void CalculateLiveNeighbors()
        {
            for (int row = 0; row < boardSize; row++)
            {
                for (int col = 0; col < boardSize; col++)
                {
                    if (!boardGrid[row, col].live)
                    {
                        int liveNeighbors = 0;

                        for (int i = -1; i <= 1; i++)
                        {
                            for (int j = -1; j <= 1; j++)
                            {
                                int r = row + i;
                                int c = col + j;

                                if (r >= 0 && r < boardSize && c >= 0 && c < boardSize && boardGrid[r, c].live)
                                {
                                    liveNeighbors++;
                                }
                            }
                        }

                        boardGrid[row, col].NumLiveNeighbors = liveNeighbors;
                    }
                    else
                    {
                        boardGrid[row, col].NumLiveNeighbors = 10;
                    }
                }
            }

            

        }

        public void floodFill(int row, int col)
        {
            // Check if the current cell is valid and has no live neighbors
            if (row >= 0 && row < boardSize && col >= 0 && col < boardSize && !boardGrid[row, col].visited && boardGrid[row, col].LiveNeighbors == 0)
            {
                // Mark the cell as visited
                boardGrid[row, col].visited = true;

                // Recursively call floodFill on surrounding cells
                floodFill(row - 1, col); // up
                floodFill(row + 1, col); // down
                floodFill(row, col - 1); // left
                floodFill(row, col + 1); // right
            }
        }

        public bool CheckWin()
        {
            foreach (Cell cell in boardGrid)
            {
                if (!cell.IsRevealed && !cell.IsBomb)
                {
                    // If there is any unrevealed non-bomb cell, the game is not won
                    return false;
                }
                else if (!cell.IsRevealed && cell.IsBomb)
                {
                    // If there is any unrevealed bomb cell, the game is not won
                    return false;
                }
            }
            // If we get here, all cells have been revealed or are bombs
            return true;
        }
    }
}

   

      
        
        
       

