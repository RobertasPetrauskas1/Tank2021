using Tank2021SharedContent.Strategy;

namespace Tank2021SharedContent.Observer.Subjects
{
    public class PlayerInfo
    {
        public int TankHealth { get; set; }
        public MoveAlgorithm TankMovement { get; set; }

        public PlayerInfo(int tankHealth, MoveAlgorithm tankMovement)
        {
            TankHealth = tankHealth;
            TankMovement = TankMovement;
        }
    }
}
