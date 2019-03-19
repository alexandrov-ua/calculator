using System;
using System.Linq;
using System.Text;
using Calculator.Common.Evaluator;
using Calculator.Common.Parser;

namespace Calculator.Repl
{
    public class ReplApp
    {
        private readonly IStringEvaluator _stringEvaluator;

        public ReplApp(IStringEvaluator stringEvaluator)
        {
            _stringEvaluator = stringEvaluator;
        }

        public void StartMainLoop()
        {
            ShowHelp();
            var exit = false;
            while (!exit)
            {
                Console.Write(">");
                var input = Console.ReadLine();
                switch (input?.Trim() ?? String.Empty)
                {
                    case "#help":
                        ShowHelp();
                        break;
                    case "#cls":
                    case "#clear":
                        Console.Clear();
                        break;
                    case "#exit":
                        exit = true;
                        break;
                    default:
                        EvaluateString(input);
                        break;
                }
            }
        }

        private void EvaluateString(string input)
        {
            try
            {
                var result = _stringEvaluator.Evaluate(input);
                if (result.IsSuccessful)
                {
                    Console.WriteLine($"{result.Result}");
                }
                else
                {
                    FormatError(input, result.Diagnostics.First());
                }
            }
            catch (Exception e)
            {
                using (new ConsoleColorRegion(ConsoleColor.Red))
                {
                    Console.WriteLine(e);
                }
            }
        }

        private void FormatError(string input, DiagnosticsEntry entry)
        {
            using (new ConsoleColorRegion(ConsoleColor.Red))
            {
                switch (entry.Kind)
                {
                    case DiagnosticKind.UnexpectedToken:
                        Console.WriteLine($"{input}\n{MakeArrow(entry.Span)}\n Expected token: {entry.Parameters[0]}\n But found: {entry.Parameters[1]}");
                        break;
                    default:
                        Console.WriteLine($"Error: {entry.Kind}");
                        break;
                }
            }
        }

        private string MakeArrow(TextSpan entrySpan)
        {
            var sb = new StringBuilder();
            for (var i = 0; i < entrySpan.Start; i++)
                sb.Append(' ');
            for (var i = 0; i < entrySpan.Length; i++)
                sb.Append('^');
            return sb.ToString();
        }

        class ConsoleColorRegion : IDisposable
        {
            private ConsoleColor _temp;
            public ConsoleColorRegion(ConsoleColor color)
            {
                _temp = Console.ForegroundColor;
                Console.ForegroundColor = color;
            }

            public void Dispose()
            {
                Console.ForegroundColor = _temp;
            }
        }

        private void ShowHelp()
        {
            Console.WriteLine("Simple math expressions evaluator.");
            Console.WriteLine("Input example: 2+3*4");
            Console.WriteLine("Supported operations: Binary: +-*/^ Unary: +- Braces: ()");
            Console.WriteLine("REPL commands: ");
            Console.WriteLine("#help - to show help");
            Console.WriteLine("#cls - clear screen");
            Console.WriteLine("#exit - to exit");
        }
    }
}