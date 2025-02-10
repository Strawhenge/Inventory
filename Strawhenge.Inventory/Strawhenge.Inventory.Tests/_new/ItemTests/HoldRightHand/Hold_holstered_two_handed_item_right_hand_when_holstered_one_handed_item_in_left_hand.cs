using System.Collections.Generic;
using FunctionalUtilities;
using Strawhenge.Inventory.Items;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests._new.ItemTests.HoldRightHand
{
    public class Hold_holstered_two_handed_item_right_hand_when_holstered_one_handed_item_in_left_hand
        : BaseItemTest
    {
        readonly Item _hammer;
        readonly Item _spear;

        public Hold_holstered_two_handed_item_right_hand_when_holstered_one_handed_item_in_left_hand(
            ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            AddHolster(RightHipHolster);
            AddHolster(BackHolster);

            _hammer = CreateItem(Hammer, new[] { RightHipHolster });
            _hammer.Holsters[RightHipHolster].Do(x => x.Equip());

            _spear = CreateTwoHandedItem(Spear, new[] { BackHolster });
            _spear.Holsters[BackHolster].Do(x => x.Equip());

            _hammer.HoldLeftHand();
            _spear.HoldRightHand();
        }

        protected override Maybe<Item> ExpectedItemInRightHand => _spear;

        protected override IEnumerable<(string holsterName, IItem expectedItem)> ExpectedItemsInHolsters()
        {
            yield return (RightHipHolster, _hammer);
            yield return (BackHolster, _spear);
        }

        protected override IEnumerable<ViewCallInfo> ExpectedViewCalls()
        {
            yield return (Hammer, RightHipHolster, x => x.Show);
            yield return (Spear, BackHolster, x => x.Show);

            yield return (Hammer, RightHipHolster, x => x.DrawLeftHand);
            yield return (Hammer, RightHipHolster, x => x.PutAwayLeftHand);

            yield return (Spear, BackHolster, x => x.DrawRightHand);
        }
    }
}