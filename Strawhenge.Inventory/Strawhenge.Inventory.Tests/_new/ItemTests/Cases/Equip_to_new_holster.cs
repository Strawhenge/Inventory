using System.Collections.Generic;
using Strawhenge.Inventory.Items;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests._new
{
    public class Equip_to_new_holster : BaseItemTest
    {
        readonly Item _hammer;

        public Equip_to_new_holster(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            AddHolster(LeftHip);
            AddHolster(RightHip);

            _hammer = CreateItem(Hammer, new[] { LeftHip, RightHip });
            _hammer.Holsters[RightHip].Do(x => x.Equip());
            _hammer.Holsters[LeftHip].Do(x => x.Equip());
        }

        protected override IEnumerable<(string holsterName, IItem expectedItem)> ExpectedItemsInHolsters()
        {
            yield return (LeftHip, _hammer);
        }

        protected override IEnumerable<ViewCallInfo> ExpectedViewCalls()
        {
            yield return (Hammer, RightHip, x => x.Show);
            yield return (Hammer, RightHip, x => x.Hide);

            yield return (Hammer, LeftHip, x => x.Show);
        }
    }
}