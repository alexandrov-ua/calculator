using System;
using System.Collections.Generic;
using System.Linq;
using Calculator.Common.Evaluator;
using Calculator.Common.Lexer;

namespace Calculator.Common.Parser
{
    public class Parser
    {
        private readonly IEnumerator<SyntaxToken> _tokens;
        private readonly List<DiagnosticsEntry> _diagnostics = new List<DiagnosticsEntry>();
        public Parser(IEnumerable<SyntaxToken> tokens)
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

    public abstract class SyntaxNode
    {
        internal abstract double Accept(SyntaxThreeVisitor visitor);
    }

    public abstract class BinaryOperationNode : SyntaxNode
    {
        public BinaryOperationNode(SyntaxNode left, SyntaxNode right)
        {
            Left = left;
            Right = right;
        }

        public SyntaxNode Left { get; }
        public SyntaxNode Right { get; }
    }

    public class PlusBinaryNode : BinaryOperationNode
    {
        public PlusBinaryNode(SyntaxNode left, SyntaxNode right) : base(left, right)
        {
        }

        internal override double Accept(SyntaxThreeVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }

    public class MinusBinaryNode : BinaryOperationNode
    {
        public MinusBinaryNode(SyntaxNode left, SyntaxNode right) : base(left, right)
        {
        }

        internal override double Accept(SyntaxThreeVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }

    public class MultiplyBinaryNode : BinaryOperationNode
    {
        public MultiplyBinaryNode(SyntaxNode left, SyntaxNode right) : base(left, right)
        {
        }

        internal override double Accept(SyntaxThreeVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }

    public class DivideBinaryNode : BinaryOperationNode
    {
        public DivideBinaryNode(SyntaxNode left, SyntaxNode right) : base(left, right)
        {
        }

        internal override double Accept(SyntaxThreeVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }

    public class NumberNode : SyntaxNode
    {
        public NumberNode(double value)
        {
            Value = value;
        }

        public double Value { get; }

        internal override double Accept(SyntaxThreeVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }

    public class ParserResult
    {
        public bool IsSuccessful { get; set; }
        public SyntaxNode Root { get; set; }
        public List<DiagnosticsEntry> Diagnostics { get; set; }
    }

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

    public static class IEnumeratorExtensions
    {
        public static T GetAndMoveNext<T>(this IEnumerator<T> enumerator)
        {
            var current = enumerator.Current;
            enumerator.MoveNext();
            return current;
        }
    }
}