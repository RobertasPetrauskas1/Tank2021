using System;
using System.Collections.Generic;
using System.Text;
using Tank2021SharedContent.Interpreter;

namespace Tank2021SharedContent.HubRezults
{
    public class ContextExecutor
    {
        private readonly Context _context;
        public ContextExecutor(Context context)
        {
            _context = context;
        }

        public void Execute()
        {
            foreach (var operation in _context)
            {
                switch (operation.Command)
                {
                    case Interpreter.Commands.HealthRestore:
                        _context.Player.Tank.Health = Helper.GetSpecificTankHp(_context.Player.Tank);
                        break;
                    case Interpreter.Commands.Shoot:
                        _context.Player.Tank.Shoot();
                        break;
                    case Interpreter.Commands.Move:
                        _context.Player.Tank.Move(operation.Direction.Value);
                        break;
                }
            }
        }

        public void Undo()
        {
            foreach (var operation in _context)
            {
                switch (operation.Command)
                {
                    case Interpreter.Commands.HealthRestore:
                        _context.Player.Tank.Health = operation.Health.Value;
                        break;
                    case Interpreter.Commands.Shoot:
                        var bulletCount = _context.Player.Tank.Gun.Bullets.Count;
                        if (bulletCount > 0)
                            _context.Player.Tank.Gun.Bullets.RemoveAt(bulletCount - 1);

                        break;
                    case Interpreter.Commands.Move:
                        _context.Player.Tank.Move(Helper.GetOppositeDirection(operation.Direction.Value));
                        break;
                }
            }
        }
    }
}
