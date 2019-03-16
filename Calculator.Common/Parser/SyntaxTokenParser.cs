using System;
using System.Collections.Generic;
using System.Linq;
using Calculator.Common.Lexer;
using Calculator.Common.SyntaxThree;
using Calculator.Common.Tools;

namespace Calculator.Common.Parser
{
    public class SyntaxTokenParser
    {
        private readonly IEnumerator<SyntaxToken> _tokens;
        private readonly List<DiagnosticsEntry> _diagnostics = new List<DiagnosticsEntry>();
        public SyntaxTokenParser(IEnumerable<SyntaxToken> tokens)
        {
            _tokens = tokens.GetEnumerator();
        }

        public ParserResult Parse()
        {
            _tokens.MoveNext();
            return new ParserResult()
            {
                IsSuccessful = !_diagnostics.Any(),
                Root = ParseBinaryExpression(),
                Diagnostics = _diagnostics
            };
        }

        private SyntaxToken MatchToken(SyntaxTokenKind kind)
        {
            if (_tokens.Current.Kind == kind)
            {
                var current = _tokens.Current;
                _tokens.MoveNext();
                return current;
            }

            _diagnostics.Add(new DiagnosticsEntry(_tokens.Current.StartIndex, _tokens.Current.Text, kind, _tokens.Current.Kind));
            return new SyntaxToken(kind, 0, null);
        }

        private SyntaxNode ParseBinaryExpression(int parentPrecedence = 0)
        {
            var left = ParseNumberLiteral();
            while (true)
            {
                var precedence = _tokens.Current.Kind.GetPrecedence();
                if (precedence == 0 || precedence <= parentPrecedence)
                    break;
                var op = _tokens.GetAndMoveNext();
                var right = ParseBinaryExpression(precedence);
                left = CreateBinaryOperator(left, op, right);
            }

            return left;
        }

        private SyntaxNode CreateBinaryOperator(SyntaxNode left, SyntaxToken op, SyntaxNode right)
        {
            switch (op.Kind)
            {
                case SyntaxTokenKind.Plus:
                    return new PlusBinaryNode(left, right);
                case SyntaxTokenKind.Minus:
                    return new MinusBinaryNode(left, right);
                case SyntaxTokenKind.Star:
                    return new MultiplyBinaryNode(left, right);
                case SyntaxTokenKind.Slash:
                    return new DivideBinaryNode(left, right);
                default:
                    throw new NotImplementedException();
            }
        }

        private SyntaxNode ParseNumberLiteral()
        {
            var literal = MatchToken(SyntaxTokenKind.Number);
            var value = string.IsNullOrEmpty(literal.Text) ? 0.0d : double.Parse(literal.Text);
            return new NumberNode(value);
        }
    }
}