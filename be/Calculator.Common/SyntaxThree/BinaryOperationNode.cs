using System;
using Calculator.Common.Lexer;

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

        public static SyntaxNode Create(SyntaxNode left, SyntaxToken op, SyntaxNode right)
        {
            switch (op.Kind)
            {
                case SyntaxTokenKind.Plus:
                    return new PlusBinaryNode(left, right);
                case SyntaxTokenKind.Minus:
                    return new MinusBinaryNode(left, right);
                case SyntaxTokenKind.Star:
                    return new MultiplyBinaryNode(left, right);
                case SyntaxTokenKind.Slash:
                    return new DivideBinaryNode(left, right);
                case SyntaxTokenKind.Hat:
                    return new PowerBinaryNode(left, right);
                default:
                    throw new NotImplementedException();
            }
        }
    }
}