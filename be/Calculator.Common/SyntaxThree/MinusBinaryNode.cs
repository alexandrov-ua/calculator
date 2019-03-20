using Calculator.Common.Evaluator;

namespace Calculator.Common.SyntaxThree
{
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
}