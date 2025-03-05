using System.Collections.Generic;
using Strawhenge.Inventory.Items;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests.ItemTests.ClearFromHands
{
    public class Clear_stored_item_from_hands : BaseItemTest
    {
        readonly Item _hammer;

        public Clear_stored_item_from_hands(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            SetStorageCapacity(10);

            _hammer = CreateItem(Hammer, storable: true);
            _hammer.HoldRightHand();
            _hammer.Storable.Do(x => x.AddToStorage());
            _hammer.ClearFromHands();
        }

        protected override IEnumerable<IItem> ExpectedItemsInStorage()
        {
            yield return _hammer;
        }

        protected override IEnumerable<ViewCallInfo> ExpectedViewCalls()
        {
            yield return (Hammer, x => x.DrawRightHand);
            yield return (Hammer, x => x.PutAwayRightHand);
        }
    }
}