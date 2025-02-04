using System.Collections.Generic;
using Strawhenge.Inventory.Items;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests._new
{
    public class Put_away_when_equipped_to_new_holster : BaseItemTest
    {
        readonly Item _hammer;

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

        protected override IEnumerable<(string holsterName, IItem expectedItem)> ExpectedItemsInHolsters()
        {
            yield return (LeftHipHolster, _hammer);
        }

        protected override IEnumerable<ViewCallInfo> ExpectedViewCalls()
        {
            yield return (Hammer, RightHipHolster, x => x.Show);
            yield return (Hammer, RightHipHolster, x => x.DrawRightHand);

            yield return (Hammer, LeftHipHolster, x => x.PutAwayRightHand);
        }
    }
}