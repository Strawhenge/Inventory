using System.Collections.Generic;
using Strawhenge.Inventory.Items;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests.InventoryItemTests.ClearFromHands
{
    public class Clear_stored_item_from_hands : BaseInventoryItemTest
    {
        readonly InventoryItem _hammer;

        public Clear_stored_item_from_hands(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            SetStorageCapacity(10);

            _hammer = CreateItem(Hammer, storable: true);
            _hammer.HoldRightHand();
            _hammer.Storable.Do(x => x.AddToStorage());
            _hammer.ClearFromHands();
        }

        protected override IEnumerable<InventoryItem> ExpectedItemsInStorage()
        {
            yield return _hammer;
        }

        protected override IEnumerable<ProcedureInfo> ExpectedProceduresCompleted()
        {
            yield return (Hammer, AppearRightHand);
            yield return (Hammer, PutAwayRightHand);
        }
    }
}