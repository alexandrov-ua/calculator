using Calculator.Common.Lexer;
using Calculator.Common.Parser;
using FluentAssertions;
using Xunit;

namespace Calculator.Tests
{
    public class SyntaxTokenKindExtensionsTests
    {
        [Fact]
        public void GetBinaryOperationPrecedence_Should_ReturnPrecedence()
        {
            SyntaxTokenKind.Star.GetBinaryOperationPrecedence().Should().Be(2);
        }

        [Fact]
        public void GetBinaryOperationPrecedence_Should_ReturnPrecedence2()
        {
            SyntaxTokenKind.Plus.GetBinaryOperationPrecedence().Should().Be(1);
        }

        [Fact]
        public void GetBinaryOperationPrecedence_Should_Return0_IfThereIsNoAttribute()
        {
            SyntaxTokenKind.CloseParenthesis.GetBinaryOperationPrecedence().Should().Be(0);
        }

        [Fact]
        public void IsInSyntaxGroup_Should_ReturnTrue_IfGroupAttributeIsExists()
        {
            SyntaxTokenKind.Plus.IsInTokenGroup(SyntaxTokenGroup.Binary).Should().BeTrue();
        }

        [Fact]
        public void IsInSyntaxGroup_Should_ReturnTrue_IfGroupAttributeIsExists2()
        {
            SyntaxTokenKind.Plus.IsInTokenGroup(SyntaxTokenGroup.Unary).Should().BeTrue();
        }

        [Fact]
        public void IsInSyntaxGroup_Should_ReturnFalse_IfGroupAttributeDoesNotExists()
        {
            SyntaxTokenKind.CloseParenthesis.IsInTokenGroup(SyntaxTokenGroup.Binary).Should().BeFalse();
        }
    }
}