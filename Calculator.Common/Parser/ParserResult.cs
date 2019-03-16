using System.Collections.Generic;
using Calculator.Common.SyntaxThree;

namespace Calculator.Common.Parser
{
    public class ParserResult
    {
        public bool IsSuccessful { get; set; }
        public SyntaxNode Root { get; set; }
        public List<DiagnosticsEntry> Diagnostics { get; set; }
    }
}