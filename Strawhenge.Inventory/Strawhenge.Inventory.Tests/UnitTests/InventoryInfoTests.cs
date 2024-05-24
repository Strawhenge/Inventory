using System.Linq;
using Strawhenge.Inventory.Apparel;
using Strawhenge.Inventory.Info;
using Strawhenge.Inventory.Items;
using Strawhenge.Inventory.Tests.Context;
using Xunit;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests.UnitTests
{
    public class InventoryInfoTests
    {
        const string HeadApparelSlot = "Head";
        const string TorsoApparelSlot = "Torso";
        const string HipHolster = "Hip";
        const string BackHolster = "Back";

        readonly TestContext _context;
        readonly InventoryInfoGenerator _infoGenerator;

        public InventoryInfoTests(ITestOutputHelper testOutputHelper)
        {
            _context = new TestContext(testOutputHelper);
            _context.ApparelSlots.Add(new ApparelSlot(HeadApparelSlot));
            _context.ApparelSlots.Add(new ApparelSlot(TorsoApparelSlot));
            _context.Holsters.Add(HipHolster);
            _context.Holsters.Add(BackHolster);

            _infoGenerator = new InventoryInfoGenerator(_context.Inventory);
        }

        [Fact]
        public void Info_should_be_empty_when_inventory_in_default_state()
        {
            var info = _infoGenerator.GenerateCurrentInfo();

            Assert.Empty(info.Items);
            Assert.Empty(info.EquippedApparel);
        }

        [Fact]
        public void Info_should_contain_equipped_apparel()
        {
            const string hat = "Hat";
            const string bodyArmour = "Body Armour";

            _context.CreateApparelPiece(hat, HeadApparelSlot)
                .Equip();
            _context.CreateApparelPiece(bodyArmour, TorsoApparelSlot)
                .Equip();

            var info = _infoGenerator.GenerateCurrentInfo();

            Assert.Equal(2, info.EquippedApparel.Count);

            Assert.Contains(hat, info.EquippedApparel);
            Assert.Contains(bodyArmour, info.EquippedApparel);
        }

        [Fact]
        public void Info_should_contain_stored_items()
        {
            const string hammer = "Hammer";
            const string stick = "Stick";

            _context.ItemStorage.Add(
                _context.CreateItem(hammer, ItemSize.OneHanded));
            _context.ItemStorage.Add(
                _context.CreateItem(stick, ItemSize.TwoHanded));

            var info = _infoGenerator.GenerateCurrentInfo();

            Assert.Equal(2, info.Items.Count);

            var hammerInfo = Assert.Single(info.Items, x => x.ItemName == hammer);
            Assert.True(hammerInfo.IsInStorage);
            Assert.Empty(hammerInfo.HolsterName);

            var stickInfo = Assert.Single(info.Items, x => x.ItemName == stick);
            Assert.True(stickInfo.IsInStorage);
            Assert.Empty(stickInfo.HolsterName);
        }

        [Fact]
        public void Info_should_contain_holstered_items()
        {
            const string hammer = "Hammer";
            const string stick = "Stick";

            _context.CreateItem(hammer, ItemSize.OneHanded, HipHolster).Holsters.First().Equip();
            _context.CreateItem(stick, ItemSize.OneHanded, BackHolster).Holsters.First().Equip();

            var info = _infoGenerator.GenerateCurrentInfo();

            Assert.Equal(2, info.Items.Count);

            var hammerInfo = Assert.Single(info.Items, x => x.ItemName == hammer);
            Assert.Equal(HipHolster, hammerInfo.HolsterName);
            Assert.False(hammerInfo.IsInStorage);

            var stickInfo = Assert.Single(info.Items, x => x.ItemName == stick);
            Assert.Equal(BackHolster, stickInfo.HolsterName);
            Assert.False(stickInfo.IsInStorage);
        }
    }
}