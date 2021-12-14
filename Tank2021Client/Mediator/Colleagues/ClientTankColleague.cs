using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank2021Client.Mediator.Enum;
using Tank2021Client.Mediator.Mediators;
using Tank2021SharedContent;
using Tank2021SharedContent.Abstract.Tanks;

namespace Tank2021Client.Mediator.Colleagues
{
    public class ClientTankColleague : BaseColleague
    {
        public ClientTankColleague(IMediator mediator) : base(mediator)
        {
        }

        public override ColleagueType getType()
        {
            return ColleagueType.Tank;
        }

        public override void ReceiveData(object data, string ev)
        {
            if(ev == "UpdateTank" && data is Tank tank)
            {
                UpdateTank(tank);
            }
        }

        public override void SendData(object data, string ev)
        {
            _mediator.Notify(ColleagueType.Tank, data, ev);
        }

        public void UpdateTank(Tank tank)
        {
            if (tank != null)
            {
                var tankImage = tank.TankImage.GetImage();
                SendData(new Figure(tank.Coordinates, tankImage.Width, tankImage.Height, tank.Rotation, tankImage), "ChangedTank");
            }
        }
    }
}
