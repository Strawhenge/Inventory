using System.Collections.Generic;
using FunctionalUtilities;
using Strawhenge.Inventory.Items;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests._new.ItemTests.HoldRightHand
{
    public class Hold_when_another_item_in_hand : BaseItemTest
    {
        readonly Item _hammer;
        readonly Item _spear;

        public Hold_when_another_item_in_hand(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            _hammer = CreateItem(Hammer);
            _hammer.HoldRightHand();

            _spear = CreateTwoHandedItem(Spear);
            _spear.HoldRightHand();
        }

        protected override Maybe<Item> ExpectedItemInRightHand => _spear;

        protected override IEnumerable<ViewCallInfo> ExpectedViewCalls()
        {
            yield return (Hammer, x => x.DrawRightHand);
            yield return (Hammer, x => x.Disappear);

            yield return (Spear, x => x.DrawRightHand);
        }
    }
}