using System;
using System.Collections.Generic;
using System.Text;

namespace Tank2021SharedContent.Interpreter.Expressions.Implementations
{
    public class MoveExpression : IExpression
    {
        private IExpression _directionExpression;
        private Context _context;
        public MoveExpression(DirectionExpression directionExpression, Context context)
        {
            _directionExpression = directionExpression;
            _context = context;
        }

        public void Interpret()
        {
            _directionExpression.Interpret();
            _context.CurrentOperation.Command = Commands.Move;

            _context.Operations.Add(_context.CurrentOperation);
        }
    }
}
