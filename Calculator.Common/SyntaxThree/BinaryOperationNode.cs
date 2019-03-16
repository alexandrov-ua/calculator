namespace Calculator.Common.SyntaxThree
{
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
}