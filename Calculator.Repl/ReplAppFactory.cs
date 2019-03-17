using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using Calculator.Common.Evaluator;
using Newtonsoft.Json;

namespace Calculator.Repl
{
    public static class ReplAppFactory
    {
        public static ReplApp Create()
        {
            return new ReplApp(EvaluatorFactory.Create());
        }
    }

    public static class EvaluatorFactory
    {
        private static ILogStorage _logStorage = new FileLogStorage("log.txt");

        public static IStringEvaluator Create()
        {
            return new LoggingStringEvaluatorDecorator(new StringEvaluator(), _logStorage);
        }
    }

    public class LoggingStringEvaluatorDecorator : IStringEvaluator
    {
        private readonly IStringEvaluator _evaluator;
        private readonly ILogStorage _logStorage;

        public LoggingStringEvaluatorDecorator(IStringEvaluator evaluator, ILogStorage logStorage)
        {
            _evaluator = evaluator;
            _logStorage = logStorage;
        }

        public EvaluatorResult Evaluate(string input)
        {
            var result = _evaluator.Evaluate(input);
            _logStorage.Log($"Input: {input}; Output: {result.Result}");
            return result;
        }
    }

    public interface ILogStorage
    {
        void Log(string log);
        void Log(LogEntry entry);
        LogEntry[] GetAll();
    }

    public class FileLogStorage : ILogStorage
    {
        private readonly string _fileName;

        public FileLogStorage(string fileName)
        {
            _fileName = fileName;
        }

        public void Log(string log)
        {
            Log(new LogEntry()
            {
                Time = DateTime.UtcNow,
                Message = log
            });
        }

        public void Log(LogEntry entry)
        {
            File.AppendAllText(_fileName, JsonConvert.SerializeObject(entry, Formatting.None));
        }

        public LogEntry[] GetAll()
        {
            return File.ReadAllLines(_fileName)
                .Select(s => JsonConvert.DeserializeObject<LogEntry>(s))
                .ToArray();
        }
    }

    public class LogEntry
    {
        public DateTime Time { get; set; }
        public string Message { get; set; }
    }
}