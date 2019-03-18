using Calculator.Common.Evaluator;

namespace Calculator.Api
{
    public class StringEvaluatorFactory
    {
        public static IStringEvaluator Create()
        {
            return new StringEvaluator();
        }
    }
}