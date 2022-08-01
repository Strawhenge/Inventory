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

        public void LogWarning(string message) => testOutput.WriteLine($"[Warning] {message}");

        public void LogError(string message) => testOutput.WriteLine($"[Error] {message}");
    }
}
