using System;
using Calculator.Common.Parser;
using Calculator.Common.SyntaxThree;

namespace Calculator.Common.Evaluator
{
    internal class SyntaxThreeVisitor
    {
        private readonly SyntaxNode _root;

        public SyntaxThreeVisitor(SyntaxNode root)
        {
            _root = root;
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
            return node.Left.Accept(this) / node.Right.Accept(this);
        }
    }
}