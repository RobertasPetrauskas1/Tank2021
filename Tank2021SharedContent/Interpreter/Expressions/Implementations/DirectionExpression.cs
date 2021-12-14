using System;
using System.Collections.Generic;
using System.Text;
using Tank2021SharedContent.Enums;

namespace Tank2021SharedContent.Interpreter.Expressions.Implementations
{
    public class DirectionExpression : IExpression
    {
        private Direction _direction;
        private Context _context;
        public DirectionExpression(Direction direction, Context context)
        {
            _direction = direction;
            _context = context;
        }

        public void Interpret()
        {
            _context.CurrentOperation = new Operation() { Direction = _direction };
        }
    }
}
