using System.Collections.Generic;
using FunctionalUtilities;
using Strawhenge.Inventory.Items;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests.InventoryItemTests.HoldRightHand
{
    public class Hold_from_hammerspace : BaseInventoryItemTest
    {
        readonly InventoryItem _hammer;

        public Hold_from_hammerspace(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            SetStorageCapacity(100);

            _hammer = CreateItem(Hammer, storable: true);
            _hammer.Storable.Do(x => x.AddToStorage());
            _hammer.HoldRightHand();
        }

        protected override Maybe<InventoryItem> ExpectedItemInRightHand => _hammer;

        protected override IEnumerable<InventoryItem> ExpectedItemsInStorage()
        {
            yield return _hammer;
        }

        protected override IEnumerable<ProcedureInfo> ExpectedProceduresCompleted()
        {
            yield return (Hammer, DrawRightHand);
        }
    }
}