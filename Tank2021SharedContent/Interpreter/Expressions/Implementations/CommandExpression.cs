using System;
using System.Collections.Generic;
using System.Text;

namespace Tank2021SharedContent.Interpreter.Expressions.Implementations
{
    public class CommandExpression : IExpression
    {
        private IList<IExpression> _expressions;
        private Context _context;

        public CommandExpression(List<IExpression> expressions, Context context)
        {
            _expressions = expressions;
            _context = context;
        }

        public void Interpret()
        {
            foreach (var expression in _expressions)
                expression.Interpret();

            _context.Execute();
        }
    }
}
