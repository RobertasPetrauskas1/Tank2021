using System;
using System.Collections.Generic;
using System.Text;

namespace Tank2021SharedContent.Interpreter.Expressions.Implementations
{
    public class ShootExpression : IExpression
    {
        private Context _context;
        public ShootExpression(Context context)
        {
            _context = context;
        }
        public void Interpret()
        {
            _context.Operations.Add(new Operation() { Command = Commands.Shoot });
        }
    }
}
