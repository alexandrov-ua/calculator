using System;
using System.IO;
using Calculator.Common.Evaluator;
using Calculator.Dal;
using FluentAssertions;
using Xunit;

namespace Calculator.IntegrationTests
{
    public class FileLogStorageTests
    {
        [Fact]
        public void FileLogStorage_GetAll_ShouldReturnValues()
        {
            var fileName = Guid.NewGuid().ToString("N") + ".log";
            var storage = new FileLogStorage<EvaluatorLog>(fileName);
            storage.Log(new EvaluatorLog(){ Input = "1", Output = new EvaluatorResult(true, 12.0, null)});
            storage.Log(new EvaluatorLog() { Input = "2", Output = new EvaluatorResult(true, 22.0, null) });

            var result = storage.GetAll();
            result[0].Data.Input.Should().Be("1");
            result[0].Data.Output.Result.Should().Be(12.0);
            result[1].Data.Input.Should().Be("2");
            result[1].Data.Output.Result.Should().Be(22.0);

            File.Delete(fileName);
        }
    }
}
