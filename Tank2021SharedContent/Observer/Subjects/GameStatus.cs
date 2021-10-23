using System.Collections.Generic;
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
    }
}
