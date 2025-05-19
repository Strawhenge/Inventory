using System.Collections.Generic;
using FunctionalUtilities;
using Strawhenge.Inventory.Items;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests.ItemTests.HoldRightHand
{
    public class Hold_when_stored_item_in_hand : BaseItemTest
    {
        readonly InventoryItem _hammer;
        readonly InventoryItem _knife;

        public Hold_when_stored_item_in_hand(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            SetStorageCapacity(10);

            _hammer = CreateItem(Hammer, storable: true);
            _hammer.Storable.Do(x => x.AddToStorage());
            _hammer.HoldRightHand();

            _knife = CreateItem(Knife);
            _knife.HoldRightHand();
        }

        protected override Maybe<InventoryItem> ExpectedItemInRightHand => _knife;

        protected override IEnumerable<InventoryItem> ExpectedItemsInStorage()
        {
            yield return _hammer;
        }

        protected override IEnumerable<ProcedureInfo> ExpectedProceduresCompleted()
        {
            yield return (Hammer, DrawRightHand);
            yield return (Hammer, PutAwayRightHand);

            yield return (Knife, AppearRightHand);
        }
    }
}