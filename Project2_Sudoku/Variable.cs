//================================================
//Author: Vincent Heredia
//Last Modification: 11/30/1015
//Purpose: Contains the variable value, domain, and constraints
//================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2_Sudoku
{
    class Variable
    {
        //Purpose: Initializes the domain of the variable
        public void initializeDomain()
        {
            domain.Clear();
            for (int i = 0; i < 9; i++)
            {
                domain.Add(i + 1);
            }
        }


        //Purpose: Get and set for the constraint array
        public List<Constraints> getConstraints()
        {
            return constraints;
        }
        public void setConstraints(List<Constraints> newConstraints)
        {
            constraints = newConstraints;
        }
        public void addConstraints(int row, int column)
        {
            //detect if the new constraint is already in the constraint list

            //for each constraint in the list
            for(int i = 0; i < constraints.Count; i++)
            {
                //if the constraint is already in the list, return
                if(row == constraints[i].getRow() && column == constraints[i].getColumn())
                {
                    return;
                }
            }

            Constraints temp = new Constraints();
            temp.setRow(row);
            temp.setColumn(column);
            constraints.Add(temp);
        }


        //Purpose: Get and set for the domain array
        public List<int> getDomain()
        {
            return domain;
        }
        public void setDomain(List<int> newDomain)
        {
            domain = newDomain;
        }

        public void addVarToDomain(int var)
        {
            domain.Add(var);
        }
        public void removeVarFromDomain(int var)
        {
            domain.Remove(var);
        }

        //Purpose: Prints the constraints in a list
        public void printConstraints()
        {
            for(int i = 0; i < constraints.Count; i++)
            {
                Console.Write("Row: " + constraints[i].getRow() + " Column: " + constraints[i].getColumn() + "\n");
            }
        }
        

        //Purpose: Get and set for the value of this node
        public void setValue(int newValue)
        {
            value = newValue;
        }
        public int getValue()
        {
            return value;
        }

        public void setOriginalValue(bool newOriginalValue)
        {
            originalValue = newOriginalValue;
        }
        public bool getOriginalValue()
        {
            return originalValue;
        }


        private List<int> domain = new List<int>();

        private List<Constraints> constraints = new List<Constraints>();

        private int value = 0;

        private bool originalValue = false;//if this is a preset value, then we cant change it

    }
}
