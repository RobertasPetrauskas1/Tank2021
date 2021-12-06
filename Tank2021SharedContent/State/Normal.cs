using System;
using System.Collections.Generic;
using System.Text;
using Tank2021SharedContent.Abstract.Tanks;
using Tank2021SharedContent.Strategy;

namespace Tank2021SharedContent.State
{
    public class Normal : TankState
    {
        private Tank tank;
        private int HalfHealth;
        public Normal(Tank tank)
        {
            this.tank = tank;
            HalfHealth = (int)(Helper.GetSpecificTankHp(tank) * 0.5);
        }

        public override void HandleChange()
        {
            // Do nothing
        }

        public override void TryTransitionState()
        {
            if (tank.Health <= HalfHealth)
            {
                tank.SetTankState(new Smoking(tank));
            }
        }
    }
}
