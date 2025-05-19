using System.Collections.Generic;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests.InventoryItemTests.ClearFromHands
{
    public class Clear_transient_item_from_hands : BaseInventoryItemTest
    {
        public Clear_transient_item_from_hands(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            var hammer = CreateTransientItem(Hammer);
            hammer.HoldRightHand();
            hammer.ClearFromHands();
        }

        protected override IEnumerable<ProcedureInfo> ExpectedProceduresCompleted()
        {
            yield return (Hammer, AppearRightHand);
            yield return (Hammer, DisappearRightHand);
        }
    }
}