using Calculator.Common.Parser;

namespace Calculator.Common.Evaluator
{
    public class EvaluatorResult
    {
        public bool IsSuccessful { get; }
        public double Result { get; }
        public DiagnosticsEntry[] Diagnostics { get; }

        public EvaluatorResult(bool isSuccessful, double result, DiagnosticsEntry[] diagnostics)
        {
            IsSuccessful = isSuccessful;
            Result = result;
            Diagnostics = diagnostics;
        }
    }
}