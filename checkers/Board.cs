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
        public enum FieldState
        {
            INVALID = -1,
            EMPTY = 0,
            WHITE = 1,
            BLACK = 2,
            WHITE_KING = 3,
            BLACK_KING = 4
        }
        private FieldState[,] board = new FieldState[8,8];

        public Board()
        {
            for(int row = 0; row < 8; row++)
            {
                for(int col = 0; col < 8; col++)
                {
                    board[row,col] = FieldState.EMPTY;
                }
            }
        }

        public void AssignPiece(int row, int col, FieldState piece)
        {
            board[row, col] = piece;
        }

        public FieldState GetPiece(int row, int col)
        {
            //return -1 if out of board
            if ((row > 7) || (row < 0) || (col > 7) || (col < 0))
                return FieldState.INVALID;

            return board[row, col];
        }
    }
}
