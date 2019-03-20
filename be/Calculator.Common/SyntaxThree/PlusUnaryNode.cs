using Calculator.Common.Evaluator;

namespace Calculator.Common.SyntaxThree
{
    public class PlusUnaryNode : UnaryOperatorNode
    {
        public PlusUnaryNode(SyntaxNode operand) : base(operand)
        {
        }

        internal override double Accept(SyntaxThreeVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}