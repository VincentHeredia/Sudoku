//================================================
//Author: Vincent Heredia
//Last Modification: 11/30/1015
//Purpose: Holds a constraint for a variable
//================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2_Sudoku
{
    class Constraints
    {

        //Purpose: Get and set for the column array
        public int getColumn()
        {
            return column;
        }
        public void setColumn(int newColumn)
        {
            column = newColumn;
        }

        //Purpose: Get and set for the row array
        public void setRow(int newRow)
        {
            row = newRow;
        }
        public int getRow()
        {
            return row;
        }


        private int row = 0;
        private int column = 0;
    }
}
