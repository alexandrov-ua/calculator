using System;
using System.Linq;
using Calculator.Common;
using Calculator.Common.Lexer;
using FluentAssertions;
using Xunit;

namespace Calculator.Tests
{
    public class SyntaxTokenEnumerableTests
    {
        [Fact]
        public void SyntaxTokenEnumerable_ShouldByAbleToParse_Operations()
        {
            new SyntaxTokenEnumerable(" + -/ *")
                .Select(t => (t.Kind, t.Text, t.StartIndex))
                .Should()
                .BeEquivalentTo(new[]
                {
                    (SyntaxTokenKind.Plus, "+", 1),
                    (SyntaxTokenKind.Minus, "-", 3),
                    (SyntaxTokenKind.Slash, "/", 4),
                    (SyntaxTokenKind.Star, "*", 6),
                    (SyntaxTokenKind.EndOfFile, "", 7),
                });
        }

        [Fact]
        public void SyntaxTokenEnumerable_ShouldByAbleToParse_Parenthesis()
        {
            new SyntaxTokenEnumerable("( ) (")
                .Select(t => (t.Kind, t.Text, t.StartIndex))
                .Should()
                .BeEquivalentTo(new[]
                {
                    (SyntaxTokenKind.OpenParenthesis, "(", 0),
                    (SyntaxTokenKind.CloseParenthesis, ")", 2),
                    (SyntaxTokenKind.OpenParenthesis, "(", 4),
                    (SyntaxTokenKind.EndOfFile, "", 5),
                });
        }

        [Fact]
        public void SyntaxTokenEnumerable_ShouldByAbleToParse_Numbers()
        {
            new SyntaxTokenEnumerable("12+3.0-7.")
                .Select(t => (t.Kind, t.Text, t.StartIndex))
                .Should()
                .BeEquivalentTo(new[]
                {
                    (SyntaxTokenKind.Number, "12", 0),
                    (SyntaxTokenKind.Plus, "+", 2),
                    (SyntaxTokenKind.Number, "3.0", 3),
                    (SyntaxTokenKind.Minus, "-", 6),
                    (SyntaxTokenKind.Number, "7.", 7),
                    (SyntaxTokenKind.EndOfFile, "", 9),
                });
        }

        [Fact]
        public void SyntaxTokenEnumerable_ShouldByAbleToParse_Identifier()
        {
            new SyntaxTokenEnumerable("sin(0.5)")
                .Select(t => (t.Kind, t.Text, t.StartIndex))
                .Should()
                .BeEquivalentTo(new[]
                {
                    (SyntaxTokenKind.Identifier, "sin", 0),
                    (SyntaxTokenKind.OpenParenthesis, "(", 3),
                    (SyntaxTokenKind.Number, "0.5", 4),
                    (SyntaxTokenKind.CloseParenthesis, ")", 7),
                    (SyntaxTokenKind.EndOfFile, "", 8),
                });
        }

        [Fact]
        public void SyntaxTokenEnumerable_ShouldByAbleToParse_Identifier2()
        {
            new SyntaxTokenEnumerable("log10(0.5)")
                .Select(t => (t.Kind, t.Text, t.StartIndex))
                .Should()
                .BeEquivalentTo(new[]
                {
                    (SyntaxTokenKind.Identifier, "log10", 0),
                    (SyntaxTokenKind.OpenParenthesis, "(", 5),
                    (SyntaxTokenKind.Number, "0.5", 6),
                    (SyntaxTokenKind.CloseParenthesis, ")", 9),
                    (SyntaxTokenKind.EndOfFile, "", 10),
                });
        }

        [Fact]
        public void SyntaxTokenEnumerable_ShouldByAbleToHandle_UnknownTokens()
        {
            new SyntaxTokenEnumerable("!2qwe#")
                .Select(t => (t.Kind, t.Text, t.StartIndex))
                .Should()
                .BeEquivalentTo(new[]
                {
                    (SyntaxTokenKind.Unknown, "!", 0),
                    (SyntaxTokenKind.Number, "2", 1),
                    (SyntaxTokenKind.Identifier, "qwe", 2),
                    (SyntaxTokenKind.Unknown, "#", 5),
                    (SyntaxTokenKind.EndOfFile, "", 6),
                });
        }

        [Fact]
        public void SyntaxTokenEnumerable_ShouldByAbleToHandle_EmptyString()
        {
            new SyntaxTokenEnumerable("")
                .Select(t => (t.Kind, t.Text, t.StartIndex))
                .Should()
                .BeEquivalentTo(new []
                {
                    (SyntaxTokenKind.EndOfFile, "", 0),
                });
        }

        [Fact]
        public void SyntaxTokenEnumerable_ShouldByAbleToHandle_WhiteSpaces()
        {
            new SyntaxTokenEnumerable(" \t\n")
                .Select(t => (t.Kind, t.Text, t.StartIndex))
                .Should()
                .BeEquivalentTo(new[]
                {
                    (SyntaxTokenKind.EndOfFile, "", 3),
                });
        }
    }
}
