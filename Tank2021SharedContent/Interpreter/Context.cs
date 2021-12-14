using System;
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

    public class Context
    {
        public IList<Operation> Operations { get; set; } = new List<Operation>();
        public Operation CurrentOperation { get; set; }
        public Player Player { get; set; }

        public Context(Player player)
        {
            Player = player;
        }

        public void Execute()
        {
            foreach (var operation in Operations)
            {
                switch (operation.Command)
                {
                    case Interpreter.Commands.HealthRestore:
                        Player.Tank.Health = Helper.GetSpecificTankHp(Player.Tank);
                        break;
                    case Interpreter.Commands.Shoot:
                        Player.Tank.Shoot();
                        break;
                    case Interpreter.Commands.Move:
                        Player.Tank.Move(operation.Direction.Value);
                        break;
                }
            }
        }

        public void Undo()
        {
            foreach(var operation in Operations)
            {
                switch (operation.Command)
                {
                    case Interpreter.Commands.HealthRestore:
                        Player.Tank.Health = operation.Health.Value;
                        break;
                    case Interpreter.Commands.Shoot:
                        var bulletCount = Player.Tank.Gun.Bullets.Count;
                        if (bulletCount > 0)
                            Player.Tank.Gun.Bullets.RemoveAt(bulletCount - 1);

                        break;
                    case Interpreter.Commands.Move:
                        Player.Tank.Move(Helper.GetOppositeDirection(operation.Direction.Value));
                        break;
                }
            }
        }

        public Momento CreateMomento() => new Momento(Operations, Player);
        public bool RestoreMomento(Momento momento) => momento.RestoreState(this);

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
