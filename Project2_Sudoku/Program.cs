//================================================
//Author: Vincent Heredia
//Last Modification: 11/30/1015
//Purpose: Program entry point. Contains the main game loop
//================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2_Sudoku
{
    class Program
    {
        //Purpose: Main function
        static void Main(string[] args)
        {
            Boolean notDone = true;
            string userInput = "";

            while (notDone)
            {
                Console.Write("Would you like to play Sudoku? (y/n)\nInput: ");
                userInput = Console.ReadLine();
                Console.Write("\n\n");

                if (userInput == "y")
                {
                    playGame();
                }
                else if (userInput == "n")
                {
                    notDone = false;
                }
                else
                {
                    Console.Write("Invalid Input");
                }

                Console.Write("\n\n");
            }


        }



        //Purpose: Initializes the board and lets the player play
        public static void playGame()
        {
            Board board = new Board();
            board.makeBoard(0);
            Display display = new Display();

            Boolean notFinished = true;
            string[] userInput;
            int userRow = 0;
            int userColumn = 0;
            int userValue = 0;
            int oldValue = 0;//used to store the old value of the node


            board.AC3();//need to run the AC-3 algorithm once before starting
            while (notFinished)
            {
                //Display Board
                display.displayBoard(board);
       
                //Get input
                Console.Write("Insert a Value (Example: A2 1 ), type exit to quit\nInput: ");
                userInput = Console.ReadLine().Split(' ');

                if(userInput[0] == "exit")
                {
                    return;
                }

                if(userInput.Length != 2 || userInput[0].Length != 2 || userInput[1].Length != 1)
                {
                    Console.Write("Invalid Input...\n\n");
                    continue;
                }

                //convert the user's input
                userRow = char.ToUpper(userInput[0][0]) - 65;
                userColumn = userInput[0][1] - 48;
                userValue = userInput[1][0] - 48;

                //check the user's input to make sure it isnt out of range and is a number from 1 to 9
                if (checkInput(userRow, userColumn, userValue, board))
                {
                    if (board.getOriginalValue(userRow, userColumn))
                    {
                        Console.Write("Cannot modify original values...\n\n");
                        continue;
                    }
                    //----------------------------AI implementation-----------------------------------
                    //Insert value
                    board.insertValue(userRow, userColumn, userValue);

                    //check if the user has violated a constraint
                    if (board.checkDomain(userRow, userColumn, userValue) == false)
                    {
                        //If the input violates the constraints, remove value
                        if(userValue != 0)
                        {
                            Console.Write("Constraints not met, removing value\n\n");
                        }
                        else
                        {
                            Console.Write("Removing value\n\n");
                        }
                        board.removeValue(userRow, userColumn);
                        board.resetDomains();
                        board.AC3();
                        continue;
                    }

                    board.resetDomains();//resets the domain of all the variables
                    if (board.AC3())//run AC-3 algorithm
                    {
                        Console.WriteLine("AC-3 Returned True");
                    }
                    else//if AC-3 algorithm returns false
                    {
                        Console.Write("AC-3 Returned false, Something is wrong");
                    }


                    if (checkForWin(board) == true)
                    {
                        Console.Write("You have won!");
                        notFinished = false;//If they have, notFinished == false;
                    }

                }//end of if (checkInput(userRow, userColumn, userValue, board))
                else//if the input is not valid
                {
                    Console.Write("Invalid Input...\n\n");
                    continue;
                }
                Console.Write("\n\n");
            }

            

        }//end of play game


        //Purpose: Checks the player's input to make sure it is valid
        //Parameters: the row, column, and the value
        //Returns: False if the input is invalid, else true
        public static Boolean checkInput(int userRow, int userColumn, int userValue, Board board)
        {
            if(userRow >= board.getSize() || userColumn >= board.getSize())
            {
                return false;
            }
            else if (userValue > 9 || userValue < 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //Purpose: Checks for a win
        //Parameters: the board
        //Returns: True if the player has won, else false
        public static Boolean checkForWin(Board board)
        {
            //check each space for a 0
            for(int i = 0; i < board.getSize(); i++)
            {
                for (int j = 0; j < board.getSize(); j++)
                {
                    if (board.getValue(i,j) == 0)
                    {
                        return false;
                    }
                }

            }
            //if all spaces have a value, then the player has won
            return true;
        }

    }//end of class
}



/*  Solution to case 0
    Puzzle from http://www.websudoku.com/
    Puzzle: 10,441,210,062 

    6 9 1  2 7 5  3 4 8
    4 3 2  8 6 1  5 9 7
    8 7 5  9 4 3  2 1 6

    9 8 6  3 2 4  1 7 5
    2 1 7  6 5 8  4 3 9
    5 4 3  1 9 7  6 8 2

    3 6 9  7 1 2  8 5 4
    1 2 4  5 8 9  7 6 3
    7 5 8  4 3 6  9 2 1



*/
