using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;
using PCLStorage;
using Xamarin.Forms;


/* Game class, holds player/computerPlayer classes, controls saving/loading and move making.
 * 
 * functions:
 * 
 * PlayerMove(string pressedButton)                 -- Makes player move, sets clicked button to playerSymbol then calls CheckWinCondition()
 * ComputerMove()                                   -- Makes computer move, sets clicked button to computerSymbol then calls CheckWinCondition()
 * CheckWinCondition()                              -- Checks the gameboard to see if it finds "OOO" or "XXX", increased score if true and sets isGameOver bool to true.
 * ResetBoard()                                     -- Resets gameboard, moveCounter, isGameOver bool and winner int.
 * 
 * LoadDataAsync()                                  -- Loads game data into game board and sends out message to MainPage to call UpdateUI()
 * SaveDataAsync()                                  -- Saves game state and scores on APP close
 * GetGameStateInString()                           -- Puts gamestate and scores into a single string for saving
 * TurnStringIntoGameState(string _strGameBoard)    -- Takes string from loading and turns that into current game state
 */

namespace TicTacToe
{
    class Game
    {
        public Player player = new Player();
        public ComputerPlayer computerPlayer = new ComputerPlayer();


        public string[] gameBoard = new string[9];
        string[] winArray = new string[8];

        string playerSymbol = "X";
        string computerSymbol = "O";
        int moveCounter = 0;
        const int MAX_MOVES = 9;


        public bool isGameOver = false;
        public int winner = 0;

        
        public void PlayerMove(string pressedButton)
        {   
                int whatButton;
                bool successfullyParsed = int.TryParse(pressedButton, out whatButton);
                gameBoard[whatButton] = playerSymbol;
                moveCounter++;
                CheckWinCondition();    
        }
        

        public void ComputerMove()
        {                               
            gameBoard[computerPlayer.MakeMove(gameBoard, moveCounter)] = computerSymbol;
            moveCounter++;
            CheckWinCondition();             
        }

 

        public void CheckWinCondition()
        {
            winArray[0] = gameBoard[0] + gameBoard[1] + gameBoard[2];
            winArray[1] = gameBoard[3] + gameBoard[4] + gameBoard[5];
            winArray[2] = gameBoard[6] + gameBoard[7] + gameBoard[8];

            winArray[3] = gameBoard[0] + gameBoard[3] + gameBoard[6];
            winArray[4] = gameBoard[1] + gameBoard[4] + gameBoard[7];
            winArray[5] = gameBoard[2] + gameBoard[5] + gameBoard[8];

            winArray[6] = gameBoard[0] + gameBoard[4] + gameBoard[8];
            winArray[7] = gameBoard[2] + gameBoard[4] + gameBoard[6];

            foreach (string winA in winArray)
            {                
                if (winA == "XXX")
                {                    
                    player.IncreaseScore();
                    isGameOver = true;
                    winner = 1;
                    return;
                }
                else if (winA == "OOO")
                {
                    computerPlayer.IncreaseScore();
                    isGameOver = true;
                    winner = 2;
                    return;
                }
            }
            if (moveCounter >= MAX_MOVES) // Max moves = Draw
            {
                winner = 3;
                isGameOver = true;
                return;
            }
            if(winner == 0)// Still in Play
            {
                isGameOver = false;
            }
        }

        public void ResetBoard()
        {
            isGameOver = false;
            winner = 0;
            moveCounter = 0;
            gameBoard = new string[9];
            for(int n = 0; n < gameBoard.Length; n++)
            {
                gameBoard[n] = " ";
            }
        }


        public async System.Threading.Tasks.Task LoadDataAsync()
        {
            IFolder folder = FileSystem.Current.LocalStorage; // Sets current folder
            folder = await folder.CreateFolderAsync("save10", CreationCollisionOption.OpenIfExists);
            IFile file = await folder.CreateFileAsync("saveData.txt", CreationCollisionOption.OpenIfExists);

            byte[] streamBuffer;
            using (System.IO.Stream stream = await file.OpenAsync(FileAccess.ReadAndWrite))
            { 
                    long length = stream.Length;
                    streamBuffer = new byte[length];
                    stream.Read(streamBuffer, 0, (int)length);          
            }

            string _strGameBoard = Encoding.ASCII.GetString(streamBuffer);
            TurnStringIntoGameState(_strGameBoard);                    
            MessagingCenter.Send<object>(this, "Loaded"); // Send message "Loaded" so board can update on MainPage
        }

        public async System.Threading.Tasks.Task SaveDataAsync()
        {
            IFolder folder = FileSystem.Current.LocalStorage; // Sets current folder
            folder = await folder.CreateFolderAsync("save10", CreationCollisionOption.OpenIfExists);
            IFile file = await folder.CreateFileAsync("saveData.txt", CreationCollisionOption.OpenIfExists);


            byte[] dataToSave = Encoding.ASCII.GetBytes(GetGameStateInString());

            using (System.IO.Stream stream = await file.OpenAsync(FileAccess.ReadAndWrite))
            {        
                    stream.Write(dataToSave, 0, dataToSave.Length);
            }
            
        }

        private string GetGameStateInString()
        {
            string _strGameBoard = "";
            foreach (string tile in gameBoard)
            {
                if (String.IsNullOrWhiteSpace(tile))
                {
                    _strGameBoard = _strGameBoard + " ";
                }
                else
                {
                    _strGameBoard = _strGameBoard + tile;
                }
            }
            _strGameBoard = _strGameBoard + player.Score;
            _strGameBoard = _strGameBoard + ":";
            _strGameBoard = _strGameBoard + computerPlayer.Score;
            _strGameBoard = _strGameBoard + "-";
            _strGameBoard = _strGameBoard + moveCounter.ToString();
            return _strGameBoard;
        }

        private void TurnStringIntoGameState(string _strGameBoard)
        {
            for (int n = 0; n < 9; n++)
            {
                if (n < 9)
                {
                    char c = _strGameBoard[n];
                    gameBoard[n] = c.ToString();
                }
            }
            int indexOfScoreSplit = _strGameBoard.IndexOf(":");
            int indexOfMoveCounter = _strGameBoard.IndexOf("-");
            player.Score = int.Parse(_strGameBoard.Substring(9, (indexOfScoreSplit - 9)));
            computerPlayer.Score = int.Parse(_strGameBoard.Substring((indexOfScoreSplit + 1), (indexOfMoveCounter - indexOfScoreSplit - 1)));
            moveCounter = int.Parse(_strGameBoard.Substring(indexOfMoveCounter + 1));

        }
    }
}
