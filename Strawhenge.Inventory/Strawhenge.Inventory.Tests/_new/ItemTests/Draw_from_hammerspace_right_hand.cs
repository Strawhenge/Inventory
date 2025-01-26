using Strawhenge.Inventory.Items;
using Xunit;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests._new
{
    public class Draw_hammer_from_hammerspace_right_hand
    {
        const string Hammer = "Hammer";
        readonly InventoryTestContext _inventoryContext;
        readonly Item _hammer;
        readonly Inventory _inventory;

        public Draw_hammer_from_hammerspace_right_hand(ITestOutputHelper testOutputHelper)
        {
            _inventoryContext = new InventoryTestContext(testOutputHelper);
            _inventory = _inventoryContext.Inventory;

            _hammer = _inventoryContext.CreateItem(Hammer);
            _hammer.HoldRightHand();
        }

        [Fact]
        public void Hammer_should_be_in_right_hand()
        {
            Assert.True(
                _inventory.RightHand.IsCurrentItem(_hammer));
        }

        [Fact]
        public void Verify_view_calls()
        {
            _inventoryContext.VerifyViewCalls(
                (Hammer, x => x.DrawRightHand));
        }
    }
}