using System;
using Strawhenge.Common.Logging;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests
{
    public class TestOutputLogger : ILogger
    {
        private readonly ITestOutputHelper testOutput;

        public TestOutputLogger(ITestOutputHelper testOutput)
        {
            this.testOutput = testOutput;
        }

        public void LogInformation(string message) => testOutput.WriteLine($"[Information] {message}");

        public void LogWarning(string message) => testOutput.WriteLine($"[Warning] {message}");

        public void LogError(string message) => testOutput.WriteLine($"[Error] {message}");
        public void LogException(Exception exception) => testOutput.WriteLine($"[Exception] {exception}");
    }
}