using System;
using Xunit;

namespace Strawhenge.Inventory.Tests
{
    public class AssertableCallback
    {
        public static implicit operator Action(AssertableCallback callback) => callback.Invoke;

        int _invokeCount;

        public void AssertNeverCalled()
        {
            Assert.Equal(0, _invokeCount);
        }

        public void AssertCalledOnce()
        {
            Assert.Equal(1, _invokeCount);
        }

        public void AssertCalledTimes(int times)
        {
            Assert.Equal(times, _invokeCount);
        }

        void Invoke() => _invokeCount++;
    }
}
