using System.Collections.Generic;
using FunctionalUtilities;
using Strawhenge.Inventory.Items;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests.InventoryItemTests.Swap
{
    public class Swap_when_item_in_right_hand : BaseInventoryItemTest
    {
        readonly InventoryItem _hammer;

        public Swap_when_item_in_right_hand(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            _hammer = CreateItem(Hammer);
            _hammer.HoldRightHand();
            
            Inventory.SwapHands();
        }

        protected override Maybe<InventoryItem> ExpectedItemInLeftHand => _hammer;

        protected override IEnumerable<ProcedureInfo> ExpectedProceduresCompleted()
        {
            yield return (Hammer, AppearRightHand);
            yield return (Hammer, RightHandToLeftHand);
        }
    }
}