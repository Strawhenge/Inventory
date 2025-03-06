using System.Collections.Generic;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests.ItemTests.Discard
{
    public class Discard_when_stored_item_in_hand : BaseItemTest
    {
        public Discard_when_stored_item_in_hand(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            SetStorageCapacity(10);
            
            var hammer = CreateItem(Hammer, storable: true);
            hammer.Storable.Do(x => x.AddToStorage());
            hammer.HoldRightHand();
            hammer.Discard();
        }

        protected override IEnumerable<ViewCallInfo> ExpectedViewCalls()
        {
            yield return (Hammer, x => x.DrawRightHand);
            yield return (Hammer, x => x.DisappearRightHand);
        }
    }
}