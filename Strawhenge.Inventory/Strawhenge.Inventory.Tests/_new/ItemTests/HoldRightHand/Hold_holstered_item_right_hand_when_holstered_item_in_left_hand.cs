using System.Collections.Generic;
using FunctionalUtilities;
using Strawhenge.Inventory.Items;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests._new.ItemTests.HoldRightHand
{
    public class Hold_holstered_item_right_hand_when_holstered_item_in_left_hand
        : BaseItemTest
    {
        readonly Item _hammer;
        readonly Item _knife;

        public Hold_holstered_item_right_hand_when_holstered_item_in_left_hand(ITestOutputHelper testOutputHelper) : base(
            testOutputHelper)
        {
            AddHolster(RightHipHolster);
            AddHolster(LeftHipHolster);

            _hammer = CreateItem(Hammer, new[] { LeftHipHolster, RightHipHolster });
            _hammer.Holsters[RightHipHolster].Do(x => x.Equip());

            _knife = CreateItem(Knife, new[] { LeftHipHolster, RightHipHolster });
            _knife.Holsters[LeftHipHolster].Do(x => x.Equip());

            _hammer.HoldLeftHand();
            _knife.HoldRightHand();
        }

        protected override Maybe<Item> ExpectedItemInLeftHand => _hammer;

        protected override Maybe<Item> ExpectedItemInRightHand => _knife;

        protected override IEnumerable<(string holsterName, IItem expectedItem)> ExpectedItemsInHolsters()
        {
            yield return (LeftHipHolster, _knife);
            yield return (RightHipHolster, _hammer);
        }

        protected override IEnumerable<ViewCallInfo> ExpectedViewCalls()
        {
            yield return (Hammer, RightHipHolster, x => x.Show);
            yield return (Knife, LeftHipHolster, x => x.Show);

            yield return (Hammer, RightHipHolster, x => x.DrawLeftHand);
            yield return (Knife, LeftHipHolster, x => x.DrawRightHand);
        }
    }
}