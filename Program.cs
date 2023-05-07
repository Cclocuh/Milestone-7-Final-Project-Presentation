using MilestoneConsoleApp;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MilestoneConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Create a new instance of the board
            Board board = new Board(8, 5);
            board.Difficulty = 32;

            // Setup the live neighbors on the board
            board.SetupLiveNeighbors();

            //board.CalculateLiveNeighbors();

            PrintBoard(board);

            // Keep playing until the game is over 
            bool gameOver = false;
            
            while (!gameOver)
            {
                // Print the current state of the board 
                PrintBoardDuringGame(board);

                // Ask the user for a row column 
                Console.Write("Enter a row number: ");
                int row = int.Parse(Console.ReadLine());

                Console.Write("Enter a column number: ");
                int col = int.Parse(Console.ReadLine());

                // Get the cell at the chosen row and column
                Cell cell = board.BoardGrid[row, col];

                // if the cell is a live bomb, game over 
                if (cell.live)
                {
                    Console.WriteLine("You hit a bomb!Game over.");
                    gameOver = true;
                }
                else
                {
                    // Mark the cell as visited
                    cell.visited = true;

                    // Check if all non-bomb cells have been visited 
                    int nonBombCells = board.boardSize * board.boardSize - board.Difficulty;
                    int visitedNonBombCells = 0;

                    foreach (Cell c in board.BoardGrid)
                    {
                        if (!c.live && c.visited)
                        {
                            visitedNonBombCells++;
                        }
                    }

                    if (visitedNonBombCells == nonBombCells)
                    {
                        Console.WriteLine("Cheers, you found all the non-bomb cells!");
                        gameOver &= true;
                    }

                    // Print the boardgrid
                    PrintBoard(board);
                }
            }
        }

        static public void PrintBoard(Board board)
        {
            for (int r = 0; r < board.Boardsize; r++)
            {
                for (int c = 0; c < board.boardSize; c++)
                {
                    
                    if (board.BoardGrid[r, c].visited)
                    {
                        
                        Console.Write(board.BoardGrid[r, c].LiveNeighbors + "");
                    }
                    else
                    {
                        Console.Write("-");
                    }
                }
            }
        }
        
        static void PrintBoardDuringGame(Board board)
        {
            for (int r = 0; r < board.Boardsize; r++)
            {
                for (int c = 0;c < board.Boardsize; c++)
                {
                    Cell cell = board.BoardGrid[r, c];

                    if (!cell.visited)
                    {
                        Console.Write("?");
                    }
                    else if (cell.live)
                    {
                        Console.Write("*");
                    }
                    else if (cell.liveNeighbors == 0)
                    {
                        Console.Write("-");
                    }
                    else
                    {
                        Console.Write(cell.liveNeighbors);
                    }

                    Console.Write(" ");
                }
                
                Console.WriteLine();
            }
        }
    }
}
