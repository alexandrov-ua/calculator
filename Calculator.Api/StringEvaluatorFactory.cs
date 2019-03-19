using Calculator.Common.Evaluator;
using Calculator.Dal;

namespace Calculator.Api
{
    public class StringEvaluatorFactory
    {
        public static IStringEvaluator Create()
        {
            return  new LoggingStringEvaluatorDecorator(new StringEvaluator(), LogSorageFactory.Create());
        }
    }
}