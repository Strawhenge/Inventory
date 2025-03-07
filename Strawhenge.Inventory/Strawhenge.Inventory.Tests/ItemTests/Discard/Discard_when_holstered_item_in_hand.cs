using System.Collections.Generic;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests.ItemTests.Discard
{
    public class Discard_when_holstered_item_in_hand : BaseItemTest
    {
        public Discard_when_holstered_item_in_hand(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            AddHolster(RightHipHolster);

            var hammer = CreateItem(Hammer, new[] { RightHipHolster });
            hammer.Holsters[RightHipHolster].Do(x => x.Equip());
            hammer.HoldRightHand();
            hammer.Discard();
        }

        protected override IEnumerable<ViewCallInfo> ExpectedViewCalls()
        {
            yield return (Hammer, RightHipHolster, x => x.Show);
            yield return (Hammer, RightHipHolster, x => x.DrawRightHand);
            yield return (Hammer, x => x.DisappearRightHand);
        }
    }
}