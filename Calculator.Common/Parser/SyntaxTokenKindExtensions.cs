using Calculator.Common.Lexer;

namespace Calculator.Common.Parser
{
    public static class SyntaxTokenKindExtensions
    {
        public static int GetPrecedence(this SyntaxTokenKind kind)
        {
            switch (kind)
            {
                case SyntaxTokenKind.Star:
                case SyntaxTokenKind.Slash:
                    return 2;
                case SyntaxTokenKind.Plus:
                case SyntaxTokenKind.Minus:
                    return 1;
                default:
                    return 0;
            }
        }
    }
}