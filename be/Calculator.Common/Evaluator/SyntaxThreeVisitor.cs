using System;
using System.Collections.Generic;
using Calculator.Common.Parser;
using Calculator.Common.SyntaxThree;

namespace Calculator.Common.Evaluator
{
    internal class SyntaxThreeVisitor
    {
        private readonly List<DiagnosticsEntry> _diagnostics = new List<DiagnosticsEntry>();

        public IReadOnlyCollection<DiagnosticsEntry> Diagnostics => _diagnostics;

        internal double Visit(PowerBinaryNode node)
        {
            return Math.Pow(node.Left.Accept(this), node.Right.Accept(this));
        }

        public double Visit(NumberNode node)
        {
            return node.Value;
        }

        internal double Visit(ParenthesisNode parenthesisNode)
        {
            return parenthesisNode.Expression.Accept(this);
        }

        public double Visit(PlusBinaryNode node)
        {
            return node.Left.Accept(this) + node.Right.Accept(this);
        }

        internal double Visit(PlusUnaryNode node)
        {
            return node.Operand.Accept(this);
        }

        public double Visit(MinusBinaryNode node)
        {
            return node.Left.Accept(this) - node.Right.Accept(this);
        }

        internal double Visit(MinusUnaryNode node)
        {
            return node.Operand.Accept(this) * -1.0d;
        }

        public double Visit(MultiplyBinaryNode node)
        {
            return node.Left.Accept(this) * node.Right.Accept(this);
        }

        public double Visit(DivideBinaryNode node)
        {
            var left = node.Left.Accept(this);
            var right = node.Right.Accept(this);
            return left / right;
        }
    }
}