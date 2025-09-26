using System.Collections.Generic;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests.ItemTests.Drop
{
    public class Drop_transient_item_when_in_hand : BaseInventoryItemTest
    {
        public Drop_transient_item_when_in_hand(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            var hammer = CreateTransientItem(Hammer);
            hammer.HoldRightHand();
            hammer.Drop(Callback);
        }

        protected override IEnumerable<ProcedureInfo> ExpectedProceduresCompleted()
        {
            yield return (Hammer, AppearRightHand);
            yield return (Hammer, DisappearRightHand);
        }
    }
}