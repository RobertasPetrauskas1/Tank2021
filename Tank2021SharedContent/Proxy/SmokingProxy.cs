using System;
using System.Collections.Generic;
using System.Text;
using Tank2021SharedContent.Abstract.Tanks;
using Tank2021SharedContent.State;

namespace Tank2021SharedContent.Proxy
{
    public class SmokingProxy : TankState
    {
        private Smoking SmokingState;
        private Tank tank;
        private int QuarterHealth;
        public SmokingProxy(Tank tank)
        {
            this.tank = tank;
            QuarterHealth = (int)(Helper.GetSpecificTankHp(tank) * 0.25);
            SmokingState = new Smoking(tank);
        }

        public void HandleChange()
        {
            SmokingState.HandleChange();
        }

        public void TransitionState()
        {
           if(tank.Health <= QuarterHealth)
            {
                SmokingState.TransitionState();
            }
        }
    }
}
