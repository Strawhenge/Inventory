using System.Collections.Generic;
using FunctionalUtilities;
using Strawhenge.Inventory.Items;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests.InventoryItemTests.RemoveFromStorage
{
    public class Remove_when_in_hammerspace : BaseInventoryItemTest
    {
        readonly InventoryItem _hammer;

        public Remove_when_in_hammerspace(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            SetStorageCapacity(100);

            _hammer = CreateItem(Hammer, storable: true);
            _hammer.Storable.Do(x => x.AddToStorage());
            _hammer.Storable.Do(x => x.RemoveFromStorage());
        }

        protected override IEnumerable<ProcedureInfo> ExpectedProceduresCompleted()
        {
            yield return (Hammer, SpawnAndDrop);
        }
    }
}