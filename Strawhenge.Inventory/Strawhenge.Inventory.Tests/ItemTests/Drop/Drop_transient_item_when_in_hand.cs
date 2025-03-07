using System.Collections.Generic;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests.ItemTests.Drop
{
    public class Drop_transient_item_when_in_hand : BaseItemTest
    {
        public Drop_transient_item_when_in_hand(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            var hammer = CreateTransientItem(Hammer);
            hammer.HoldRightHand();
            hammer.Drop();
        }

        protected override IEnumerable<ViewCallInfo> ExpectedViewCalls()
        {
            yield return (Hammer, x => x.AppearRightHand);
            yield return (Hammer, x => x.DisappearRightHand);
        }
    }
}