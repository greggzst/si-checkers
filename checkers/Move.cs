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

        public Move(int cRow, int cCol, int mRow, int mCol)
        {
            CurrentRow = cRow;
            CurrentCol = cCol;
            RowToBeMovedTo = mRow;
            ColToBeMovedTo = mCol;
        }
    }
}
