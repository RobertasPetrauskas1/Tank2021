using System;
using System.Collections.Generic;
using System.Text;
using Tank2021SharedContent.HubRezults;

namespace Tank2021SharedContent.Interpreter.Expressions.Implementations
{
    public class CommandExpression : IExpression
    {
        private IList<IExpression> _expressions;
        private ContextExecutor _contextExecutor;

        public CommandExpression(List<IExpression> expressions, ContextExecutor contextExecutor)
        {
            _expressions = expressions;
            _contextExecutor = contextExecutor;
        }

        public void Interpret()
        {
            foreach (var expression in _expressions)
                expression.Interpret();

            _contextExecutor.Execute();
        }
    }
}
