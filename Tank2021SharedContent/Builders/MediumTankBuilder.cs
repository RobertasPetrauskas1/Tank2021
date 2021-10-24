using System;
using System.Collections.Generic;
using System.Text;
using Tank2021SharedContent.Abstract.Tanks;
using Tank2021SharedContent.Constants;

namespace Tank2021SharedContent.Builders
{
    public class MediumTankBuilder : Builder
    {
        public Tank Tank { get; set; }

        public MediumTankBuilder(Tank tank)
        {
            Tank = tank;
        }

        public override void BuildTankHealth()
        {
            Tank.SetHealth(ServerSideConstants.MediumTankHealth);
        }

        public override void BuildTankPhoto()
        {
            Tank.SetPhoto(@"../../../Properties/Resources/medium_tank.png");
        }

        public override void BuildTankSpeed()
        {
            Tank.SetSpeed(6);
        }

        public override Tank GetResult()
        {
            return Tank;
        }
    }
}
