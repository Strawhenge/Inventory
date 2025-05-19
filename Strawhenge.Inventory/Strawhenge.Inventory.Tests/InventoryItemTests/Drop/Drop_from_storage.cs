using System.Collections.Generic;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests.InventoryItemTests.Drop
{
    public class Drop_from_storage : BaseInventoryItemTest
    {
        public Drop_from_storage(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            SetStorageCapacity(10);
            
            var hammer = CreateItem(Hammer, storable: true);
            hammer.Storable.Do(x => x.AddToStorage());
            hammer.Drop();
        }

        protected override IEnumerable<ProcedureInfo> ExpectedProceduresCompleted()
        {
            yield return (Hammer, SpawnAndDrop);
        }
    }
}