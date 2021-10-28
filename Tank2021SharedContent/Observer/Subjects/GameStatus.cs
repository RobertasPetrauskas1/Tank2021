using System.Collections.Generic;
using Tank2021SharedContent.Enums;
using Tank2021SharedContent.Observer.Observers;

namespace Tank2021SharedContent.Observer.Subjects
{
    public class GameStatus : Subject
    {
        public PlayerInfo Player1Info { get; set; }
        public PlayerInfo Player2Info { get; set; }

        public GameStatus(PlayerInfo player1Info, PlayerInfo player2Info)
        {
            Observers = new List<IObserver>();
            Player1Info = player1Info;
            Player2Info = player2Info;
        }

        public GameStatus(Map map)
        {
            Observers = new List<IObserver>();

            var player1 = map.GetPlayer(PlayerType.PLAYER1);
            var player2 = map.GetPlayer(PlayerType.PLAYER2);

            Player1Info = new PlayerInfo(player1.Tank.Health, player1.Tank.MoveAlgorithm);
            Player2Info = new PlayerInfo(player2.Tank.Health, player2.Tank.MoveAlgorithm);
        }

        public override void AttatchObserver(IObserver observer)
        {
            Observers.Add(observer);
        }

        public override void DeattatchObserver(IObserver observer)
        {
            Observers.Remove(observer);
        }

        public override void NotifyAll()
        {
            foreach (var observer in Observers)
                observer.Update();
        }

        public void UpdateStatus(Map map)
        {
            var player1 = map.GetPlayer(PlayerType.PLAYER1);
            var player2 = map.GetPlayer(PlayerType.PLAYER2);

            Player1Info.TankHealth = player1.Tank.Health;
            Player1Info.TankMovement = player1.Tank.MoveAlgorithm;

            Player2Info.TankHealth = player2.Tank.Health;
            Player2Info.TankMovement = player2.Tank.MoveAlgorithm;

            NotifyAll();
        }
    }
}
