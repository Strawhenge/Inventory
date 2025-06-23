using System.Collections.Generic;
using FunctionalUtilities;
using Strawhenge.Inventory.Items;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests.ItemTests.HoldRightHand
{
    public class Hold_holstered_item_right_hand_when_holstered_item_in_left_hand
        : BaseInventoryItemTest
    {
        readonly InventoryItem _hammer;
        readonly InventoryItem _knife;

        public Hold_holstered_item_right_hand_when_holstered_item_in_left_hand(ITestOutputHelper testOutputHelper) : base(
            testOutputHelper)
        {
            AddHolster(RightHipHolster);
            AddHolster(LeftHipHolster);

            _hammer = CreateItem(Hammer, new[] { LeftHipHolster, RightHipHolster });
            _hammer.Holsters[RightHipHolster].Do(x => x.Equip());

            _knife = CreateItem(Knife, new[] { LeftHipHolster, RightHipHolster });
            _knife.Holsters[LeftHipHolster].Do(x => x.Equip());

            _hammer.HoldLeftHand();
            _knife.HoldRightHand();
        }

        protected override Maybe<InventoryItem> ExpectedItemInLeftHand => _hammer;

        protected override Maybe<InventoryItem> ExpectedItemInRightHand => _knife;

        protected override IEnumerable<(string holsterName, InventoryItem expectedItem)> ExpectedItemsInHolsters()
        {
            yield return (LeftHipHolster, _knife);
            yield return (RightHipHolster, _hammer);
        }

        protected override IEnumerable<ProcedureInfo> ExpectedProceduresCompleted()
        {
            yield return (Hammer, RightHipHolster, Show);
            yield return (Knife, LeftHipHolster, Show);

            yield return (Hammer, RightHipHolster, DrawLeftHand);
            yield return (Knife, LeftHipHolster, DrawRightHand);
        }
    }
}