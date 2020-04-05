using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe
{
    class Player
    {

        private int _score;
        public int Score
        {
            get { return _score; }
            set { _score = value; }
        }


        public void IncreaseScore()
        {
            System.Diagnostics.Debug.WriteLine("Increase score!");
            _score++;
        }
    }
}
