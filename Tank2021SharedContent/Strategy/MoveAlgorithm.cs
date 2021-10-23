using System;
using System.Collections.Generic;
using System.Text;
using Tank2021SharedContent.Abstract.Tanks;

namespace Tank2021SharedContent.Strategy
{
    public abstract class MoveAlgorithm
    {
        public abstract void MoveUp(Tank tank);
        public abstract void MoveDown(Tank tank);
        public abstract void MoveLeft(Tank tank);
        public abstract void MoveRight(Tank tank);
    }
}
