using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Strawhenge.Inventory.Tests
{
    class ApparelViewCallTracker
    {
        readonly List<ApparelViewCallInfo> _calls = new List<ApparelViewCallInfo>();
        readonly TestOutputLogger _logger;

        public ApparelViewCallTracker(TestOutputLogger logger)
        {
            _logger = logger;
        }

        public void Track(ApparelViewFake view)
        {
            view.ShowInvoked += () => Log((view.ItemName, "Show"));
            view.HideInvoked += () => Log((view.ItemName, "Hide"));
            view.DropInvoked += () => Log((view.ItemName, "Drop"));
        }
        
        public void VerifyCalls(ApparelViewCallInfo[] expectedCalls)
        {
            var expected = expectedCalls
                .Select(x => x.ToString())
                .ToArray();

            var actual = _calls
                .Select(x => x.ToString())
                .ToArray();

            Assert.Equal(expected, actual);
        }

        void Log(ApparelViewCallInfo info)
        {
            _calls.Add(info);
            _logger.LogInformation(info.ToString());
        }
    }
}