using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace checkers
{
    class Move
    {
        public int CurrentRow { get; set; }
        public int CurrentCol { get; set; }
        public int RowToBeMovedTo { get; set; }
        public int ColToBeMovedTo { get; set; }

        public Move()
        {
            CurrentRow = -1;
            CurrentCol = -1;
            RowToBeMovedTo = -1;
            ColToBeMovedTo = -1;
        }

        public Move(int cRow, int cCol, int mRow, int mCol)
        {
            CurrentRow = cRow;
            CurrentCol = cCol;
            RowToBeMovedTo = mRow;
            ColToBeMovedTo = mCol;
        }

        public bool isAdjacent(string color)
        {
            if (color.Equals("black"))
            {
                if ((CurrentRow - 1 == RowToBeMovedTo) && (CurrentCol - 1 == ColToBeMovedTo))
                    return true;
                if ((CurrentRow - 1 == RowToBeMovedTo) && (CurrentCol + 1 == ColToBeMovedTo))
                    return true;
            }
            if (color.Equals("red"))
            {
                if ((CurrentRow + 1 == RowToBeMovedTo) && (CurrentCol - 1 == ColToBeMovedTo))
                    return true;
                if ((CurrentRow + 1 == RowToBeMovedTo) && (CurrentCol + 1 == ColToBeMovedTo))
                    return true;
            }
            if (color.Equals("lady"))
            {
                if ((CurrentRow - 1 == RowToBeMovedTo) && (CurrentCol - 1 == ColToBeMovedTo))
                    return true;
                if ((CurrentRow - 1 == RowToBeMovedTo) && (CurrentCol + 1 == ColToBeMovedTo))
                    return true;
                if ((CurrentRow + 1 == RowToBeMovedTo) && (CurrentCol - 1 == ColToBeMovedTo))
                    return true;
                if ((CurrentRow + 1 == RowToBeMovedTo) && (CurrentCol + 1 == ColToBeMovedTo))
                    return true;
            }
            return false;
        }

        public Tuple<int,int> checkJump(string color)
        {
            if (color == "black")
            {
                if ((CurrentRow - 2 == RowToBeMovedTo) && (CurrentCol - 2 == ColToBeMovedTo))
                    return new Tuple<int, int>(CurrentRow - 1, CurrentCol - 1);
                if ((CurrentRow - 2 == RowToBeMovedTo) && (CurrentCol + 2 == ColToBeMovedTo))
                    return new Tuple<int, int>(CurrentRow - 1, CurrentCol + 1);
            }
            if (color == "red")
            {
                if ((CurrentRow + 2 == RowToBeMovedTo) && (CurrentCol - 2 == ColToBeMovedTo))
                    return new Tuple<int, int>(CurrentRow + 1, CurrentCol - 1);
                if ((CurrentRow + 2 == RowToBeMovedTo) && (CurrentCol + 2 == ColToBeMovedTo))
                    return new Tuple<int, int>(CurrentRow + 1, CurrentCol + 1);
            }
            if (color == "lady")
            {
                if ((CurrentRow - 2 == RowToBeMovedTo) && (CurrentCol - 2 == ColToBeMovedTo))
                    return new Tuple<int, int>(CurrentRow - 1, CurrentCol - 1);
                if ((CurrentRow - 2 == RowToBeMovedTo) && (CurrentCol + 2 == ColToBeMovedTo))
                    return new Tuple<int, int>(CurrentRow - 1, CurrentCol + 1);
                if ((CurrentRow + 2 == RowToBeMovedTo) && (CurrentCol - 2 == ColToBeMovedTo))
                    return new Tuple<int, int>(CurrentRow + 1, CurrentCol - 1);
                if ((CurrentRow + 2 == RowToBeMovedTo) && (CurrentCol + 2 == ColToBeMovedTo))
                    return new Tuple<int, int>(CurrentRow + 1, CurrentCol + 1);
            }

            return null;
        }
    }
}
