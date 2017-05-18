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
        private bool jumped;

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

        public bool isJumped()
        {
            return jumped;
        }

        public void setRedCount(int value)
        {
            redCount = value;
        }

        public void setBlackCount(int value)
        {
            blackCount = value;
        }

        public void setRedLadyCount(int value)
        {
            redLadyCount = value;
        }

        public void setBlackLadyCount(int value)
        {
            blackLadyCount = value;
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

        public int getRedWeightedScore()
        {
            return 5 * redCount + 50 * redLadyCount;
        }

        public int getBlackWeightedScore()
        {
            return 5 * blackCount + 50 * blackLadyCount;
        }

        public List<Move> getJumps(string player)
        {
            List<Move> jumps = new List<Move>();
            for(int row = 0; row < 8; row++)
            {
                for(int col = 0; col < 8; col++)
                {
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
                }
            }
            
            return jumps;
        }

        public List<Move> getJumpsFromLocation(string player, int row, int col)
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

        public List<Move> getAllLegalMovesForColor(string colour)
        {
            List<Move> moves = new List<Move>();
            int count = 0;

            // Loop through board and get moves at each location
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    FieldState currPosition = GetPiece(row, col);
                    if (currPosition.ToString().ToLower().Equals(colour))
                    {
                        moves.AddRange(getLegalMovesForPlayerAtPosition(colour, row, col));
                        count++;
                    }

                    // Get king moves
                    if (colour.Equals("red") && currPosition == FieldState.RED_LADY)
                    {
                        moves.AddRange(getLegalMovesForPlayerAtPosition(colour, row, col));
                        count++;
                    }
                    else if (colour.Equals("black") && currPosition == FieldState.BLACK_LADY)
                    {
                        moves.AddRange(getLegalMovesForPlayerAtPosition(colour, row, col));
                        count++;
                    }

                    // Stop if all the pieces of the color have been found
                    if (count == 12)
                    {
                        return moves;
                    }
                }
            }
            return moves;
        }

        public List<Move> getLegalMovesForPlayerAtPosition(string player, int row, int col)
        {
            FieldState chosenPiece = GetPiece(row, col);
            List<Move> moves = new List<Move>();

            // Get red moves
            if (player.Equals("red"))
            {
                if (chosenPiece == FieldState.RED || chosenPiece == FieldState.RED_LADY)
                {
                    if (GetPiece(row + 1, col + 1) == FieldState.EMPTY)
                        moves.Add(new Move(row, col, row + 1, col + 1));
                    if (GetPiece(row + 1, col - 1) == FieldState.EMPTY)
                        moves.Add(new Move(row, col, row + 1, col - 1));

                }
                if (chosenPiece == FieldState.RED_LADY)
                {
                    if (GetPiece(row - 1, col + 1) == FieldState.EMPTY)
                        moves.Add(new Move(row, col, row - 1, col + 1));
                    if (GetPiece(row - 1, col - 1) == FieldState.EMPTY)
                        moves.Add(new Move(row, col, row - 1, col - 1));

                }
            }
            else if (player.Equals("black"))
            { // Get black moves
                if (chosenPiece == FieldState.BLACK || chosenPiece == FieldState.BLACK_LADY)
                {
                    if (GetPiece(row - 1, col + 1) == FieldState.EMPTY)
                        moves.Add(new Move(row, col, row - 1, col + 1));
                    if (GetPiece(row - 1, col - 1) == FieldState.EMPTY)
                        moves.Add(new Move(row, col, row - 1, col - 1));
                }
                if (chosenPiece == FieldState.BLACK_LADY)
                {
                    if (GetPiece(row + 1, col + 1) == FieldState.EMPTY)
                        moves.Add(new Move(row, col, row + 1, col + 1));
                    if (GetPiece(row + 1, col - 1) == FieldState.EMPTY)
                        moves.Add(new Move(row, col, row + 1, col - 1));
                }
            }

            // Add jumps
            List<Move> jumps = getJumpsFromLocation(player,row,col);
            moves.AddRange(jumps);
            return moves;
        }

        public bool makeMove(Move move, string player)
        {
            FieldState tmp = GetPiece(move.CurrentRow, move.CurrentCol);
            AssignPiece(move.CurrentRow, move.CurrentCol, FieldState.EMPTY);

            if (player.Equals("red") && move.RowToBeMovedTo == 7)
            {
                AssignPiece(move.RowToBeMovedTo,move.ColToBeMovedTo, FieldState.RED_LADY);
                increaseRedLadyCount();
                return true;
            }
            else if (player.Equals("black") && move.RowToBeMovedTo == 0)
            {
                AssignPiece(move.RowToBeMovedTo, move.ColToBeMovedTo, FieldState.BLACK_LADY);
                increaseBlackLadyCount();
                return true;
            }
            else
            {
                AssignPiece(move.RowToBeMovedTo, move.ColToBeMovedTo, tmp);
                return false;
            }
        }

        public void handleJump(Move move, string player)
        {
            Tuple<int, int> spaceSkipped = move.getSpaceInBetween();

            // Verifies that jump was made
            if (spaceSkipped.Item1 != move.CurrentRow && spaceSkipped.Item1 != move.RowToBeMovedTo &&
                    spaceSkipped.Item2 != move.CurrentCol && spaceSkipped.Item2 != move.ColToBeMovedTo)
            {
                if (GetPiece(spaceSkipped.Item1,spaceSkipped.Item2) == FieldState.RED_LADY)
                {
                    redLadyCount -= 1;
                }
                if (GetPiece(spaceSkipped.Item1, spaceSkipped.Item2) == FieldState.BLACK_LADY)
                {
                    blackLadyCount -= 1;
                }
                AssignPiece(spaceSkipped.Item1, spaceSkipped.Item2, FieldState.EMPTY);
                jumped = true;
                if (player.Equals("red"))
                {
                    blackCount -= 1;

                }
                else
                {
                    redCount -= 1;
                }
            }
            else
            {
                jumped = false;
            }

        }

        public int getFirstAreaScore(string colour)
        {
            return firstArea(colour);
        }

        public int getSecondAreaScore(string colour)
        {
            return secondArea(colour);
        }

        public int getThirdAreaScore(string colour)
        {
            return thirdArea(colour);
        }

        private int firstArea(string colour)
        {
            int score = 0;
            for(int i = 0; i < 8; i++)
            {
                if(colour.Equals("red"))
                {
                    FieldState piece = GetPiece(0, i);
                    if (piece == FieldState.RED || piece == FieldState.RED_LADY)
                        score += 15;
                    piece = GetPiece(7, i);
                    if (piece == FieldState.RED || piece == FieldState.RED_LADY)
                        score += 15;
                    piece = GetPiece(i, 0);
                    if (piece == FieldState.RED || piece == FieldState.RED_LADY)
                        score += 15;
                    piece = GetPiece(i, 7);
                    if (piece == FieldState.RED || piece == FieldState.RED_LADY)
                        score += 15;
                }
                else
                {
                    FieldState piece = GetPiece(0, i);
                    if (piece == FieldState.BLACK || piece == FieldState.BLACK_LADY)
                        score += 15;
                    piece = GetPiece(7, i);
                    if (piece == FieldState.BLACK || piece == FieldState.BLACK_LADY)
                        score += 15;
                    piece = GetPiece(i, 0);
                    if (piece == FieldState.BLACK || piece == FieldState.BLACK_LADY)
                        score += 15;
                    piece = GetPiece(i, 7);
                    if (piece == FieldState.BLACK || piece == FieldState.BLACK_LADY)
                        score += 15;
                }
            }

            return score;
        }

        private int secondArea(string colour)
        {
            int score = 0;
            for (int i = 1; i < 7; i++)
            {
                if (colour.Equals("red"))
                {
                    FieldState piece = GetPiece(1, i);
                    if (piece == FieldState.RED || piece == FieldState.RED_LADY)
                        score += 10;
                    piece = GetPiece(6, i);
                    if (piece == FieldState.RED || piece == FieldState.RED_LADY)
                        score += 10;
                    piece = GetPiece(i, 1);
                    if (piece == FieldState.RED || piece == FieldState.RED_LADY)
                        score += 10;
                    piece = GetPiece(i, 6);
                    if (piece == FieldState.RED || piece == FieldState.RED_LADY)
                        score += 10;
                }
                else
                {
                    FieldState piece = GetPiece(1, i);
                    if (piece == FieldState.BLACK || piece == FieldState.BLACK_LADY)
                        score += 10;
                    piece = GetPiece(6, i);
                    if (piece == FieldState.BLACK || piece == FieldState.BLACK_LADY)
                        score += 10;
                    piece = GetPiece(i, 1);
                    if (piece == FieldState.BLACK || piece == FieldState.BLACK_LADY)
                        score += 10;
                    piece = GetPiece(i, 6);
                    if (piece == FieldState.BLACK || piece == FieldState.BLACK_LADY)
                        score += 10;
                }
            }

            return score;
        }

        private int thirdArea(string colour)
        {
            int score = 0;
            for (int i = 2; i < 6; i++)
            {
                if (colour.Equals("red"))
                {
                    FieldState piece = GetPiece(2, i);
                    if (piece == FieldState.RED || piece == FieldState.RED_LADY)
                        score += 5;
                    piece = GetPiece(5, i);
                    if (piece == FieldState.RED || piece == FieldState.RED_LADY)
                        score += 5;
                    piece = GetPiece(i, 2);
                    if (piece == FieldState.RED || piece == FieldState.RED_LADY)
                        score += 5;
                    piece = GetPiece(i, 5);
                    if (piece == FieldState.RED || piece == FieldState.RED_LADY)
                        score += 5;
                }
                else
                {
                    FieldState piece = GetPiece(2, i);
                    if (piece == FieldState.BLACK || piece == FieldState.BLACK_LADY)
                        score += 5;
                    piece = GetPiece(5, i);
                    if (piece == FieldState.BLACK || piece == FieldState.BLACK_LADY)
                        score += 5;
                    piece = GetPiece(i, 2);
                    if (piece == FieldState.BLACK || piece == FieldState.BLACK_LADY)
                        score += 5;
                    piece = GetPiece(i, 5);
                    if (piece == FieldState.BLACK || piece == FieldState.BLACK_LADY)
                        score += 5;
                }
            }

            return score;
        }
    }
}
