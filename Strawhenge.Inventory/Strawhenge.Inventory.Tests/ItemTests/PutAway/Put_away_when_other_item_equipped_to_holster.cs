using System.Collections.Generic;
using Strawhenge.Inventory.Items;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests.ItemTests.PutAway
{
    public class Put_away_when_other_item_equipped_to_holster : BaseInventoryItemTest
    {
        readonly InventoryItem _knife;

        public Put_away_when_other_item_equipped_to_holster(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            AddHolster(RightHipHolster);

            var hammer = CreateItem(Hammer, new[] { RightHipHolster });
            hammer.Holsters[RightHipHolster].Do(x => x.Equip());
            hammer.HoldRightHand();

            _knife = CreateItem(Knife, new[] { RightHipHolster });
            _knife.Holsters[RightHipHolster].Do(x => x.Equip());

            hammer.PutAway(Callback);
        }

        protected override IEnumerable<(string holsterName, InventoryItem expectedItem)> ExpectedItemsInHolsters()
        {
            yield return (RightHipHolster, _knife);
        }

        protected override IEnumerable<ProcedureInfo> ExpectedProceduresCompleted()
        {
            yield return (Hammer, RightHipHolster, Show);
            yield return (Hammer, RightHipHolster, DrawRightHand);

            yield return (Knife, RightHipHolster, Show);

            yield return (Hammer, PutAwayRightHand);
        }
    }
}