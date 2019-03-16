using Calculator.Common.Parser;

namespace Calculator.Common.Evaluator
{
    public class SyntaxThreeEvaluator
    {
        private readonly SyntaxNode _root;

        public SyntaxThreeEvaluator(SyntaxNode root)
        {
            _root = root;
        }

        public double Evaluate()
        {
            var visitor = new SyntaxThreeVisitor(_root);
            return _root.Accept(visitor);
        }
    }
}