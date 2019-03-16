using Calculator.Common.Evaluator;

namespace Calculator.Common.SyntaxThree
{
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
}