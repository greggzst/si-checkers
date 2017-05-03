using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace checkers
{
    class Board
    {
        public enum FieldState
        {
            INVALID,
            EMPTY,
            RED,
            BLACK,
            RED_KING,
            BLACK_KING
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
