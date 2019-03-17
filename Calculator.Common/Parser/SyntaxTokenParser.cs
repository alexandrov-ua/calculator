﻿using System;
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

        private SyntaxNode ParseBinaryExpression(int parentPrecedence = 0, bool afterUnary = false)
        {
            SyntaxNode left;
            if (_tokens.Current.Kind.IsInTokenGroup(SyntaxTokenGroup.Unary))
            {
                var operatorToken = _tokens.GetAndMoveNext();
                var operand = ParseBinaryExpression(afterUnary:true);
                left = UnaryOperatorNode.Create(operatorToken, operand);
            }
            else
            {
                left = ParseParenthesisOrValue();
            }
            while (true)
            {
                var precedence = _tokens.Current.Kind.GetBinaryOperationPrecedence();
                if (!_tokens.Current.Kind.IsInTokenGroup(SyntaxTokenGroup.Binary) || precedence <= parentPrecedence || afterUnary)
                    break;
                var op = _tokens.GetAndMoveNext();
                var right = ParseBinaryExpression(precedence);
                left = BinaryOperationNode.Create(left, op, right);
            }

            return left;
        }

        private SyntaxNode ParseParenthesisOrValue()
        {
            switch (_tokens.Current.Kind)
            {
                case SyntaxTokenKind.OpenParenthesis:
                    return ParseParenthesis();
                default:
                    return ParseNumberLiteral();
            }
        }

        private SyntaxNode ParseParenthesis()
        {
            var left = MatchToken(SyntaxTokenKind.OpenParenthesis);
            var expression = ParseBinaryExpression();
            var right = MatchToken(SyntaxTokenKind.CloseParenthesis);
            return new ParenthesisNode(expression); 
        }

        private SyntaxNode ParseNumberLiteral()
        {
            var literal = MatchToken(SyntaxTokenKind.Number);
            var value = string.IsNullOrEmpty(literal.Text) ? 0.0d : double.Parse(literal.Text);
            return new NumberNode(value);
        }
    }
}