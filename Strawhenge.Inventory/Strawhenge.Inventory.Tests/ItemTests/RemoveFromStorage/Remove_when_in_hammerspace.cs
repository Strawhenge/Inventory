using System.Collections.Generic;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests.ItemTests.RemoveFromStorage
{
    public class Remove_when_in_hammerspace : BaseInventoryItemTest
    {
        public Remove_when_in_hammerspace(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            SetStorageCapacity(100);

            var hammer = CreateItem(Hammer, storable: true);
            hammer.Storable.Do(x => x.AddToStorage());
            hammer.Storable.Do(x => x.RemoveFromStorage());
        }

        protected override IEnumerable<ProcedureInfo> ExpectedProceduresCompleted()
        {
            yield return (Hammer, SpawnAndDrop);
        }
    }
}