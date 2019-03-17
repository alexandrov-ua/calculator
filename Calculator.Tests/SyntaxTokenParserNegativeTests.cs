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

            error.Expected.Should().Be(SyntaxTokenKind.CloseParenthesis);
            error.Index.Should().Be(4);
            error.Found.Should().Be(SyntaxTokenKind.EndOfFile);
        }

        [Fact]
        public void Parser_ShouldReturnAnError_WithWrongParenthesis()
        {
            var error = ParseFailed(")2+3(").First();

            error.Expected.Should().Be(SyntaxTokenKind.Number);
            error.Index.Should().Be(0);
            error.Found.Should().Be(SyntaxTokenKind.CloseParenthesis);
        }

        [Fact]
        public void Parser_ShouldReturnAnError_WhenOperatorIsMissing()
        {
            var error = ParseFailed("2 3").First();

            error.Expected.Should().Be(SyntaxTokenKind.EndOfFile);
            error.Found.Should().Be(SyntaxTokenKind.Number);
            error.Index.Should().Be(2);
        }

        [Fact]
        public void Parser_ShouldReturnAnError_WhenIdentifierInsteadOfOperator()
        {
            var error = ParseFailed("2a3").First();

            error.Expected.Should().Be(SyntaxTokenKind.EndOfFile);
            error.Found.Should().Be(SyntaxTokenKind.Identifier);
            error.Index.Should().Be(1);
        }
    }
}