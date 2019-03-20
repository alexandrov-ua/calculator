using System.Diagnostics;
using System.Runtime.InteropServices.ComTypes;

namespace Calculator.Repl
{
    public static class ReplAppFactory
    {
        public static ReplApp Create()
        {
            return new ReplApp(EvaluatorFactory.Create());
        }
    }
}