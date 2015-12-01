//================================================
//Author: Vincent Heredia
//Last Modification: 11/30/1015
//Purpose: Holds the arcs for the AC-3 algorithm
//================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2_Sudoku
{
    class Arc
    {
        //Basic Gets and sets for the variables
        public int getVar1Row()
        {
            return var1Row;
        }
        public int getVar2Row()
        {
            return var2Row;
        }
        public void setVar1Row(int newVar)
        {
            var1Row = newVar;
        }
        public void setVar2Row(int newVar)
        {
            var2Row = newVar;
        }


        public int getVar1Column()
        {
            return var1Column;
        }
        public int getVar2Column()
        {
            return var2Column;
        }
        public void setVar1Column(int newVar)
        {
            var1Column = newVar;
        }
        public void setVar2Column(int newVar)
        {
            var2Column = newVar;
        }

        int var1Row = 0;
        int var1Column = 0;

        int var2Row = 0;
        int var2Column = 0;
    }
}
