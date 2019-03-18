using Calculator.Common.Parser;

namespace Calculator.Api.ViewModels
{
    public class DiagnosticsEntryViewModel
    {
        public DiagnosticSeverity Severity { get; set; }
        public DiagnosticKind Kind { get; set; }
        public TextSpan Span { get; set; }
        public object[] Parameters { get; set; }

        public static DiagnosticsEntryViewModel FromModel(DiagnosticsEntry model)
        {
            return new DiagnosticsEntryViewModel()
            {
                Kind = model.Kind,
                Severity = model.Severity,
                Span = model.Span,
                Parameters = model.Parameters
            };
        }
    }
}