using System;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace Calculator.Common.Lexer
{
    public enum SyntaxTokenKind
    {
        [BinaryOperationPrecedence(1)]
        [SyntaxTokenGroup(SyntaxTokenGroup.Unary)]
        [SyntaxTokenGroup(SyntaxTokenGroup.Binary)]
        Plus,
        [BinaryOperationPrecedence(1)]
        [SyntaxTokenGroup(SyntaxTokenGroup.Unary)]
        [SyntaxTokenGroup(SyntaxTokenGroup.Binary)]
        Minus,
        [BinaryOperationPrecedence(2)]
        [SyntaxTokenGroup(SyntaxTokenGroup.Binary)]
        Star,
        [BinaryOperationPrecedence(2)]
        [SyntaxTokenGroup(SyntaxTokenGroup.Binary)]
        Slash,
        [BinaryOperationPrecedence(3)]
        [SyntaxTokenGroup(SyntaxTokenGroup.Binary)]
        Hat,
        OpenParenthesis,
        CloseParenthesis,
        Number,
        Identifier,
        Unknown,
        EndOfFile
    }

    public enum SyntaxTokenGroup
    {
        Binary,
        Unary,
    }


    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class SyntaxTokenGroupAttribute : Attribute
    {
        public SyntaxTokenGroup Group { get; }

        public SyntaxTokenGroupAttribute(SyntaxTokenGroup @group)
        {
            Group = @group;
        }
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class BinaryOperationPrecedenceAttribute : Attribute
    {
        public int Precedence { get; }

        public BinaryOperationPrecedenceAttribute(int precedence)
        {
            Precedence = precedence;
        }
    }
}