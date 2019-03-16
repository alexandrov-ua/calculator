using Calculator.Common.Parser;

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

        public double Visit(PlusBinaryNode node)
        {
            return node.Left.Accept(this) + node.Right.Accept(this);
        }

        public double Visit(MinusBinaryNode node)
        {
            return node.Left.Accept(this) - node.Right.Accept(this);
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