using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Calculator.Common.Lexer
{
    public class SyntaxTokenEnumerable : IEnumerable<SyntaxToken>
    {
        private readonly string _input;

        public SyntaxTokenEnumerable(string input)
        {
            _input = input;
        }

        public IEnumerator<SyntaxToken> GetEnumerator()
        {
            return new SyntaxTokenEnumerator(_input);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class SyntaxTokenEnumerator : IEnumerator<SyntaxToken>
    {
        private readonly string _input;
        private int _index;
        object IEnumerator.Current => Current;
        public SyntaxToken Current { get; private set; }

        public SyntaxTokenEnumerator(string input)
        {
            _input = input;
            _index = 0;
        }

        public bool MoveNext()
        {
            for (; _index < _input.Length; _index++)
            {
                switch (_input[_index])
                {
                    case '+':
                        Current = new SyntaxToken(SyntaxTokenKind.Plus, _index, _input.Substring(_index, 1));
                        _index++;
                        return true;
                    case '-':
                        Current = new SyntaxToken(SyntaxTokenKind.Minus, _index, _input.Substring(_index, 1));
                        _index++;
                        return true;
                    case '/':
                        Current = new SyntaxToken(SyntaxTokenKind.Slash, _index, _input.Substring(_index, 1));
                        _index++;
                        return true;
                    case '*':
                        Current = new SyntaxToken(SyntaxTokenKind.Star, _index, _input.Substring(_index, 1));
                        _index++;
                        return true;
                    case '(':
                        Current = new SyntaxToken(SyntaxTokenKind.OpenParenthesis, _index, _input.Substring(_index, 1));
                        _index++;
                        return true;
                    case ')':
                        Current = new SyntaxToken(SyntaxTokenKind.CloseParenthesis, _index, _input.Substring(_index, 1));
                        _index++;
                        return true;
                    case var d when Char.IsDigit(d): //match numbers
                    {
                        var str = ReadChars(c => Char.IsDigit(c) || c == '.');
                        Current = new SyntaxToken(SyntaxTokenKind.Number, _index, str);
                        _index += str.Length;
                        return true;
                    }
                    case var l when Char.IsLetter(l): //match identifiers
                        {
                        var str = ReadChars(Char.IsLetterOrDigit);
                        Current = new SyntaxToken(SyntaxTokenKind.Identifier, _index, str);
                        _index += str.Length;
                        return true;
                    }
                    case var w when Char.IsWhiteSpace(w)://skipping white spaces
                        continue;
                    default:
                        Current = new SyntaxToken(SyntaxTokenKind.Unknown, _index, _input.Substring(_index, 1));
                        _index++;
                        return true;
                }
            }
            return false;
        }

        private string ReadChars(Func<char, bool> func)
        {
            StringBuilder sb = new StringBuilder();
            for (var i = 0; _index + i < _input.Length && func(_input[_index + i]); i++)
            {
                sb.Append(_input[_index + i]);
            }

            return sb.ToString();
        }

        public void Reset()
        {
            _index = 0;
        }

        public void Dispose()
        {
        }
    }
}