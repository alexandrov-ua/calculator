using Calculator.Common.Evaluator;

namespace Calculator.Common.SyntaxThree
{
    public abstract class SyntaxNode
    {
        internal abstract double Accept(SyntaxThreeVisitor visitor);
    }
}