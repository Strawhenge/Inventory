using System.Collections.Generic;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests.ItemTests.Drop
{
    public class Drop_when_stored_and_in_hand : BaseInventoryItemTest
    {
        public Drop_when_stored_and_in_hand(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            SetStorageCapacity(10);

            var hammer = CreateItem(Hammer, storable: true);
            hammer.Storable.Do(x => x.AddToStorage());
            hammer.HoldRightHand();
            hammer.Drop(Callback);
        }

        protected override IEnumerable<ProcedureInfo> ExpectedProceduresCompleted()
        {
            yield return (Hammer, DrawRightHand);
            yield return (Hammer, DropRightHand);
        }
    }
}