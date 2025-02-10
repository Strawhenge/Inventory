using System.Collections.Generic;
using FunctionalUtilities;
using Strawhenge.Inventory.Items;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests._new
{
    public class Hold_one_handed_left_hand_when_two_handed_in_right_hand : BaseItemTest
    {
        readonly Item _spear;
        readonly Item _hammer;

        public Hold_one_handed_left_hand_when_two_handed_in_right_hand(ITestOutputHelper testOutputHelper) : base(
            testOutputHelper)
        {
            _spear = CreateTwoHandedItem(Spear);
            _spear.HoldRightHand();

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