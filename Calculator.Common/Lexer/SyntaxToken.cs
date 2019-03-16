namespace Calculator.Common.Lexer
{
    public class SyntaxToken
    {
        public SyntaxTokenKind Kind { get; }
        public int StartIndex { get; }
        public string Text { get; }

        public SyntaxToken(SyntaxTokenKind kind, int startIndex, string text)
        {
            Kind = kind;
            StartIndex = startIndex;
            Text = text;
        }
    }
}