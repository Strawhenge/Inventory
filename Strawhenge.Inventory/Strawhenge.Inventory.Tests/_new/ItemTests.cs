using System.Linq;
using Strawhenge.Inventory.Items;
using Strawhenge.Inventory.Items.HolsterForItem;
using Xunit;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests._new
{
    public class ItemTests
    {
        readonly InventoryTestContext _inventoryContext;

        public ItemTests(ITestOutputHelper testOutputHelper)
        {
            _inventoryContext = new InventoryTestContext(testOutputHelper);
        }

        [Fact]
        public void Test()
        {
            var item = _inventoryContext.CreateItem("Hammer", new[] { "Right Hip" });

            item.HoldLeftHand();
            item.Holsters["Right Hip"].Do(holster => holster.Equip());
            item.PutAway();

            _inventoryContext.VerifyViewCalls(
                (itemName: "Hammer", methodInvocation: x => x.DrawLeftHand),
                (itemName: "Hammer", holsterName: "Right Hip", methodInvocation: x => x.PutAwayLeftHand));
        }
    }
}