using System.Collections.Generic;
using System.Linq;
using Calculator.Common.Lexer;
using Calculator.Common.Parser;
using DeepEqual.Syntax;
using FluentAssertions;
using Xunit;

namespace Calculator.Tests
{
    public class SyntaxTokenParserNegativeTests
    {
        private static DiagnosticsEntry[] ParseFailed(string input)
        {
            var parser = new SyntaxTokenParser(new SyntaxTokenEnumerable(input));
            var parserResult = parser.Parse();
            parserResult.IsSuccessful.Should().BeFalse();
            return parserResult.Diagnostics;
        }

        [Fact]
        public void Parser_ShouldReturnAnError_WhenMissingParenthesis()
        {
            var error = ParseFailed("(2+3").First();

            error.Parameters[0].Should().Be(SyntaxTokenKind.CloseParenthesis);
            error.Parameters[1].Should().Be(SyntaxTokenKind.EndOfFile);
            error.Span.Start.Should().Be(4);
        }

        [Fact]
        public void Parser_ShouldReturnAnError_WithWrongParenthesis()
        {
            var error = ParseFailed(")2+3(").First();

            error.Parameters[0].Should().Be(SyntaxTokenKind.Number);
            error.Parameters[1].Should().Be(SyntaxTokenKind.CloseParenthesis);
            error.Span.Start.Should().Be(0);
        }

        [Fact]
        public void Parser_ShouldReturnAnError_WhenOperatorIsMissing()
        {
            var error = ParseFailed("2 3").First();

            error.Parameters[0].Should().Be(SyntaxTokenKind.EndOfFile);
            error.Parameters[1].Should().Be(SyntaxTokenKind.Number);
            error.Span.Start.Should().Be(2);
        }

        [Fact]
        public void Parser_ShouldReturnAnError_WhenIdentifierInsteadOfOperator()
        {
            var error = ParseFailed("2a3").First();

            error.Parameters[0].Should().Be(SyntaxTokenKind.EndOfFile);
            error.Parameters[1].Should().Be(SyntaxTokenKind.Identifier);
            error.Span.Start.Should().Be(1);
        }
    }
}