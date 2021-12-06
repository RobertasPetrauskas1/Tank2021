using System;
using System.Collections.Generic;
using System.Text;
using Tank2021SharedContent.Abstract.Tanks;
using Tank2021SharedContent.Strategy;

namespace Tank2021SharedContent.State
{
    public class Burning : TankState
    {
        private Tank tank;
        private int CriticalHealth;
        public Burning(Tank tank)
        {
            this.tank = tank;
            CriticalHealth = (int)(Helper.GetSpecificTankHp(tank) * 0.1);
        }
        public override void HandleChange()
        {
            tank.SetOnFire();
            tank.SetMoveAlgorithm(new SlowMovement());
        }

        public override void TryTransitionState()
        {
            if(tank.Health <= CriticalHealth)
            {
                tank.SetTankState(new Critical(tank));
            }
        }
    }
}
