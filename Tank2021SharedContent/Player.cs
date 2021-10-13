using System;
using System.Collections.Generic;
using System.Text;

namespace Tank2021SharedContent
{
    public enum PlayerType
    {
        PLAYER1,
        PLAYER2
    }
    public class Player
    {
        public int Coins { get; set; }
        public Tank Tank { get; set; }

        public Player()
        {

        }
    }
}
