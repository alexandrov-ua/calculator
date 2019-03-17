using System;
using System.IO;
using Calculator.Repl;
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
            var storage = new FileLogStorage(fileName);
            storage.Log(new LogEntry() { Time = new DateTime(1234, 5, 6), Message = "1" });
            storage.Log(new LogEntry() { Time = new DateTime(1234, 5, 7), Message = "2" });

            var result = storage.GetAll();
            result[0].Message.Should().Be("1");
            result[0].Time.Should().Be(new DateTime(1234, 5, 6));
            result[1].Message.Should().Be("2");
            result[1].Time.Should().Be(new DateTime(1234, 5, 7));
            File.Delete(fileName);
        }
    }
}
