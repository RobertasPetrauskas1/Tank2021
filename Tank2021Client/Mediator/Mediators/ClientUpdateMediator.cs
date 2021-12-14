using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank2021Client.Facade;
using Tank2021Client.Mediator.Colleagues;
using Tank2021Client.Mediator.Enum;
using Tank2021SharedContent.Enums;

namespace Tank2021Client.Mediator.Mediators
{
    public class ClientUpdateMediator : IMediator
    {
        private ClientBulletColleague _clientBullets { get; set;}
        private GameWindowColleague _gameWindow { get; set; }
        private ClientTankColleague _clientTank { get; set; }
        private ClientScoreColleague _clientScores { get; set; }

        public ClientUpdateMediator()
        {
            _clientBullets = new ClientBulletColleague(this);
            _clientTank = new ClientTankColleague(this);
            _clientScores = new ClientScoreColleague(this);
        }

        public void Notify(ColleagueType sender, object data, string ev)
        {
            if (sender.Equals(ColleagueType.GameWindow) && ev.Equals("UpdateBullet"))
                _clientBullets.ReceiveData(data, ev);
            if (sender.Equals(ColleagueType.GameWindow) && ev.Equals("UpdateTank"))
                _clientTank.ReceiveData(data, ev);
            if (sender.Equals(ColleagueType.GameWindow) && ev.Equals("UpdateScoreTank1") || ev.Equals("UpdateScoreTank2"))
                _clientScores.ReceiveData(data, ev);

            if (sender.Equals(ColleagueType.Bullet) && ev.Equals("ChangedBullet"))
                _gameWindow.ReceiveData(data, ev);
            if (sender.Equals(ColleagueType.Tank) && ev.Equals("ChangedTank"))
                _gameWindow.ReceiveData(data, ev);
            if (sender.Equals(ColleagueType.Score) && ev.Equals("ChangedScore"))
                _gameWindow.ReceiveData(data, ev);
        }

        public void AddColleague(BaseColleague colleague)
        {
            if (colleague.getType().Equals(ColleagueType.GameWindow))
                _gameWindow = (GameWindowColleague)colleague;
        }
    }
}
