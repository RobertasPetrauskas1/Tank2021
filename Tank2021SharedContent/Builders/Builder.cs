using System;
using System.Collections.Generic;
using System.Text;
using Tank2021SharedContent.Abstract.Tanks;

namespace Tank2021SharedContent.Builders
{
    public abstract class Builder
    {
        public abstract void BuildTankSpeed();
        public abstract void BuildTankHealth();
        public abstract void BuildTankPhoto();
        public abstract Tank GetResult();
    }
}
