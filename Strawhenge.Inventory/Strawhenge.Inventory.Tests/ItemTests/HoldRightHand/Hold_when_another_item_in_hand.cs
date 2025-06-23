using System.Collections.Generic;
using FunctionalUtilities;
using Strawhenge.Inventory.Items;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests.ItemTests.HoldRightHand
{
    public class Hold_when_another_item_in_hand : BaseInventoryItemTest
    {
        readonly InventoryItem _spear;

        public Hold_when_another_item_in_hand(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            var hammer = CreateItem(Hammer);
            hammer.HoldRightHand();

            _spear = CreateTwoHandedItem(Spear);
            _spear.HoldRightHand();
        }

        protected override Maybe<InventoryItem> ExpectedItemInRightHand => _spear;

        protected override IEnumerable<ProcedureInfo> ExpectedProceduresCompleted()
        {
            yield return (Hammer, AppearRightHand);
            yield return (Hammer, DropRightHand);

            yield return (Spear, AppearRightHand);
        }
    }
}