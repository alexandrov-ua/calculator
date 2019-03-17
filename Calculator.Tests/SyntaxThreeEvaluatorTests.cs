using Calculator.Common.Evaluator;
using Calculator.Common.Lexer;
using Calculator.Common.Parser;
using FluentAssertions;
using Xunit;

namespace Calculator.Tests
{
    public class SyntaxThreeEvaluatorTests
    {
        private static double Evaluate(string input)
        {
            var parser = new SyntaxTokenParser(new SyntaxTokenEnumerable(input));
            var parserResult = parser.Parse();
            var evaluator = new SyntaxThreeEvaluator(parserResult.Root);
            return evaluator.Evaluate();
        }

        [Fact]
        public void SyntaxThreeEvaluator_ShouldEvaluate_BinaryOperators_AccordingToPrecedence()
        {
            var result = Evaluate("2+3*4");
            result.Should().Be(14);
        }

        [Fact]
        public void SyntaxThreeEvaluator_ShouldEvaluate_BinaryOperators_AccordingToPrecedence2()
        {
            var result = Evaluate("2*3+4");
            result.Should().Be(10);
        }

        [Fact]
        public void SyntaxThreeEvaluator_ShouldEvaluate_UnaryOperators()
        {
            var result = Evaluate("2*-4");
            result.Should().Be(-8);
        }

        [Fact]
        public void SyntaxThreeEvaluator_ShouldEvaluate_UnaryOperators2()
        {
            var result = Evaluate("-2*4");
            result.Should().Be(-8);
        }

        [Fact]
        public void SyntaxThreeEvaluator_ShouldEvaluate_Parenthesis()
        {
            var result = Evaluate("(2+3)*4");
            result.Should().Be(20);
        }
    }
}