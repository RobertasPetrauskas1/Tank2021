using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank2021SharedContent.Enums;

namespace Tank2021Client.Mediator.Colleagues
{
    public class TankStatus
    {
        public PlayerType PlayerType { get; set; }
        public int? ArmorHits { get; set; }
        public int? Health { get; set; }
    }
}
