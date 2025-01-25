using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;

namespace Strawhenge.Inventory.Tests._new
{
    class ViewCallsTracker
    {
        readonly List<ViewCallInfo> _viewCalls = new List<ViewCallInfo>();
        readonly TestOutputLogger _logger;

        public ViewCallsTracker(TestOutputLogger logger)
        {
            _logger = logger;
        }

        public void Track(ItemViewFake itemView) =>
            itemView.MethodInvoked += OnMethodInvoked;

        public void Track(HolsterForItemViewFake holsterForItemView) =>
            holsterForItemView.MethodInvoked += OnMethodInvoked;

        void OnMethodInvoked(ItemViewFake view, MethodInfo method) =>
            Log(new ViewCallInfo(view.ItemName, method));

        void OnMethodInvoked(HolsterForItemViewFake view, MethodInfo method) =>
            Log(new ViewCallInfo(view.ItemName, view.HolsterName, method));

        void Log(ViewCallInfo viewCall)
        {
            _viewCalls.Add(viewCall);
            _logger.LogInformation(viewCall.ToString());
        }

        public void VerifyViewCalls(ViewCallInfo[] expectedViewCalls)
        {
            var expected = expectedViewCalls.Select(x => x.ToString()).ToArray();
            var actual = _viewCalls.Select(x => x.ToString()).ToArray();

            Assert.Equal(expected, actual);
        }
    }
}