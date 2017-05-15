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
            RED_LADY,
            BLACK_LADY
        }
        private FieldState[,] board = new FieldState[8,8];
        private int redCount;
        private int blackCount;
        private int blackLadyCount;
        private int redLadyCount;

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

        public int getRedCount()
        {
            return redCount;
        }

        public void increaseRedCount(int i)
        {
            redCount += i;
        }

        public int getBlackCount()
        {
            return blackCount;
        }

        public void increaseBlackCount(int i)
        {
            blackCount += i;
        }

        public int getRedLadyCount()
        {
            return redLadyCount;
        }

        public void increaseRedLadyCount(int i)
        {
            redLadyCount += i;
        }

        public int getBlackLadyCount()
        {
            return blackLadyCount;
        }

        public void increaseBlackLadyCount(int i)
        {
            blackLadyCount += i;
        }
    }
}
