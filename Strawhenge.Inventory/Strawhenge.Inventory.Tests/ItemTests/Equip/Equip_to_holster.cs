using System.Collections.Generic;
using Strawhenge.Inventory.Items;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests.ItemTests.Equip
{
    public class Equip_to_holster : BaseInventoryItemTest
    {
        readonly InventoryItem _hammer;

        public Equip_to_holster(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            AddHolster(RightHipHolster);

            _hammer = CreateItem(Hammer, new[] { RightHipHolster });
            _hammer.Holsters[RightHipHolster].Do(x => x.Equip(Callback));
        }

        protected override IEnumerable<(string holsterName, InventoryItem expectedItem)> ExpectedItemsInHolsters()
        {
            yield return (RightHipHolster, _hammer);
        }

        protected override IEnumerable<ProcedureInfo> ExpectedProceduresCompleted()
        {
            yield return (Hammer, RightHipHolster, Show);
        }
    }
}