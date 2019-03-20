using System.Linq;
using Calculator.Common.Evaluator;

namespace Calculator.Api.Models
{
    public class EvaluatorOutputViewModel
    {
        public bool IsSuccessful { get; set; }
        public double Result { get; set; }
        public DiagnosticsEntryViewModel[] Diagnostics { get; set; }

        public static EvaluatorOutputViewModel FromModel(EvaluatorResult model)
        {
            return new EvaluatorOutputViewModel()
            {
                IsSuccessful = model.IsSuccessful,
                Result = model.Result,
                Diagnostics = model.Diagnostics?.Select(DiagnosticsEntryViewModel.FromModel).ToArray()
            };
        }
    }
}