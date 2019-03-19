using Calculator.Common.Evaluator;
using Calculator.Dal;

namespace Calculator.Api.ViewModels
{
    public class EvaluatorLogViewModel
    {
        public string Input { get; set; }
        public EvaluatorOutputViewModel Output { get; set; }

        public static EvaluatorLogViewModel FromModel(EvaluatorLog evaluatorLog)
        {
            return new EvaluatorLogViewModel()
            {
                Input = evaluatorLog.Input,
                Output =  EvaluatorOutputViewModel.FromModel(evaluatorLog.Output)
            };
        }
    }
}