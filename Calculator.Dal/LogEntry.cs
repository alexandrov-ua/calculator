using System;

namespace Calculator.Dal
{
    public class LogEntry<T>
    {
        public DateTime Time { get; set; }
        public T Data { get; set; }
    }
}