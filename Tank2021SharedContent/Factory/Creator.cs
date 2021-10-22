using System;
using System.Collections.Generic;
using System.Text;
using Tank2021SharedContent.Abstract.Tanks;
using Tank2021SharedContent.Enums;

namespace Tank2021SharedContent.Factory
{
    public abstract class Creator
    {
        public abstract Tank CreateTank(TankType tankType);
    }
}
