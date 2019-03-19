using System;
using Calculator.Dal;

namespace Calculator.Api
{
    public static class LogSorageFactory
    {
        private static Lazy<ILogStorage<EvaluatorLog>> _lazy = new Lazy<ILogStorage<EvaluatorLog>>(()=>new InMemoryLogStorage<EvaluatorLog>());

        public static ILogStorage<EvaluatorLog> Create()
        {
            return _lazy.Value;
        }
    }
}