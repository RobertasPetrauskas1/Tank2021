using Tank2021SharedContent.Abstract.Tanks;

namespace Tank2021SharedContent
{
    public class Player
    {
        public bool IsConnected { get; set; }
        public int Points { get; set; }
        public Tank Tank { get; set; }

        public Player()
        {
            Points = 0;
            IsConnected = false;
        }
    }
}
