using System.Collections.Generic;
using Strawhenge.Inventory.Items;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests._new.ItemTests.Discard
{
    public class Discard_when_holstered_item_in_hand : BaseItemTest
    {
        readonly Item _hammer;

        public Discard_when_holstered_item_in_hand(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            AddHolster(RightHipHolster);

            _hammer = CreateItem(Hammer, new[] { RightHipHolster });
            _hammer.Holsters[RightHipHolster].Do(x => x.Equip());
            _hammer.HoldRightHand();
            _hammer.Discard();
        }

        protected override IEnumerable<ViewCallInfo> ExpectedViewCalls()
        {
            yield return (Hammer, RightHipHolster, x => x.Show);
            yield return (Hammer, RightHipHolster, x => x.DrawRightHand);
            yield return (Hammer, x => x.Disappear);
        }
    }
}