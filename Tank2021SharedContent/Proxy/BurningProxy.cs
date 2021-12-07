using System;
using System.Collections.Generic;
using System.Text;
using Tank2021SharedContent.Abstract.Tanks;
using Tank2021SharedContent.State;

namespace Tank2021SharedContent.Proxy
{
    public class BurningProxy : TankState
    {
        private Burning BurningState;
        private Tank tank;
        private int CriticalHealth;
        public BurningProxy(Tank tank)
        {
            this.tank = tank;
            CriticalHealth = (int)(Helper.GetSpecificTankHp(tank) * 0.1);
            BurningState = new Burning(tank);
        }
        public void HandleChange()
        {
            BurningState.HandleChange();
        }

        public void TransitionState()
        {
            if(tank.Health <= CriticalHealth)
            {
                BurningState.TransitionState();
            }
        }
    }
}
