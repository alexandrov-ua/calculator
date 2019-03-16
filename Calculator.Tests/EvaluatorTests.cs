using Calculator.Common.Evaluator;
using Calculator.Common.Lexer;
using Calculator.Common.Parser;
using FluentAssertions;
using Xunit;

namespace Calculator.Tests
{
    public class EvaluatorTests
    {
        [Fact]
        public void Foo()
        {
            var parser = new Parser(new SyntaxTokenEnumerable("2+3*4"));
            var parserResult = parser.Parse();
            var evaluator = new SyntaxThreeEvaluator(parserResult.Root);
            var result = evaluator.Evaluate();
            result.Should().Be(14);
        }

        [Fact]
        public void Bar()
        {
            var parser = new Parser(new SyntaxTokenEnumerable("2*3+4"));
            var parserResult = parser.Parse();
            var evaluator = new SyntaxThreeEvaluator(parserResult.Root);
            var result = evaluator.Evaluate();
            result.Should().Be(10);
        }
    }
}