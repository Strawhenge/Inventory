using System.Collections.Generic;
using FunctionalUtilities;
using Strawhenge.Inventory.Items;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests.ItemTests.Unequip
{
    public class Unequip_from_holster_when_in_hand : BaseItemTest
    {
        readonly InventoryItem _hammer;

        public Unequip_from_holster_when_in_hand(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            AddHolster(RightHipHolster);

            _hammer = CreateItem(Hammer, new[] { RightHipHolster });
            _hammer.Holsters[RightHipHolster].Do(x => x.Equip());
            _hammer.HoldRightHand();
            _hammer.Holsters[RightHipHolster].Do(x => x.Unequip());
        }

        protected override Maybe<InventoryItem> ExpectedItemInRightHand => _hammer;

        protected override IEnumerable<ProcedureInfo> ExpectedProceduresCompleted()
        {
            yield return (Hammer, RightHipHolster, Show);
            yield return (Hammer, RightHipHolster, DrawRightHand);
        }
    }
}