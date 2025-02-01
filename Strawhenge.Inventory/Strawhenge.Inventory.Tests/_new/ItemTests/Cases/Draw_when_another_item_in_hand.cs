using System.Collections.Generic;
using FunctionalUtilities;
using Strawhenge.Inventory.Items;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests._new
{
    public class Draw_when_another_item_in_hand : BaseItemTest
    {
        readonly Item _hammer;
        readonly Item _spear;

        public Draw_when_another_item_in_hand(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            AddHolster(RightHip);

            _hammer = CreateItem(Hammer, new[] { RightHip });
            _hammer.Holsters[RightHip].Do(x => x.Equip());
            _hammer.HoldRightHand();

            _spear = CreateTwoHandedItem(Spear);
            _spear.HoldRightHand();
        }

        protected override Maybe<Item> ExpectedItemInRightHand => _spear;

        protected override IEnumerable<(string holsterName, IItem expectedItem)> ExpectedItemsInHolsters()
        {
            yield return (RightHip, _hammer);
        }

        protected override IEnumerable<ViewCallInfo> ExpectedViewCalls()
        {
            yield return (Hammer, RightHip, x => x.Show);
            yield return (Hammer, RightHip, x => x.DrawRightHand);
            yield return (Hammer, RightHip, x => x.PutAwayRightHand);

            yield return (Spear, x => x.DrawRightHand);
        }
    }
}