using System.Collections.Generic;
using Strawhenge.Inventory.Items;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests._new
{
    public class Draw_from_holster_then_put_away : BaseItemTest
    {
        readonly Item _hammer;

        public Draw_from_holster_then_put_away(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            AddHolster(RightHip);

            _hammer = CreateItem(Hammer, new[] { RightHip });
            _hammer.Holsters[RightHip].Do(x => x.Equip());
            _hammer.HoldRightHand();
            _hammer.PutAway();
        }

        protected override IEnumerable<(string holsterName, IItem expectedItem)> ExpectedItemsInHolsters()
        {
            yield return (RightHip, _hammer);
        }

        protected override IEnumerable<ViewCallInfo> ExpectedViewCalls()
        {
            yield return (Hammer, RightHip, x => x.Show);
            yield return (Hammer, RightHip, x => x.DrawRightHand);
            yield return (Hammer, RightHip, x => x.PutAwayRightHand);
        }
    }
}