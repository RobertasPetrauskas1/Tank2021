using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank2021Client.Mediator.Enum;
using Tank2021Client.Mediator.Mediators;
using Tank2021SharedContent.Abstract.Tanks;
using Tank2021SharedContent.Enums;

namespace Tank2021Client.Mediator.Colleagues
{
    public class ClientScoreColleague : BaseColleague
    {
        public ClientScoreColleague(IMediator mediator) : base(mediator)
        {
        }

        public void UpdateScores(Tank tank, PlayerType playerType)
        {
            SendData(new TankStatus() { PlayerType = playerType, ArmorHits = tank?.Armor?.HitsLeft, Health = tank?.Health }, "ChangedScore");
        }

        public override ColleagueType getType()
        {
            return ColleagueType.Score;
        }

        public override void ReceiveData(object data, string ev)
        {
            if(ev == "UpdateScoreTank1" && data is Tank tank)
            {
                UpdateScores(tank, PlayerType.PLAYER1);
            }

            if (ev == "UpdateScoreTank2" && data is Tank tank1)
            {
                UpdateScores(tank1, PlayerType.PLAYER2);
            }
        }

        public override void SendData(object data, string ev)
        {
            _mediator.Notify(ColleagueType.Score, data, ev);
        }
    }
}
