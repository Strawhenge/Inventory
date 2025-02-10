using System.Collections.Generic;
using FunctionalUtilities;
using Strawhenge.Inventory.Items;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests._new
{
    public class Hold_when_stored_item_in_hand : BaseItemTest
    {
        readonly Item _hammer;
        readonly Item _knife;

        public Hold_when_stored_item_in_hand(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            SetStorageCapacity(10);
            
            _hammer = CreateItem(Hammer, storable: true);
            _hammer.Storable.Do(x => x.AddToStorage());
            _hammer.HoldRightHand();

            _knife = CreateItem(Knife);
            _knife.HoldRightHand();
        }

        protected override Maybe<Item> ExpectedItemInRightHand => _knife;

        protected override IEnumerable<IItem> ExpectedItemsInStorage()
        {
            yield return _hammer;
        }

        protected override IEnumerable<ViewCallInfo> ExpectedViewCalls()
        {
            yield return (Hammer, x => x.DrawRightHand);
            yield return (Hammer, x => x.PutAwayRightHand);

            yield return (Knife, x => x.DrawRightHand);
        }
    }
}