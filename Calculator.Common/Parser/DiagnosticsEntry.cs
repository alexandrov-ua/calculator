using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using Calculator.Common.Lexer;

namespace Calculator.Common.Parser
{
    public class DiagnosticsEntry
    {
        public DiagnosticSeverity Severity { get; }
        public DiagnosticKind Kind { get;  }
        public TextSpan Span { get;  }
        public object[] Parameters { get; }

        public DiagnosticsEntry(DiagnosticSeverity severity, DiagnosticKind kind, TextSpan span, object[] parameters)
        {
            Severity = severity;
            Kind = kind;
            Span = span;
            Parameters = parameters;
        }

        //public int Index { get; }
        //public string Text { get; }
        //public SyntaxTokenKind Expected { get; }
        //public SyntaxTokenKind Found { get; }

        //public DiagnosticsEntry(int index, string text, SyntaxTokenKind expected, SyntaxTokenKind found)
        //{
        //    Index = index;
        //    Text = text;
        //    Expected = expected;
        //    Found = found;
        //}
    }

    public class DiagnosticsBag : IEnumerable<DiagnosticsEntry>
    {
        private readonly List<DiagnosticsEntry> _list;

        public DiagnosticsBag()
        {
            _list = new List<DiagnosticsEntry>();
        }

        public IEnumerator<DiagnosticsEntry> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void ReportError(DiagnosticKind kind, int startIndex, int length, params object[] parameters)
        {
            _list.Add(new DiagnosticsEntry(DiagnosticSeverity.Error, kind, new TextSpan(startIndex, length), parameters));
        }
    }

    public enum DiagnosticKind
    {
        //params: 1) expected 2) found
        UnexpectedToken,
    }

    public enum DiagnosticSeverity
    {
        Error,
        Warning
    }

    public struct TextSpan
    {
        public TextSpan(int start, int length)
        {
            Start = start;
            Length = length;
        }

        public int Start { get; }
        public int Length { get; }
        public int End => Start + Length;

        public override string ToString() => $"{Start}..{End}";
    }
}