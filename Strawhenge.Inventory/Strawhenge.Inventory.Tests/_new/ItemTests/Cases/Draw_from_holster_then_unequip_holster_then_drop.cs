using System.Collections.Generic;
using Strawhenge.Inventory.Items;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests._new
{
    public class Draw_from_holster_then_unequip_holster_then_drop : BaseItemTest
    {
        readonly Item _hammer;

        public Draw_from_holster_then_unequip_holster_then_drop(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            AddHolster(RightHip);
            
            _hammer = CreateItem(Hammer, new[] { RightHip });
            _hammer.Holsters[RightHip].Do(x => x.Equip());
            _hammer.HoldRightHand();
            _hammer.UnequipFromHolster();
            _hammer.Drop();
        }

        protected override IEnumerable<ViewCallInfo> ExpectedViewCalls()
        {
            yield return (Hammer, RightHip, x => x.Show);
            yield return (Hammer, RightHip, x => x.DrawRightHand);
            yield return (Hammer, x => x.DropRightHand);
        }
    }
}