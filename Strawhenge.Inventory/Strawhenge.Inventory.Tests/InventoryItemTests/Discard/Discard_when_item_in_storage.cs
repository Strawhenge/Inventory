using System.Collections.Generic;
using System.Linq;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests.InventoryItemTests.Discard
{
    public class Discard_when_item_in_storage : BaseInventoryItemTest
    {
        public Discard_when_item_in_storage(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            SetStorageCapacity(10);

            var hammer = CreateItem(Hammer, storable: true);
            hammer.Storable.Do(x => x.AddToStorage());
            hammer.Discard();
        }

        protected override IEnumerable<ProcedureInfo> ExpectedProceduresCompleted() =>
            Enumerable.Empty<ProcedureInfo>();
    }
}