using System.Linq;
using Calculator.Common.Parser;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Calculator.Api.ViewModels
{
    public class DiagnosticsEntryViewModel
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public DiagnosticSeverity Severity { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public DiagnosticKind Kind { get; set; }
        public TextSpan Span { get; set; }
        public string[] Parameters { get; set; }

        public static DiagnosticsEntryViewModel FromModel(DiagnosticsEntry model)
        {
            return new DiagnosticsEntryViewModel()
            {
                Kind = model.Kind,
                Severity = model.Severity,
                Span = model.Span,
                Parameters = model.Parameters.Select(t=>t.ToString()).ToArray()
            };
        }
    }
}