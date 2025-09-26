using System.Collections.Generic;
using FunctionalUtilities;
using Strawhenge.Inventory.Items;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests.ItemTests.HoldRightHand
{
    public class Hold_holstered_two_handed_item_right_hand_when_holstered_one_handed_item_in_left_hand
        : BaseInventoryItemTest
    {
        readonly InventoryItem _hammer;
        readonly InventoryItem _spear;

        public Hold_holstered_two_handed_item_right_hand_when_holstered_one_handed_item_in_left_hand(
            ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            AddHolster(RightHipHolster);
            AddHolster(BackHolster);

            _hammer = CreateItem(Hammer, new[] { RightHipHolster });
            _hammer.Holsters[RightHipHolster].Do(x => x.Equip());

            _spear = CreateTwoHandedItem(Spear, new[] { BackHolster });
            _spear.Holsters[BackHolster].Do(x => x.Equip());

            _hammer.HoldLeftHand();
            _spear.HoldRightHand(Callback);
        }

        protected override Maybe<InventoryItem> ExpectedItemInRightHand => _spear;

        protected override IEnumerable<(string holsterName, InventoryItem expectedItem)> ExpectedItemsInHolsters()
        {
            yield return (RightHipHolster, _hammer);
            yield return (BackHolster, _spear);
        }

        protected override IEnumerable<ProcedureInfo> ExpectedProceduresCompleted()
        {
            yield return (Hammer, RightHipHolster, Show);
            yield return (Spear, BackHolster, Show);

            yield return (Hammer, RightHipHolster, DrawLeftHand);
            yield return (Hammer, RightHipHolster, PutAwayLeftHand);

            yield return (Spear, BackHolster, DrawRightHand);
        }
    }
}