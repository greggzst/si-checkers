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

        public void increaseRedCount()
        {
            redCount++;
        }

        public int getBlackCount()
        {
            return blackCount;
        }

        public void increaseBlackCount()
        {
            blackCount++;
        }

        public int getRedLadyCount()
        {
            return redLadyCount;
        }

        public void increaseRedLadyCount()
        {
            redLadyCount++;
        }

        public int getBlackLadyCount()
        {
            return blackLadyCount;
        }

        public void increaseBlackLadyCount()
        {
            blackLadyCount++;
        }

        public List<Move> getJumps(string player, int row, int col)
        {
            List<Move> jumps = new List<Move>();
            FieldState chosenPiece = GetPiece(row, col);

            // Get red jumps
            if (player.Equals("red"))
            {
                if (chosenPiece == FieldState.RED || chosenPiece == FieldState.RED_LADY)
                {
                    if (GetPiece(row + 1, col + 1) == FieldState.BLACK ||
                            GetPiece(row + 1, col + 1) == FieldState.BLACK_LADY)
                    {
                        if (GetPiece(row + 2, col + 2) == FieldState.EMPTY)
                        {
                            jumps.Add(new Move(row, col, row + 2, col + 2));
                        }
                    }
                    if (GetPiece(row + 1, col - 1) == FieldState.BLACK ||
                            GetPiece(row + 1, col - 1) == FieldState.BLACK_LADY)
                    {
                        if (GetPiece(row + 2, col - 2) == FieldState.EMPTY)
                        {
                            jumps.Add(new Move(row, col, row + 2, col - 2));
                        }
                    }
                }

                // Get lady jumps - lady can jump backward
                if (chosenPiece == FieldState.RED_LADY)
                {
                    if (GetPiece(row - 1, col + 1) == FieldState.BLACK ||
                            GetPiece(row - 1, col + 1) == FieldState.BLACK_LADY)
                    {
                        if (GetPiece(row - 2, col + 2) == FieldState.EMPTY)
                        {
                            jumps.Add(new Move(row, col, row - 2, col + 2));
                        }
                    }
                    if (GetPiece(row - 1, col - 1) == FieldState.BLACK ||
                          GetPiece(row - 1, col - 1) == FieldState.BLACK_LADY)
                    {
                        if (GetPiece(row - 2, col - 2) == FieldState.EMPTY)
                        {
                            jumps.Add(new Move(row, col, row - 2, col - 2));
                        }
                    }
                }
            }
            else if (player.Equals("black"))
            { // Get black jumps
                if (chosenPiece == FieldState.BLACK || chosenPiece == FieldState.BLACK_LADY)
                {
                    if (GetPiece(row - 1, col + 1) == FieldState.RED ||
                            GetPiece(row - 1, col + 1) == FieldState.RED_LADY)
                    {
                        if (GetPiece(row - 2, col + 2) == FieldState.EMPTY)
                        {
                            jumps.Add(new Move(row, col, row - 2, col + 2));
                        }
                    }
                    if (GetPiece(row - 1, col - 1) == FieldState.RED ||
                            GetPiece(row - 1, col - 1) == FieldState.RED_LADY)
                    {
                        if (GetPiece(row - 2, col - 2) == FieldState.EMPTY)
                        {
                            jumps.Add(new Move(row, col, row - 2, col - 2));
                        }
                    }
                }

                // Get lady jumps - lady can jump backward
                if (chosenPiece == FieldState.BLACK_LADY)
                {
                    if (GetPiece(row + 1, col + 1) == FieldState.RED ||
                            GetPiece(row + 1, col + 1) == FieldState.RED_LADY)
                    {
                        if (GetPiece(row + 2, col + 2) == FieldState.EMPTY)
                        {
                            jumps.Add(new Move(row, col, row + 2, col + 2));
                        }
                    }
                    if (GetPiece(row + 1, col - 1) == FieldState.RED ||
                            GetPiece(row + 1, col - 1) == FieldState.RED_LADY)
                    {
                        if (GetPiece(row + 2, col - 2) == FieldState.EMPTY)
                        {
                            jumps.Add(new Move(row, col, row + 2, col - 2));
                        }
                    }
                }
            }
            return jumps;
        }
    }
}
