using System;
using System.Collections.Generic;
using System.Linq;
using Calculator.Common.Lexer;
using Calculator.Common.Tools;

namespace Calculator.Common.Parser
{
    public static class SyntaxTokenKindExtensions
    {
        private static Lazy<Dictionary<SyntaxTokenKind, int>> _precedenceDictionary = new Lazy<Dictionary<SyntaxTokenKind, int>>(GetBinaryOperationPrecedenceImpl);

        public static int GetBinaryOperationPrecedence(this SyntaxTokenKind kind)
        {
            if (_precedenceDictionary.Value.ContainsKey(kind))
            {
                return _precedenceDictionary.Value[kind];
            }

            return 0;
        }

        private static Lazy<ILookup<SyntaxTokenGroup, SyntaxTokenKind>> _groupLookup = new Lazy<ILookup<SyntaxTokenGroup, SyntaxTokenKind>>(GetSyntaxTokenKindsDictionary);

        public static bool IsInTokenGroup(this SyntaxTokenKind kind, SyntaxTokenGroup @group)
        {
            if (_groupLookup.Value.Contains(group))
            {
                return _groupLookup.Value[group].Contains(kind);
            }

            return false;
        }

        private static ILookup<SyntaxTokenGroup, SyntaxTokenKind> GetSyntaxTokenKindsDictionary()
        {
            return EnumExtensions.GetAttribute<SyntaxTokenKind, SyntaxTokenGroupAttribute>()
                .SelectMany(t => t.Select(a => (Kind:t.Key, a.Group))).ToLookup(k => k.Group, v => v.Kind);
        }

        private static Dictionary<SyntaxTokenKind, int> GetBinaryOperationPrecedenceImpl()
        {
            return EnumExtensions.GetAttribute<SyntaxTokenKind, BinaryOperationPrecedenceAttribute>()
                .ToDictionary(k => k.Key, v => v.First().Precedence);
        }
    }
}