using System.Collections.Generic;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests.ItemTests.ClearFromHands
{
    public class Clear_transient_item_from_hands : BaseItemTest
    {
        public Clear_transient_item_from_hands(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            var hammer = CreateTransientItem(Hammer);
            hammer.HoldRightHand();
            hammer.ClearFromHands();
        }

        protected override IEnumerable<ViewCallInfo> ExpectedViewCalls()
        {
            yield return (Hammer, x => x.AppearRightHand);
            yield return (Hammer, x => x.DisappearRightHand);
        }
    }
}