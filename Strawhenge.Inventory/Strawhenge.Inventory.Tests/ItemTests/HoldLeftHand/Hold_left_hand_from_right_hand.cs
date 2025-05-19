using System.Collections.Generic;
using FunctionalUtilities;
using Strawhenge.Inventory.Items;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests.ItemTests.HoldLeftHand
{
    public class Hold_left_hand_from_right_hand : BaseItemTest
    {
        readonly InventoryItem _hammer;

        public Hold_left_hand_from_right_hand(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            _hammer = CreateItem(Hammer);
            _hammer.HoldRightHand();
            _hammer.HoldLeftHand();
        }

        protected override Maybe<InventoryItem> ExpectedItemInLeftHand => _hammer;

        protected override IEnumerable<ProcedureInfo> ExpectedProceduresCompleted()
        {
            yield return (Hammer, AppearRightHand);
            yield return (Hammer, RightHandToLeftHand);
        }
    }
}