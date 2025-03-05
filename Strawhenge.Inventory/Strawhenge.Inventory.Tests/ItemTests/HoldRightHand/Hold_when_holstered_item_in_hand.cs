using System.Collections.Generic;
using FunctionalUtilities;
using Strawhenge.Inventory.Items;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests.ItemTests.HoldRightHand
{
    public class Hold_when_holstered_item_in_hand : BaseItemTest
    {
        readonly Item _hammer;
        readonly Item _spear;

        public Hold_when_holstered_item_in_hand(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            AddHolster(RightHipHolster);

            _hammer = CreateItem(Hammer, new[] { RightHipHolster });
            _hammer.Holsters[RightHipHolster].Do(x => x.Equip());
            _hammer.HoldRightHand();

            _spear = CreateTwoHandedItem(Spear);
            _spear.HoldRightHand();
        }

        protected override Maybe<Item> ExpectedItemInRightHand => _spear;

        protected override IEnumerable<(string holsterName, Item expectedItem)> ExpectedItemsInHolsters()
        {
            yield return (RightHipHolster, _hammer);
        }

        protected override IEnumerable<ViewCallInfo> ExpectedViewCalls()
        {
            yield return (Hammer, RightHipHolster, x => x.Show);
            yield return (Hammer, RightHipHolster, x => x.DrawRightHand);
            yield return (Hammer, RightHipHolster, x => x.PutAwayRightHand);

            yield return (Spear, x => x.DrawRightHand);
        }
    }
}