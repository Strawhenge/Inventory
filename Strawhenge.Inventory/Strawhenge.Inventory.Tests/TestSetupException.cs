using System;

namespace Strawhenge.Inventory.Tests
{
    public class TestSetupException : Exception
    {
        public TestSetupException(string reason)
            : base($"Test is not setup correctly. Reason: {reason}")
        {
        }
    }
}