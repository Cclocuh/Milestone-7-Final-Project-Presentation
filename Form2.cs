using Milestone_WFA1.Properties;
using MilestoneConsoleApp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Milestone_WFA1
{
    public partial class Form2 : Form
    {
        Board board = new Board(10, 5);
        private Board tableLayoutPanel;
        private Button button;
        private int timeElapsed = 0;
        public static Stopwatch Stopwatch = new Stopwatch();
        private int numMines;
        

        public Form2()
        {
            InitializeComponent();
            InitializeGrid();
            tableLayoutPanel = new Board(10, 5);
            tableLayoutPanel.SetupLiveNeighbors();
            tableLayoutPanel.CalculateLiveNeighbors();
            AssociateClickEvent();
            
            
            // Load the high score from the settings
            int highScore = Properties.Settings.HighScores;

            // Display the high score
            highScorelabel.Text = "High Score: " + highScore.ToString();

        }

        private void DisplayBoard()
        {
            // Clear any existing buttons
            Controls.Clear();
            

            // Create a button for each cell in the grid
            for (int row = 0; row < board.boardSize;  row++)
            {
                for (int col = 0; col < board.boardSize; col++)
                {
                    Cell cell = board.BoardGrid[row, col];
                    Button button = new Button();
                    button.Size = new Size(30, 30);
                    button.Location = new Point(row * 30, col * 30);
                    button.Tag = cell;
                    button.Click += Button_Click;

                    if (!cell.visited)
                    {
                        button.Text = "";
                        button.BackColor = Color.LightGray;
                    }
                    else if (cell.live) 
                    {
                        button.Text = "";
                        button.BackColor = Color.LightGreen;
                    }
                    else
                    {
                        button.Text = "";
                        button.BackColor = Color.LightSkyBlue;
                    }

                    Controls.Add(button);
                    
                }
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {

            Button clickedButton = sender as Button;
            Cell clickedCell = (Cell)clickedButton.Tag;


            if (clickedCell.isBomb)
            {
                // Handle bomb click
                clickedButton.BackgroundImage = Properties.Resources.Bomb;
                MessageBox.Show("Game Over");
                RevealAllCells();
                return;
            }
            else if (clickedCell.IsFlagged)
            {
                // Do nothing if cell is flagged
                return;
            }
            else
            {
               
                // Reveal cell
                clickedCell.IsRevealed = true;

                // Check for win
                if (board.CheckWin())
                {
                    // Reveal all cells and display "You Win" message
                    MessageBox.Show("You Win");
                }

            }

        }

        private void RevealAllCells()
        {
           //this.board.setMines();
        }

        private void floodFill(int row, int col)
        {
            Cell cell = board.BoardGrid[row, col];
            if (cell.liveNeighbors == 0 && !cell.visited)
            {
                cell.visited = true;
                floodFill(row - 1, col - 1);
                floodFill(row - 1, col);
                floodFill(row - 1, col + 1);
                floodFill(row, col - 1);
                floodFill(row, col + 1);
                floodFill(row + 1, col - 1);
                floodFill(row + 1, col);
                floodFill(row + 1, col + 1);
            }
            else if (!cell.live && !cell.visited)
            {
                cell.visited = true; 
            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            this.FormClosed += MainPage_FormClosed;
        }

        // Create Buttons for all cells in the grid
        private void MainPage_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void InitializeGrid()
        {
            for (int i = 0; i < tableLayoutPanel1.ColumnCount; i++)
            {
                for (int j = 0; j < tableLayoutPanel1.RowCount; j++) 
                {
                    button = new Button();
                    button.Visible = true;
                    button.Dock = DockStyle.Fill;

                    tableLayoutPanel1.Controls.Add(button, i, j);
                }
            }
        }

        // Associate a Click event for all the buttons in the grid
        private void AssociateClickEvent()
        {
            foreach (Control c  in tableLayoutPanel1.Controls.OfType<Button>()) 
            {
                c.Click += new EventHandler(Button_Click);
            }
        }

        // Handle the Click event
        private void OnClick(object sender, EventArgs e)
        {
            Button button = sender as Button;
            Button clickedButton = sender as Button;
            button.Visible = true;
            button.BackColor = Color.Yellow;
           
        }


        private void startButton_Click(object sender, EventArgs e)
        {
            Stopwatch.Start();
            timer1.Start();
            
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            Stopwatch.Stop();
            timer1.Stop();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timeTickerLabel.Text = Stopwatch.Elapsed.ToString(@"hh\:mm\:ss");
        }
    }
}
