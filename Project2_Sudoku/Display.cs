//================================================
//Author: Vincent Heredia
//Last Modification: 11/30/1015
//Purpose: Displays the board
//================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2_Sudoku
{
    class Display
    {

        //Purpose: Displays the board in the console
        //Parameter: The board to display
        public void displayBoard(Board board)
        {
            char Rows = 'A';


            Console.Write("      0     1     2     3     4     5     6     7     8      \n");
            for (int i = 0; i < board.getSize(); i++)
            {

                if (i % 3 == 0)
                {
                    Console.Write("  ========================================================= \n");
                }
                else
                {
                    Console.Write("  --------------------------------------------------------- \n");
                }

                Console.Write(Rows);
                for (int j = 0; j < board.getSize(); j++)
                {
                    if (j % 3 == 0)
                    {   
                        if (board.getValue(i, j) == 0)
                        {
                            Console.Write(" |||  ");
                        }
                        else if (board.getOriginalValue(i, j) == true)
                        {
                            Console.Write(" |||" + board.getValue(i, j) + "'");
                        }
                        else
                        {
                            Console.Write(" ||| " + board.getValue(i, j));
                        }
                    }
                    else
                    {
                        if (board.getValue(i, j) == 0)
                        {
                            Console.Write("  |   ");
                        }
                        else if (board.getOriginalValue(i, j) == true)
                        {
                            Console.Write("  | " + board.getValue(i, j) + "'");
                        }
                        else
                        {
                            Console.Write("  |  " + board.getValue(i, j));
                        }
                    }   
                }

                Console.Write(" ||| \n");
                Rows++;
            }

            Console.Write("  ========================================================= \n");

        }
        


    }
}
