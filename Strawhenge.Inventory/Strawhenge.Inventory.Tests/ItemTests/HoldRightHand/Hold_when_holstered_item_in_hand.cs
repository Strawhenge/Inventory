using System.Collections.Generic;
using FunctionalUtilities;
using Strawhenge.Inventory.Items;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests.ItemTests.HoldRightHand
{
    public class Hold_when_holstered_item_in_hand : BaseItemTest
    {
        readonly InventoryItem _hammer;
        readonly InventoryItem _spear;

        public Hold_when_holstered_item_in_hand(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            AddHolster(RightHipHolster);

            _hammer = CreateItem(Hammer, new[] { RightHipHolster });
            _hammer.Holsters[RightHipHolster].Do(x => x.Equip());
            _hammer.HoldRightHand();

            _spear = CreateTwoHandedItem(Spear);
            _spear.HoldRightHand();
        }

        protected override Maybe<InventoryItem> ExpectedItemInRightHand => _spear;

        protected override IEnumerable<(string holsterName, InventoryItem expectedItem)> ExpectedItemsInHolsters()
        {
            yield return (RightHipHolster, _hammer);
        }

        protected override IEnumerable<ProcedureInfo> ExpectedProceduresCompleted()
        {
            yield return (Hammer, RightHipHolster, Show);
            yield return (Hammer, RightHipHolster, DrawRightHand);
            yield return (Hammer, RightHipHolster, PutAwayRightHand);

            yield return (Spear, AppearRightHand);
        }
    }
}