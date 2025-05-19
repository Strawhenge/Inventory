using System.Collections.Generic;
using FunctionalUtilities;
using Strawhenge.Inventory.Items;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests.InventoryItemTests.HoldLeftHand
{
    public class Hold_one_handed_left_hand_when_two_handed_in_right_hand : BaseInventoryItemTest
    {
        readonly InventoryItem _hammer;

        public Hold_one_handed_left_hand_when_two_handed_in_right_hand(ITestOutputHelper testOutputHelper) : base(
            testOutputHelper)
        {
            var spear = CreateTwoHandedItem(Spear);
            spear.HoldRightHand();

            _hammer = CreateItem(Hammer);
            _hammer.HoldLeftHand();
        }

        protected override Maybe<InventoryItem> ExpectedItemInLeftHand => _hammer;

        protected override IEnumerable<ProcedureInfo> ExpectedProceduresCompleted()
        {
            yield return (Spear, AppearRightHand);
            yield return (Spear, DropRightHand);

            yield return (Hammer, AppearLeftHand);
        }
    }
}