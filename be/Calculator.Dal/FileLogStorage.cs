using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Calculator.Dal
{
    public class FileLogStorage<T> : ILogStorage<T>
    {
        private readonly string _fileName;

        public FileLogStorage(string fileName)
        {
            _fileName = fileName;
        }

        public void Log(T obj)
        {
            Log(new LogEntry<T>()
            {
                Time = DateTime.UtcNow,
                Data = obj
            });
        }

        private void Log(LogEntry<T> entry)
        {
            File.AppendAllText(_fileName, JsonConvert.SerializeObject(entry, Formatting.None) + Environment.NewLine);
        }

        public LogEntry<T>[] GetAll()
        {
            return File.ReadAllLines(_fileName)
                .Select(s => JsonConvert.DeserializeObject<LogEntry<T>>(s))
                .ToArray();
        }
    }
}