using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

/*  Computer Player Logic, inherits from Player.cs
 *  
 *  Functions
 *  
 *  MakeMove(string[] gameBoard, int moveCounter)
 *  Takes gameboard and how many moves have been taken,
 *  Then decides what to do and returns int of button.
 *  
 *  RandomiseWinArrays(string[] winArray)
 *  Takes the array of all winning combinations and randomises the order
 */


namespace TicTacToe
{
    class ComputerPlayer : Player
    {
        Random r = new Random();

        public int MakeMove(string[] gameBoard, int moveCounter)
        {
            string[] winArray = new string[8];
            winArray[0] = gameBoard[0] + gameBoard[1] + gameBoard[2] + "012";
            winArray[1] = gameBoard[3] + gameBoard[4] + gameBoard[5] + "345";
            winArray[2] = gameBoard[6] + gameBoard[7] + gameBoard[8] + "678";

            winArray[3] = gameBoard[0] + gameBoard[3] + gameBoard[6] + "036";
            winArray[4] = gameBoard[1] + gameBoard[4] + gameBoard[7] + "147";
            winArray[5] = gameBoard[2] + gameBoard[5] + gameBoard[8] + "258";

            winArray[6] = gameBoard[0] + gameBoard[4] + gameBoard[8] + "048";
            winArray[7] = gameBoard[2] + gameBoard[4] + gameBoard[6] + "246";

            string[] winArrayRandomised = new string[8];

            winArrayRandomised = RandomiseWinArrays(winArray);


            // Check for win or block
            foreach (string array in winArray)
            {
                int Ocount = array.Split('O').Length - 1;
                int Xcount = array.Split('X').Length - 1;
                // If computer can win
                if (Ocount == 2 && Xcount == 0)
                {                                                          
                    int indexOfwinIndexInArray = array.IndexOf(" ") + 3;
                    char c = array[indexOfwinIndexInArray];
                    int winningIndex = int.Parse(c.ToString());
                    return winningIndex;     
                }                             
            }

            // If computer can block player
            foreach (string array in winArray)            
            {
                int Ocount = array.Split('O').Length - 1;
                int Xcount = array.Split('X').Length - 1;                
                if (Xcount == 2 && Ocount == 0)
                {
                    int indexOfwinIndexInArray = array.IndexOf(" ") + 3;
                    char c = array[indexOfwinIndexInArray];
                    int blockWinIndex = int.Parse(c.ToString());
                    return blockWinIndex;
                }
            } 

            // If centre free take it
            if (gameBoard[4] == " ")
            {
                return 4;
            }

            // If centre is taken and its first move take corner to force draw
            if(gameBoard[4] == "X" && moveCounter == 1)
            {
                int randInt = r.Next(0, 4);
                int[] corners = new int[4];
                corners[0] = 0;
                corners[1] = 2;
                corners[2] = 6;
                corners[3] = 8;

                return corners[randInt];
            }

            //Take space next to another computer taken space
            foreach (string array in winArray)
            {
                int Ocount = array.Split('O').Length - 1;
                int Xcount = array.Split('X').Length - 1;
                if (Ocount == 1 && Xcount == 0)
                {
                    int indexOfO = array.IndexOf("O");   
                    
                    // Take space either side
                    if(indexOfO == 1)
                    {
                        int[] possibleIndexs = new int[2];
                        possibleIndexs[0] = 3;
                        possibleIndexs[1] = 5;
                        int randomInt = r.Next(0, 1);

                        int indexOfNextDoor = possibleIndexs[randomInt];
                        char c = array[indexOfNextDoor];
                        int nextdoorIndex = int.Parse(c.ToString());
                        return nextdoorIndex;
                    }
                    //Take space next to or one gap over
                    if (indexOfO == 0)
                    {
                        int[] possibleIndexs = new int[2];
                        possibleIndexs[0] = 4;
                        possibleIndexs[1] = 5;
                        int randomInt = r.Next(0, 1);

                        int indexOfinLineIndex = possibleIndexs[randomInt];
                        char c = array[indexOfinLineIndex];
                        int indexinLine = int.Parse(c.ToString());
                        return indexinLine;

                    }
                    //Take space next to or one gap over
                    if (indexOfO == 2)
                    {
                        int[] possibleIndexs = new int[2];
                        possibleIndexs[0] = 3;
                        possibleIndexs[1] = 4;
                        int randomInt = r.Next(0, 1);

                        int indexOfinLineIndex = possibleIndexs[randomInt];
                        char c = array[indexOfinLineIndex];
                        int indexinLine = int.Parse(c.ToString());
                        return indexinLine;
                    }                    
                }
            }

            // Make random move
            while (true)
            {
                int randomMoveInt = r.Next(0, 8);
                if(gameBoard[randomMoveInt] == " ")
                {
                    return randomMoveInt;
                }
                
            }        
        }

        private string[] RandomiseWinArrays(string[] winArray)
        {
            string[] winArrayRandomised = new string[8];
            ArrayList possibleIndexes = new ArrayList();

            possibleIndexes.Add(0);
            possibleIndexes.Add(1);
            possibleIndexes.Add(2);
            possibleIndexes.Add(3);
            possibleIndexes.Add(4);
            possibleIndexes.Add(5);
            possibleIndexes.Add(6);
            possibleIndexes.Add(7);

            for(int n = 7; n >= 0; n--)
            {
                int randomInt = r.Next(0, n);
                winArrayRandomised[(int)possibleIndexes[randomInt]] = winArray[n];              
                possibleIndexes.RemoveAt(randomInt);                    
            }
            return winArrayRandomised;
        }
    }
}
