using System.Collections.Generic;
using Strawhenge.Inventory.Items;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests.ItemTests.ClearFromHands
{
    public class Clear_holstered_item_from_hands : BaseInventoryItemTest
    {
        readonly InventoryItem _hammer;

        public Clear_holstered_item_from_hands(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            AddHolster(RightHipHolster);

            _hammer = CreateItem(Hammer, new[] { RightHipHolster });
            _hammer.HoldRightHand();
            _hammer.Holsters[RightHipHolster].Do(x => x.Equip());
            _hammer.ClearFromHands();
        }

        protected override IEnumerable<(string holsterName, InventoryItem expectedItem)> ExpectedItemsInHolsters()
        {
            yield return (RightHipHolster, _hammer);
        }

        protected override IEnumerable<ProcedureInfo> ExpectedProceduresCompleted()
        {
            yield return (Hammer, AppearRightHand);
            yield return (Hammer, RightHipHolster, PutAwayRightHand);
        }
    }
}