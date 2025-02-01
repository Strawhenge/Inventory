using System;

namespace Strawhenge.Inventory.Tests._new
{
    public class TestSetupException : Exception
    {
        public TestSetupException(string reason)
            : base($"Test is not setup correctly. Reason: {reason}")
        {
        }
    }
}