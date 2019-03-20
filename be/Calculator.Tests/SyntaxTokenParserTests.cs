using Calculator.Common.Lexer;
using Calculator.Common.Parser;
using Calculator.Common.SyntaxThree;
using DeepEqual.Syntax;
using FluentAssertions;
using Xunit;

namespace Calculator.Tests
{
    public class SyntaxTokenParserTests
    {
        private static ParserResult Parse(string input)
        {
            var parser = new SyntaxTokenParser(new SyntaxTokenEnumerable(input));
            return parser.Parse();
        }

        [Fact]
        public void SyntaxTokenParser_ShouldParse_AccordingToPrecedence()
        {
            var res = Parse("2+3*4");

            res.IsSuccessful.Should().BeTrue();
            res.Root.ShouldDeepEqual(new PlusBinaryNode(new NumberNode(2), new MultiplyBinaryNode(new NumberNode(3), new NumberNode(4))));
        }

        [Fact]
        public void SyntaxTokenParser_ShouldParse_AccordingToPrecedence2()
        {
            var res = Parse("2*3+4");

            res.IsSuccessful.Should().BeTrue();
            res.Root.ShouldDeepEqual(new PlusBinaryNode(new MultiplyBinaryNode(new NumberNode(2), new NumberNode(3)), new NumberNode(4)));
        }

        [Fact]
        public void SyntaxTokenParser_ShouldParse_UnaryOperator()
        {
            var res = Parse("-2++3");

            res.IsSuccessful.Should().BeTrue();
            res.Root.ShouldDeepEqual(new PlusBinaryNode(new MinusUnaryNode(new NumberNode(2)), new PlusUnaryNode(new NumberNode(3))));
        }

        [Fact]
        public void SyntaxTokenParser_ShouldParse_UnaryOperators_Sequentially()
        {
            var res = Parse("-2++-+3");

            res.IsSuccessful.Should().BeTrue();
            res.Root.ShouldDeepEqual(new PlusBinaryNode(new MinusUnaryNode(new NumberNode(2)), new PlusUnaryNode(new MinusUnaryNode(new PlusUnaryNode(new NumberNode(3))))));
        }

        [Fact]
        public void SyntaxTokenParser_ShouldParse_UnaryOperators_AccordingToPrecedence()
        {
            var res = Parse("2+3*+4");

            res.IsSuccessful.Should().BeTrue();
            res.Root.ShouldDeepEqual(new PlusBinaryNode(new NumberNode(2), new MultiplyBinaryNode(new NumberNode(3), new PlusUnaryNode(new NumberNode(4)))));
        }

        [Fact]
        public void SyntaxTokenParser_ShouldParse_UnaryOperators_AccordingToPrecedence2()
        {
            var res = Parse("2*3++4");

            res.IsSuccessful.Should().BeTrue();
            var plusBinaryNode = new MultiplyBinaryNode(new MultiplyBinaryNode(new NumberNode(2), new NumberNode(3)), new PlusUnaryNode(new NumberNode(4)));
            res.Root.ShouldDeepEqual(plusBinaryNode);
        }

        [Fact]
        public void SyntaxTokenParser_ShouldParse_Parenthesis()
        {
            var res = Parse("(2+3)*4");

            res.IsSuccessful.Should().BeTrue();
            res.Root.ShouldDeepEqual(new MultiplyBinaryNode(new ParenthesisNode(new PlusBinaryNode(new NumberNode(2), new NumberNode(3))), new NumberNode(4)));
        }

        [Fact]
        public void SyntaxTokenParser_ShouldParse_Parenthesis2()
        {
            var res = Parse("-(2)");

            res.IsSuccessful.Should().BeTrue();
            res.Root.ShouldDeepEqual(new MinusUnaryNode(new ParenthesisNode(new NumberNode(2))));
        }
    }
}