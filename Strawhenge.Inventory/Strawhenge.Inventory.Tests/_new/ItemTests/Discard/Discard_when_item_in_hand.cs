using System.Collections.Generic;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests._new.ItemTests.Discard
{
    public class Discard_when_item_in_hand : BaseItemTest
    {
        public Discard_when_item_in_hand(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            var hammer = CreateItem(Hammer);
            hammer.HoldRightHand();
            hammer.Discard();
        }

        protected override IEnumerable<ViewCallInfo> ExpectedViewCalls()
        {
            yield return (Hammer, x => x.DrawRightHand);
            yield return (Hammer, x => x.Disappear);
        }
    }
}