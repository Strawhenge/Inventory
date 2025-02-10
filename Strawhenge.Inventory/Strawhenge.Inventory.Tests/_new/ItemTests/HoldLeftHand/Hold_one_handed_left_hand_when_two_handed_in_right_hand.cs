using System.Collections.Generic;
using FunctionalUtilities;
using Strawhenge.Inventory.Items;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests._new.ItemTests.HoldLeftHand
{
    public class Hold_one_handed_left_hand_when_two_handed_in_right_hand : BaseItemTest
    {
        readonly Item _hammer;

        public Hold_one_handed_left_hand_when_two_handed_in_right_hand(ITestOutputHelper testOutputHelper) : base(
            testOutputHelper)
        {
            var spear = CreateTwoHandedItem(Spear);
            spear.HoldRightHand();

            _hammer = CreateItem(Hammer);
            _hammer.HoldLeftHand();
        }

        protected override Maybe<Item> ExpectedItemInLeftHand => _hammer;

        protected override IEnumerable<ViewCallInfo> ExpectedViewCalls()
        {
            yield return (Spear, x => x.DrawRightHand);
            yield return (Spear, x => x.Disappear);

            yield return (Hammer, x => x.DrawLeftHand);
        }
    }
}