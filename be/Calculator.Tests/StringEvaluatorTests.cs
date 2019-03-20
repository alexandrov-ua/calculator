using Calculator.Common.Evaluator;
using Calculator.Common.Lexer;
using Calculator.Common.Parser;
using FluentAssertions;
using Xunit;

namespace Calculator.Tests
{
    public class StringEvaluatorTests
    {
        private static double EvaluateSuccess(string input)
        {
            var evaluator = new StringEvaluator();
            var result =  evaluator.Evaluate(input);
            result.IsSuccessful.Should().BeTrue();
            return result.Result;
        }

        [Fact]
        public void SyntaxThreeEvaluator_ShouldEvaluate_BinaryOperators_AccordingToPrecedence()
        {
            var result = EvaluateSuccess("2+3*4");
            result.Should().Be(14);
        }

        [Fact]
        public void SyntaxThreeEvaluator_ShouldEvaluate_BinaryOperators_AccordingToPrecedence2()
        {
            var result = EvaluateSuccess("2*3+4");
            result.Should().Be(10);
        }

        [Fact]
        public void SyntaxThreeEvaluator_ShouldEvaluate_UnaryOperators()
        {
            var result = EvaluateSuccess("2*-4");
            result.Should().Be(-8);
        }

        [Fact]
        public void SyntaxThreeEvaluator_ShouldEvaluate_UnaryOperators2()
        {
            var result = EvaluateSuccess("-2*4");
            result.Should().Be(-8);
        }

        [Fact]
        public void SyntaxThreeEvaluator_ShouldEvaluate_Parenthesis()
        {
            var result = EvaluateSuccess("(2+3)*4");
            result.Should().Be(20);
        }

        [Fact]
        public void SyntaxThreeEvaluator_ShouldEvaluate_AndReturnDoubleResult()
        {
            var result = EvaluateSuccess("3/2");
            result.Should().Be(1.5);
        }

        [Fact]
        public void SyntaxThreeEvaluator_ShouldEvaluate_Doubles()
        {
            var result = EvaluateSuccess("3.2/2.0");
            result.Should().Be(1.6);
        }

        [Fact]
        public void SyntaxThreeEvaluator_ShouldEvaluate_Power()
        {
            var result = EvaluateSuccess("2*3^4");
            result.Should().Be(162);
        }

        [Fact]
        public void SyntaxThreeEvaluator_ShouldEvaluate_Power2()
        {
            var result = EvaluateSuccess("1+2^3*4+5");
            result.Should().Be(38);
        }
    }
}