using Calculator.Common.Lexer;

namespace Calculator.Common.Parser
{
    public class DiagnosticsEntry
    {
        public int Index { get; }
        public string Text { get; }
        public SyntaxTokenKind Expected { get; }
        public SyntaxTokenKind Found { get; }

        public DiagnosticsEntry(int index, string text, SyntaxTokenKind expected, SyntaxTokenKind found)
        {
            Index = index;
            Text = text;
            Expected = expected;
            Found = found;
        }
    }
}