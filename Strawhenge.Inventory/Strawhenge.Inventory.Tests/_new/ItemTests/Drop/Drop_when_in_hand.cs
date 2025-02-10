using System.Collections.Generic;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests._new.ItemTests.Drop
{
    public class Drop_when_in_hand : BaseItemTest
    {
        public Drop_when_in_hand(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            var hammer = CreateItem(Hammer);
            hammer.HoldRightHand();
            hammer.Drop();
        }

        protected override IEnumerable<ViewCallInfo> ExpectedViewCalls()
        {
            yield return (Hammer, x => x.DrawRightHand);
            yield return (Hammer, x => x.DropRightHand);
        }
    }
}