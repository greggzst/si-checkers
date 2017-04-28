using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace checkers
{
    // 0 - empty field
    // 1 - white
    // 2 - black
    // 3 - white king
    // 4 - black king
    class Board
    {
        private int[,] board = new int[8,8];

        public Board()
        {
            for(int row = 0; row < 8; row++)
            {
                for(int col = 0; col < 8; col++)
                {
                    board[row,col] = 0;
                }
            }
        }

        public void AssignPiece(int row, int col, int piece)
        {
            board[row, col] = piece;
        }

        public int GetPiece(int row, int col)
        {
            return board[row, col];
        }
    }
}
