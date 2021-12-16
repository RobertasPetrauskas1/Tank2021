using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Tank2021SharedContent.Enums;

namespace Tank2021SharedContent.Interpreter
{
    public enum Commands
    {
        HealthRestore,
        Shoot,
        Move
    }

    public class Operation
    {
        public Commands Command { get; set; }
        public Direction? Direction { get; set; }
        public int? Health { get; set; }
    }

    public class Context : IEnumerable<Operation>
    {
        public IList<Operation> Operations { get; set; } = new List<Operation>();
        public Operation CurrentOperation { get; set; }
        public Player Player { get; set; }

        public Context(Player player)
        {
            Player = player;
        }

        public Momento CreateMomento() => new Momento(Operations, Player);
        public bool RestoreMomento(Momento momento) => momento.RestoreState(this);

        public IEnumerator<Operation> GetEnumerator()
        {
            return Operations.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public class Momento
        {
            private IList<Operation> Operations { get; set; } = new List<Operation>();
            private Player Player { get; set; }

            public Momento(IList<Operation> operations, Player player)
            {
                Operations = operations;
                Player = player;
            }

            internal bool RestoreState(Context context)
            {
                if(context.Player.PlayerType == Player.PlayerType)
                {
                    context.Operations = Operations;
                    context.Player = Player;

                    return true;
                }

                return false;
            }
        }
    }
}
