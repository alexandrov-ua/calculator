using Calculator.Common.Evaluator;

namespace Calculator.Dal
{
    public class LoggingStringEvaluatorDecorator : IStringEvaluator
    {
        private readonly IStringEvaluator _evaluator;
        private readonly ILogStorage<EvaluatorLog> _logStorage;

        public LoggingStringEvaluatorDecorator(IStringEvaluator evaluator, ILogStorage<EvaluatorLog> logStorage)
        {
            _evaluator = evaluator;
            _logStorage = logStorage;
        }

        public EvaluatorResult Evaluate(string input)
        {
            var result = _evaluator.Evaluate(input);
            _logStorage.Log(new EvaluatorLog()
            {
                Input = input,
                Output = result
            });
            return result;
        }
    }
}