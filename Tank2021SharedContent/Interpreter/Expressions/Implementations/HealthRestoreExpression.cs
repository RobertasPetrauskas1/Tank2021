using System;
using System.Collections.Generic;
using System.Text;

namespace Tank2021SharedContent.Interpreter.Expressions.Implementations
{
    public class HealthRestoreExpression : IExpression
    {
        private Context _context;
        private int _currentHealth;
        public HealthRestoreExpression(Context context, int currentHealth)
        {
            _context = context;
            _currentHealth = currentHealth;
        }

        public void Interpret()
        {
            _context.Operations.Add(new Operation() { Command = Commands.HealthRestore, Health = _currentHealth});
        }
    }
}
