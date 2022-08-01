using System;
using Xunit;

namespace Strawhenge.Inventory.Tests
{
    public class AssertableCallback
    {
        public static implicit operator Action(AssertableCallback callback) => callback.Invoke;

        int invokeCount;

        public void AssertNeverCalled()
        {
            Assert.Equal(0, invokeCount);
        }

        public void AssertCalledOnce()
        {
            Assert.Equal(1, invokeCount);
        }

        public void AssertCalledTimes(int times)
        {
            Assert.Equal(times, invokeCount);
        }

        void Invoke() => invokeCount++;
    }
}
