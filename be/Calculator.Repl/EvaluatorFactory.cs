using Calculator.Common.Evaluator;
using Calculator.Dal;

namespace Calculator.Repl
{
    public static class EvaluatorFactory
    {
        private static readonly ILogStorage<EvaluatorLog> _logStorage = new FileLogStorage<EvaluatorLog>("log.txt");

        public static IStringEvaluator Create()
        {
            return new LoggingStringEvaluatorDecorator(new StringEvaluator(), _logStorage);
        }
    }
}