using System.Collections.Generic;
using FunctionalUtilities;
using Strawhenge.Inventory.Items;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests.ItemTests.Swap
{
    public class Swap_when_item_in_each_hand : BaseInventoryItemTest
    {
        readonly InventoryItem _hammer;
        readonly InventoryItem _knife;

        public Swap_when_item_in_each_hand(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            _hammer = CreateItem(Hammer);
            _hammer.HoldRightHand();

            _knife = CreateItem(Knife);
            _knife.HoldLeftHand();

            Inventory.SwapHands();
        }

        protected override Maybe<InventoryItem> ExpectedItemInLeftHand => _hammer;

        protected override Maybe<InventoryItem> ExpectedItemInRightHand => _knife;

        protected override IEnumerable<ProcedureInfo> ExpectedProceduresCompleted()
        {
            yield return (Hammer, AppearRightHand);
            yield return (Knife, AppearLeftHand);

            yield return (Knife, DisappearLeftHand);
            yield return (Hammer, RightHandToLeftHand);
            yield return (Knife, AppearRightHand);
        }
    }
}