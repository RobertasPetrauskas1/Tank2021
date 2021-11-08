using System;
using System.Collections.Generic;
using System.Text;
using Tank2021SharedContent.Abstract.Tanks;
using Tank2021SharedContent.Strategy;

namespace Tank2021SharedContent.Facade
{
    public class TankConfigurer
    {

        public void ConfigureTankImage(Tank tank)
        {
            var tankStartingHealth = Helper.GetSpecificTankHp(tank);
            var tankHalfHealth = (int)(tankStartingHealth * 0.5);
            var tankQuarterHealth = (int)(tankStartingHealth * 0.25);
            var tankMinimalHealth = (int)(tankStartingHealth * 0.1);

            if (tank.Health <= tankMinimalHealth)
            {
                tank.SetCriticalyDamaged();
            }
            else if (tank.Health <= tankQuarterHealth)
            {
                tank.SetOnFire();
            }
            else if (tank.Health <= tankHalfHealth)
            {
                tank.SetSmoking();
            }
        }
        public void ConfigureMoveAlgorithm(Tank tank)
        {
            var tankStartingHealth = Helper.GetSpecificTankHp(tank);
            var tankHalfHealth = (int)(tankStartingHealth * 0.5);
            var tankQuarterHealth = (int)(tankStartingHealth * 0.25);
            var tankMinimalHealth = (int)(tankStartingHealth * 0.1);

            if (tank.Health <= tankMinimalHealth)
            {
                tank.SetMoveAlgorithm(new StopMovement());
            }
            else if (tank.Health <= tankQuarterHealth)
            {
                tank.SetMoveAlgorithm(new SlowMovement());
            }
            else if (tank.Health <= tankHalfHealth)
            {
                tank.SetMoveAlgorithm(new MediumMovement());
            }
        }


    }
    
}
