using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

/*
 * View controller for Tic Tac Toe
 * 
 * Holds a game board which holds a Player class and ComputerPlayer class
 * 
 * Has button click event handler that checks the button has not been clicked, (checks if the text is empty or not)
 * Then fires game.PlayerMove(), checks for win, fires game.ComputerMove(), then checks for win while calling UpdatingUI when needed.
 * 
 */

namespace TicTacToe
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        Game game;

        public MainPage()
        {
            InitializeComponent();
            game  = new Game();

            _ = game.LoadDataAsync();

            // When "Loaded" is sent, run UpdateBoard
            MessagingCenter.Subscribe<object>(this, "Loaded", (sender) => 
            {
                UpdateGameBoard();
            });


            // When "Sleeping" is sent, run SaveQuotes            
            MessagingCenter.Subscribe<object>(this, "Sleeping", (sender) =>
            {
                _ = game.SaveDataAsync(); // Run save function
            });
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Button buttonPressed = (Button)sender;
            string buttonClassID = buttonPressed.ClassId;
            UpdateGameBoard();
            if (String.IsNullOrWhiteSpace(buttonPressed.Text))
            {
                game.PlayerMove(buttonClassID);                
                if (!CheckIfFinished())
                {                    
                    game.ComputerMove();
                    CheckIfFinished();
                }
            }
            
        }


        private void UpdateGameBoard()
        {
            
            TopLeft.Text = game.gameBoard[0];
            TopMiddle.Text = game.gameBoard[1];
            TopRight.Text = game.gameBoard[2];

            MiddleLeft.Text = game.gameBoard[3];
            MiddleMiddle.Text = game.gameBoard[4];
            MiddleRight.Text = game.gameBoard[5];

            BottomLeft.Text = game.gameBoard[6];
            BottomMiddle.Text = game.gameBoard[7];
            BottomRight.Text = game.gameBoard[8];
            Debug.WriteLine("Updated the board");
        }


        private bool CheckIfFinished()
        {    
            if (game.isGameOver)
            {
                // Resetboard
                if (game.winner == 1) // Player Wins
                {
                    //System.Threading.Thread.Sleep(500);
                    DisplayAlert("Player Wins!", "Player: " + game.player.Score.ToString() + "   Computer: " + game.computerPlayer.Score.ToString(), "Play Again");
                    game.ResetBoard();
                    UpdateGameBoard();
                    return true;
                }
                else if (game.winner == 2) // Computer Wins
                {
                    //System.Threading.Thread.Sleep(500);
                    DisplayAlert("Computer Wins!", "Player: " + game.player.Score.ToString() + "   Computer: " + game.computerPlayer.Score.ToString(), "Play Again");
                    game.ResetBoard();
                    UpdateGameBoard();
                    return true;
                }
                else
                {
                    //System.Threading.Thread.Sleep(500);
                    DisplayAlert("Draw", "Player: " + game.player.Score.ToString() + "   Computer: " + game.computerPlayer.Score.ToString(), "Play Again");
                    game.ResetBoard();
                    UpdateGameBoard();
                    return true;
                }
            }
            else
            {
                UpdateGameBoard();
                return false;                
            }
        }
    }
}
