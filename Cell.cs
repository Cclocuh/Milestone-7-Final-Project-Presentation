using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilestoneConsoleApp
{
    public class Cell
    {
        public int row = -1;
        public int col = -1;
        public bool visited = false;
        public bool live = false;
        public int liveNeighbors = 0;
        public bool isBomb = false;
        internal int NumLiveNeighbors;

        public Cell()
        {

        }

        public Cell(bool v)
        {
            V = v;
        }

        public Cell(int row, int col)
        {
            this.row = row;
            this.col = col;
        }

        public int Row
        {
            get { return row; }
            set { this.row = value; }
        }

        public int Col
        {
            get { return col; }
            set { this.col = value; }
        }

        public bool IsVisited
        {
            get { return visited; }
            set { this.visited = value; }
        }

        public bool IsLive
        {
            get { return live; }
            set { this.live = value; }
        }

        public int LiveNeighbors
        {
            get { return liveNeighbors; }
            set { liveNeighbors = value; }
        }

        public bool IsBomb
        {
            get
            {
                return isBomb;
            }
            set
            {
                isBomb = value;
            }
        }
        public bool IsFlagged { get; set; }
        public bool IsRevealed { get; set; }
        public bool IsMine { get; set; }
        public bool V { get; }

    }
}

