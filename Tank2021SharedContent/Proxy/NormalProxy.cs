using System;
using System.Collections.Generic;
using System.Text;
using Tank2021SharedContent.Abstract.Tanks;
using Tank2021SharedContent.State;

namespace Tank2021SharedContent.Proxy
{
    public class NormalProxy : TankState
    {
        private Normal NormalState;
        private int HalfHealth;
        private Tank tank;

        public NormalProxy(Tank tank)
        {
            this.tank = tank;
            HalfHealth = (int)(Helper.GetSpecificTankHp(tank) * 0.5);
            NormalState = new Normal(tank);
        }

        public void HandleChange()
        {
            NormalState.HandleChange();
        }

        public void TransitionState()
        {
            if (tank.Health <= HalfHealth)
            {
                NormalState.TransitionState();
            }
        }
    }
}
