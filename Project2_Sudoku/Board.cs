//================================================
//Author: Vincent Heredia
//Last Modification: 11/30/1015
//Purpose: Holds the board matrix and contains the AC-3 algorithm
//================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2_Sudoku
{
    class Board
    {

        //Purpose: Creates the board and initializes the boards values
        //Parameter: Selects the board that the player will start with
        public void makeBoard(int boardNumber)
        {
            boardWidthAndHeight = 9;//9 by 9 matrix

            board = new Variable[boardWidthAndHeight, boardWidthAndHeight];


            for (int i = 0; i < boardWidthAndHeight; i++)
            {
                for (int j = 0; j < boardWidthAndHeight; j++)
                {
                    board[i, j] = new Variable();
                    board[i, j].initializeDomain();
                    initializeConstraints(i,j);
                }
            }

            //initializes the boards preset variables
            switch (boardNumber)
            {
                case 0:
                    {
                        board[0, 1].setValue(9); board[0, 2].setValue(1); board[0, 6].setValue(3);
                        board[0, 7].setValue(4); board[0, 8].setValue(8);

                        board[0, 1].setOriginalValue(true); board[0, 2].setOriginalValue(true);
                        board[0, 6].setOriginalValue(true); board[0, 7].setOriginalValue(true);
                        board[0, 8].setOriginalValue(true);

                        board[1, 0].setValue(4); board[1, 1].setValue(3); board[1, 4].setValue(6);
                        board[1, 5].setValue(1); board[1, 6].setValue(5);

                        board[1, 0].setOriginalValue(true); board[1, 1].setOriginalValue(true);
                        board[1, 4].setOriginalValue(true); board[1, 5].setOriginalValue(true);
                        board[1, 6].setOriginalValue(true);

                        board[2, 0].setValue(8); board[2, 6].setValue(2); board[2, 7].setValue(1);

                        board[2, 0].setOriginalValue(true); board[2, 6].setOriginalValue(true);
                        board[2, 7].setOriginalValue(true);

                        board[3, 0].setValue(9); board[3, 4].setValue(2); board[3, 5].setValue(4);

                        board[3, 0].setOriginalValue(true); board[3, 4].setOriginalValue(true);
                        board[3, 5].setOriginalValue(true);

                        board[4, 0].setValue(2); board[4, 2].setValue(7); board[4, 6].setValue(4);
                        board[4, 8].setValue(9);

                        board[4, 0].setOriginalValue(true); board[4, 2].setOriginalValue(true);
                        board[4, 6].setOriginalValue(true);board[4, 8].setOriginalValue(true);

                        board[5, 3].setValue(1); board[5, 4].setValue(9); board[5, 8].setValue(2);

                        board[5, 3].setOriginalValue(true); board[5, 4].setOriginalValue(true);
                        board[5, 8].setOriginalValue(true);

                        board[6, 1].setValue(6); board[6, 2].setValue(9); board[6, 8].setValue(4);

                        board[6, 1].setOriginalValue(true); board[6, 2].setOriginalValue(true);
                        board[6, 8].setOriginalValue(true);

                        board[7, 2].setValue(4); board[7, 3].setValue(5); board[7, 4].setValue(8);
                        board[7, 7].setValue(6); board[7, 8].setValue(3);

                        board[7, 2].setOriginalValue(true); board[7, 3].setOriginalValue(true);
                        board[7, 4].setOriginalValue(true); board[7, 7].setOriginalValue(true);
                        board[7, 8].setOriginalValue(true);

                        board[8, 0].setValue(7); board[8, 1].setValue(5); board[8, 2].setValue(8);
                        board[8, 6].setValue(9); board[8, 7].setValue(2);

                        board[8, 0].setOriginalValue(true); board[8, 1].setOriginalValue(true);
                        board[8, 2].setOriginalValue(true); board[8, 6].setOriginalValue(true);
                        board[8, 7].setOriginalValue(true);

                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            return;
        }


        //Purpose: Inserts a value into the board
        //Parameters: The row and column, the value to insert
        public void insertValue(int row, int column, int value)
        {
            if (row < boardWidthAndHeight || column < boardWidthAndHeight)
            {
                board[row, column].setValue(value);
            }
        }

        //Purpose: Removes a value from the board
        //Parameters: The row and column
        public void removeValue(int row, int column)
        {
            board[row, column].setValue(0);
        }

        //Purpose: Returns a value from the board
        //Parameters: The row and column
        //Returns: The value in that space
        public int getValue(int row, int column)
        {
            return board[row, column].getValue();
        }

        public bool getOriginalValue(int row, int column)
        {
            return board[row, column].getOriginalValue();
        }

        //Purpose: Returns the size of the board
        //Returns: the size
        public int getSize()
        {
            return boardWidthAndHeight;
        }

        //Purpose: Checks the domain of the node to see if the user's value
        //         is in it
        //Parameters: the row, the column and the user's valyue
        //Returns: true if the the user's value is in the domain
        public bool checkDomain(int row, int column, int value)
        {
            List<int> domain = board[row, column].getDomain();

            for(int i = 0; i < domain.Count; i++)
            {
                if (domain[i] == value)
                {
                    return true;
                }
            }

            return false;
        }


        //Purpose: Initializes the constraints for each node
        //Parameter: the row and the column
        private void initializeConstraints(int row, int column)
        {
            //array of section "headers"
            //A section is one of the 3x3 cubes in the sudoku board
            /// the header is the top left space in a section
            int[] section = new int[18];
            int sectionRow = 0;
            int sectionColumn = 0;
            
            //populate the array with each section "header"
            int tempRow = 0, tempColumn = 0;
            for(int i = 0; i < section.Length; i++)
            {
                section[i] = tempRow;
                i++;
                section[i] = tempColumn;
                i++; tempColumn += 3;

                section[i] = tempRow;
                i++;
                section[i] = tempColumn;
                i++; tempColumn += 3;

                section[i] = tempRow;
                i++;
                section[i] = tempColumn;
                tempRow += 3; tempColumn = 0;
            }

            //Add constraints to this node
            //row and column constraints
            for (int i = 0; i < boardWidthAndHeight; i++)
            {
                if (i != row)
                {
                    //add constraints for row
                    board[row, column].addConstraints(i, column);
                }

                if (i != column)
                {
                    //add constraints for column
                    board[row, column].addConstraints(row, i);
                }

                
            }

            //section constraints
            for(int i = 0; i < section.Length; i++)
            {
                if((row >= section[i]) && 
                   (row < section[i] + 3) &&
                   (column >= section[i+1]) && 
                   (column < section[i+1] + 3))
                {
                    sectionRow = section[i];
                    sectionColumn = section[i + 1];
                }

                i++;
            }

            //add the section to the constraints
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if(i + sectionRow == row && j + sectionColumn == column)
                    {
                        continue;
                    }
                    board[row, column].addConstraints(i + sectionRow, j + sectionColumn);
                }
            }
        }//end of initializeConstraints(int row, int column)


        //Purpose: returns false if an inconsistency is found and true otherwise
        public bool AC3()
        {
            //AC3 Algorithm from the book

            //local variables: Queue of arcs, initially all the arcs in CSP
            Queue<Arc> arcQueue = new Queue<Arc>();
            
            

            //Add all the arcs into the queue
            for (int i = 0; i < boardWidthAndHeight; i++)
            {
                for (int j = 0; j < boardWidthAndHeight; j++)
                {
                    for (int k = 0; k < board[i, j].getConstraints().Count; k++)
                    {
                        Arc temp = new Arc();//used for loading arcs into the arc queue, 
                        temp.setVar1Row(i);
                        temp.setVar1Column(j);

                        temp.setVar2Row(board[i, j].getConstraints()[k].getRow());
                        temp.setVar2Column(board[i, j].getConstraints()[k].getColumn());
                        arcQueue.Enqueue(temp);
                    }
                }
            }
            
            //while(queue is not empty) do 
            while (arcQueue.Count > 0)
            {
                Arc temp = new Arc();//holds the object removed from the queue
                //(Xi, Xj) <- Remove First(queue)
                temp = arcQueue.Dequeue();

                //if (Revise(csp, Xi, Xj))
                if (revise(temp))
                {
                    //if (size of Domain == 0)
                    if (board[temp.getVar1Row(), temp.getVar1Column()].getDomain().Count == 0)
                    {
                        return false;
                    }
                    //for (each Xk in Xi, Neighbors - {Xj} do
                    for (int k = 0; k < board[temp.getVar1Row(), temp.getVar1Column()].getConstraints().Count; k++)
                    {
                        Arc temp2 = new Arc();//used for loading new arcs into the arc queue
                        //add (Xk, Xi) to the queue
                        temp2.setVar1Row(board[temp.getVar1Row(), temp.getVar1Column()].getConstraints()[k].getRow());
                        temp2.setVar1Column(board[temp.getVar1Row(), temp.getVar1Column()].getConstraints()[k].getColumn());

                        temp2.setVar2Row(temp.getVar1Row());
                        temp2.setVar2Column(temp.getVar1Column());
                        arcQueue.Enqueue(temp2);
                    }
                  
                }
                
            }


            return true;
        }

        //Purpose: Returns true iff we revise the domain of Xi
        private bool revise(Arc arc)
        {
            int x = 0;//used to hold value from domain
            //revised <- false;
            bool revised = false;

            //for(each x in the domain){
            for (int i = 0; i < board[arc.getVar1Row(), arc.getVar1Column()].getDomain().Count; i++) {
                x = board[arc.getVar1Row(), arc.getVar1Column()].getDomain()[i];

                //if (no value y in Dj allows (x,y) to satisfy the constraint between Xi and Xj){
                if (board[arc.getVar2Row(), arc.getVar2Column()].getValue() == x) {
                    //delete x from Di
                    board[arc.getVar1Row(), arc.getVar1Column()].removeVarFromDomain(x);
                    //revised = true;
                    revised = true;
                }
            }
            
            return revised;
        }

        //Purpose: Resets the domain of each variable before reducing them with AC-3
        public void resetDomains()
        {
            for (int i = 0; i < boardWidthAndHeight; i++)
            {
                for (int j = 0; j < boardWidthAndHeight; j++)
                {
                    board[i, j].initializeDomain();
                }
            }
        }

        //board matrix
        private Variable[,] board;

        //width and height will always be the same
        private int boardWidthAndHeight;


    }
}
