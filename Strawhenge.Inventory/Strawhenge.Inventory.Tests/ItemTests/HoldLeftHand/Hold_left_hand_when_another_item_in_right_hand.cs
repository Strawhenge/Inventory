using System.Collections.Generic;
using FunctionalUtilities;
using Strawhenge.Inventory.Items;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests.ItemTests.HoldLeftHand
{
    public class Hold_left_hand_when_another_item_in_right_hand : BaseInventoryItemTest
    {
        readonly InventoryItem _hammer;
        readonly InventoryItem _knife;

        public Hold_left_hand_when_another_item_in_right_hand(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            _hammer = CreateItem(Hammer);
            _hammer.HoldRightHand();

            _knife = CreateItem(Knife);
            _knife.HoldLeftHand(Callback);
        }

        protected override Maybe<InventoryItem> ExpectedItemInLeftHand => _knife;

        protected override Maybe<InventoryItem> ExpectedItemInRightHand => _hammer;

        protected override IEnumerable<ProcedureInfo> ExpectedProceduresCompleted()
        {
            yield return (Hammer, AppearRightHand);
            yield return (Knife, AppearLeftHand);
        }
    }
}