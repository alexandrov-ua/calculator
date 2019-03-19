using System;
using System.Collections.Generic;

namespace Calculator.Dal
{
    public class InMemoryLogStorage<T> : ILogStorage<T>
    {
        private List<LogEntry<T>> _entries = new List<LogEntry<T>>();

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
            _entries.Add(entry);
        }

        public LogEntry<T>[] GetAll()
        {
            return _entries.ToArray();
        }
    }
}