using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using Calculator.Common.Lexer;
using Calculator.Common.Parser;
using FluentAssertions;
using Xunit;

namespace Calculator.Tests
{
    public class ParserTests
    {
        [Fact]
        public void Foo()
        {
            var parser = new Parser(new SyntaxTokenEnumerable("2+3*4"));
            var res = parser.Parse();

            res.IsSuccessful.Should().BeTrue();
            res.Root.Should().BeEquivalentTo(new PlusBinaryNode(new NumberNode(2), new MultiplyBinaryNode(new NumberNode(3), new NumberNode(4))));
        }

        [Fact]
        public void Bar()
        {
            var parser = new Parser(new SyntaxTokenEnumerable("2*3+4"));
            var res = parser.Parse();

            res.IsSuccessful.Should().BeTrue();
            res.Root.Should().BeEquivalentTo(new PlusBinaryNode(new MultiplyBinaryNode(new NumberNode(2), new NumberNode(3)), new NumberNode(4)));
        }
    }
}