using Strawhenge.Inventory.Info;
using Strawhenge.Inventory.Tests.Context;
using Xunit;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests.UnitTests
{
    public class InventoryInfoTests
    {
        readonly TestContext _context;
        readonly InventoryInfoGenerator _infoGenerator;

        public InventoryInfoTests(ITestOutputHelper testOutputHelper)
        {
            _context = new TestContext(testOutputHelper);
            _infoGenerator = new InventoryInfoGenerator(_context.Inventory);
        }

        [Fact]
        public void Info_should_be_empty_when_inventory_in_default_state()
        {
            var info = _infoGenerator.GenerateCurrentInfo();

            Assert.NotNull(info);
            Assert.Empty(info.Items);
            Assert.Empty(info.Apparel);
        }
    }
}