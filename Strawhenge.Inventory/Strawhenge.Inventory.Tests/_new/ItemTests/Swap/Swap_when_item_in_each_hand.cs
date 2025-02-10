using System.Collections.Generic;
using FunctionalUtilities;
using Strawhenge.Inventory.Items;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests._new.ItemTests.Swap
{
    public class Swap_when_item_in_each_hand : BaseItemTest
    {
        readonly Item _hammer;
        readonly Item _knife;

        public Swap_when_item_in_each_hand(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            _hammer = CreateItem(Hammer);
            _hammer.HoldRightHand();

            _knife = CreateItem(Knife);
            _knife.HoldLeftHand();

            Inventory.SwapHands();
        }

        protected override Maybe<Item> ExpectedItemInLeftHand => _hammer;

        protected override Maybe<Item> ExpectedItemInRightHand => _knife;

        protected override IEnumerable<ViewCallInfo> ExpectedViewCalls()
        {
            yield return (Hammer, x => x.DrawRightHand);
            yield return (Knife, x => x.DrawLeftHand);

            // TODO This sequence isn't quite right
            yield return (Knife, x => x.Disappear);
            yield return (Hammer, x => x.RightHandToLeftHand);
            yield return (Knife, x => x.DrawRightHand);
        }
    }
}