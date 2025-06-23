using System.Collections.Generic;
using Strawhenge.Inventory.Items;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests.ItemTests.PutAway
{
    public class Put_away_when_equipped_to_new_holster : BaseInventoryItemTest
    {
        readonly InventoryItem _hammer;

        public Put_away_when_equipped_to_new_holster(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            AddHolster(LeftHipHolster);
            AddHolster(RightHipHolster);

            _hammer = CreateItem(Hammer, new[] { LeftHipHolster, RightHipHolster });
            _hammer.Holsters[RightHipHolster].Do(x => x.Equip());
            _hammer.HoldRightHand();
            _hammer.Holsters[LeftHipHolster].Do(x => x.Equip());
            _hammer.PutAway();
        }

        protected override IEnumerable<(string holsterName, InventoryItem expectedItem)> ExpectedItemsInHolsters()
        {
            yield return (LeftHipHolster, _hammer);
        }

        protected override IEnumerable<ProcedureInfo> ExpectedProceduresCompleted()
        {
            yield return (Hammer, RightHipHolster, Show);
            yield return (Hammer, RightHipHolster, DrawRightHand);

            yield return (Hammer, LeftHipHolster, PutAwayRightHand);
        }
    }
}