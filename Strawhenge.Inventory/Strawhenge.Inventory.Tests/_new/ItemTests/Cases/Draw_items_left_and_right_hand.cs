using System.Collections.Generic;
using FunctionalUtilities;
using Strawhenge.Inventory.Items;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests._new
{
    public class Draw_items_left_and_right_hand : BaseItemTest
    {
        readonly Item _hammer;
        readonly Item _knife;

        public Draw_items_left_and_right_hand(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            _hammer = CreateItem(Hammer);
            _hammer.HoldRightHand();

            _knife = CreateItem(Knife);
            _knife.HoldLeftHand();
        }

        protected override Maybe<Item> ExpectedItemInLeftHand => _knife;

        protected override Maybe<Item> ExpectedItemInRightHand => _hammer;

        protected override IEnumerable<ViewCallInfo> ExpectedViewCalls()
        {
            yield return (Hammer, x => x.DrawRightHand);
            yield return (Knife, x => x.DrawLeftHand);
        }
    }
}