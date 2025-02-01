using System.Collections.Generic;
using Strawhenge.Inventory.Items;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests._new
{
    public class Equip_to_holster : BaseItemTest
    {
        readonly Item _hammer;

        public Equip_to_holster(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            AddHolster(RightHip);

            _hammer = CreateItem(Hammer, new[] { RightHip });
            _hammer.Holsters[RightHip].Do(x => x.Equip());
        }

        protected override IEnumerable<(string holsterName, IItem expectedItem)> ExpectedItemsInHolsters()
        {
            yield return (RightHip, _hammer);
        }

        protected override IEnumerable<ViewCallInfo> ExpectedViewCalls()
        {
            yield return (Hammer, RightHip, x => x.Show);
        }
    }
}