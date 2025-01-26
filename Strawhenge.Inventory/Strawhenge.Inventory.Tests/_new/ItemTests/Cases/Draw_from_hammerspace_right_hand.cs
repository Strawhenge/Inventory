using System.Collections.Generic;
using FunctionalUtilities;
using Strawhenge.Common;
using Strawhenge.Inventory.Items;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests._new
{
    public class Draw_hammer_from_hammerspace_right_hand : BaseItemTest
    {
        const string Hammer = "Hammer";
        readonly Item _hammer;

        public Draw_hammer_from_hammerspace_right_hand(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            _hammer = CreateItem(Hammer);
            _hammer.HoldRightHand();
        }

        protected override Maybe<Item> ExpectedItemInRightHand => _hammer;

        protected override IEnumerable<ViewCallInfo> ExpectedViewCalls()
        {
            yield return (Hammer, x => x.DrawRightHand);
        }
    }
}