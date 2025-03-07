using System.Collections.Generic;
using Strawhenge.Inventory.Items;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests.ItemTests.Equip
{
    public class Equip_to_holster_when_other_item_in_holster : BaseItemTest
    {
        readonly Item _knife;

        public Equip_to_holster_when_other_item_in_holster(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            AddHolster(RightHipHolster);

            var hammer = CreateItem(Hammer, new[] { RightHipHolster });
            hammer.Holsters[RightHipHolster].Do(x => x.Equip());

            _knife = CreateItem(Knife, new[] { RightHipHolster });
            _knife.Holsters[RightHipHolster].Do(x => x.Equip());
        }

        protected override IEnumerable<(string holsterName, Item expectedItem)> ExpectedItemsInHolsters()
        {
            yield return (RightHipHolster, _knife);
        }

        protected override IEnumerable<ViewCallInfo> ExpectedViewCalls()
        {
            yield return (Hammer, RightHipHolster, x => x.Show);
            yield return (Hammer, RightHipHolster, x => x.Drop);

            yield return (Knife, RightHipHolster, x => x.Show);
        }
    }
}