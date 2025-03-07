using System.Collections.Generic;
using Strawhenge.Inventory.Items;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests.ItemTests.PutAway
{
    public class Put_away_when_other_item_equipped_to_holster : BaseItemTest
    {
        readonly Item _knife;

        public Put_away_when_other_item_equipped_to_holster(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            AddHolster(RightHipHolster);

            var hammer = CreateItem(Hammer, new[] { RightHipHolster });
            hammer.Holsters[RightHipHolster].Do(x => x.Equip());
            hammer.HoldRightHand();

            _knife = CreateItem(Knife, new[] { RightHipHolster });
            _knife.Holsters[RightHipHolster].Do(x => x.Equip());

            hammer.PutAway();
        }

        protected override IEnumerable<(string holsterName, Item expectedItem)> ExpectedItemsInHolsters()
        {
            yield return (RightHipHolster, _knife);
        }

        protected override IEnumerable<ViewCallInfo> ExpectedViewCalls()
        {
            yield return (Hammer, RightHipHolster, x => x.Show);
            yield return (Hammer, RightHipHolster, x => x.DrawRightHand);

            yield return (Knife, RightHipHolster, x => x.Show);

            yield return (Hammer, x => x.PutAwayRightHand);
        }
    }
}