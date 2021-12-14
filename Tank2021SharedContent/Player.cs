using Tank2021SharedContent.Abstract.Tanks;
using Tank2021SharedContent.Enums;

namespace Tank2021SharedContent
{
    public class Player
    {
        public bool IsConnected { get; set; }
        public int Points { get; set; }
        public Tank Tank { get; set; }
        public PlayerType PlayerType { get; set; }

        public Player(PlayerType playerType)
        {
            Points = 0;
            IsConnected = false;
            PlayerType = playerType;
        }

        public Player Copy()
        {
            return (Player)this.MemberwiseClone();
        }
    }
}
