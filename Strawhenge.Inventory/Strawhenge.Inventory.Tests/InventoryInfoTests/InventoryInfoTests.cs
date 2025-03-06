using System.Linq;
using Strawhenge.Inventory.Info;
using Strawhenge.Inventory.Items;
using Xunit;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests.InventoryInfoTests
{
    public class InventoryInfoTests
    {
        const string HeadApparelSlot = "Head";
        const string TorsoApparelSlot = "Torso";
        const string HipHolster = "Hip";
        const string BackHolster = "Back";
        const string Hat = "Hat";
        const string BodyArmour = "Body Armour";
        const string Hammer = "Hammer";
        const string Stick = "Stick";
        const string Crowbar = "Crowbar";

        readonly InventoryTestContext _context;
        readonly InventoryInfoGenerator _infoGenerator;

        public InventoryInfoTests(ITestOutputHelper testOutputHelper)
        {
            _context = new InventoryTestContext(testOutputHelper);
            _context.SetStorageCapacity(100);
            _context.AddApparelSlot(HeadApparelSlot);
            _context.AddApparelSlot(TorsoApparelSlot);
            _context.AddHolster(HipHolster);
            _context.AddHolster(BackHolster);

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
            _context
                .CreateApparel(Hat, HeadApparelSlot)
                .Equip();

            _context
                .CreateApparel(BodyArmour, TorsoApparelSlot)
                .Equip();

            var info = _infoGenerator.GenerateCurrentInfo();

            Assert.Equal(2, info.EquippedApparel.Count);

            Assert.Contains(Hat, info.EquippedApparel);
            Assert.Contains(BodyArmour, info.EquippedApparel);
        }

        [Fact]
        public void Info_should_contain_stored_items()
        {
            _context
                .CreateItem(Hammer, ItemSize.OneHanded, storable: true)
                .Storable.Do(x => x.AddToStorage());

            _context
                .CreateItem(Stick, ItemSize.TwoHanded, storable: true)
                .Storable.Do(x => x.AddToStorage());

            var info = _infoGenerator.GenerateCurrentInfo();

            Assert.Equal(2, info.Items.Count);

            var hammerInfo = Assert.Single(info.Items, x => x.ItemName == Hammer);
            Assert.True(hammerInfo.IsInStorage);
            Assert.Empty(hammerInfo.HolsterName);

            var stickInfo = Assert.Single(info.Items, x => x.ItemName == Stick);
            Assert.True(stickInfo.IsInStorage);
            Assert.Empty(stickInfo.HolsterName);
        }

        [Fact]
        public void Info_should_contain_holstered_items()
        {
            _context
                .CreateItem(Hammer, ItemSize.OneHanded, new[] { HipHolster })
                .Holsters
                .First()
                .Equip();

            _context
                .CreateItem(Stick, ItemSize.OneHanded, new[] { BackHolster })
                .Holsters
                .First()
                .Equip();

            var info = _infoGenerator.GenerateCurrentInfo();

            Assert.Equal(2, info.Items.Count);

            var hammerInfo = Assert.Single(info.Items, x => x.ItemName == Hammer);
            Assert.Equal(HipHolster, hammerInfo.HolsterName);
            Assert.False(hammerInfo.IsInStorage);

            var stickInfo = Assert.Single(info.Items, x => x.ItemName == Stick);
            Assert.Equal(BackHolster, stickInfo.HolsterName);
            Assert.False(stickInfo.IsInStorage);
        }

        [Fact]
        public void Info_should_contain_held_items()
        {
            _context
                .CreateItem(Hammer, ItemSize.OneHanded, new[] { HipHolster })
                .HoldLeftHand();

            _context
                .CreateItem(Stick, ItemSize.OneHanded, new[] { BackHolster })
                .HoldRightHand();

            var info = _infoGenerator.GenerateCurrentInfo();

            Assert.Equal(2, info.Items.Count);

            var hammerInfo = Assert.Single(info.Items, x => x.ItemName == Hammer);
            Assert.True(hammerInfo.IsInLeftHand);
            Assert.False(hammerInfo.IsInRightHand);
            Assert.Empty(hammerInfo.HolsterName);
            Assert.False(hammerInfo.IsInStorage);

            var stickInfo = Assert.Single(info.Items, x => x.ItemName == Stick);
            Assert.True(stickInfo.IsInRightHand);
            Assert.False(stickInfo.IsInLeftHand);
            Assert.Empty(stickInfo.HolsterName);
            Assert.False(stickInfo.IsInStorage);
        }

        [Fact]
        public void Info_should_not_contain_duplicate_items_that_are_both_in_hand_holster_or_storage()
        {
            var hammer = _context.CreateItem(Hammer, holsterNames: new[] { HipHolster }, storable: true);
            hammer.Storable.Do(x => x.AddToStorage());
            hammer.HoldRightHand();

            var stick = _context.CreateItem(Stick, holsterNames: new[] { BackHolster }, storable: true);
            stick.Holsters.First().Equip();
            stick.HoldLeftHand();

            var crowbar = _context.CreateItem(Crowbar, holsterNames: new[] { HipHolster }, storable: true);
            crowbar.Holsters.First().Equip();
            crowbar.Storable.Do(x => x.AddToStorage());

            var info = _infoGenerator.GenerateCurrentInfo();

            Assert.Equal(3, info.Items.Count);

            var hammerInfo = Assert.Single(info.Items, x => x.ItemName == Hammer);
            Assert.True(hammerInfo.IsInRightHand);
            Assert.True(hammerInfo.IsInStorage);
            Assert.False(hammerInfo.IsInLeftHand);
            Assert.Empty(hammerInfo.HolsterName);

            var stickInfo = Assert.Single(info.Items, x => x.ItemName == Stick);
            Assert.True(stickInfo.IsInLeftHand);
            Assert.Equal(BackHolster, stickInfo.HolsterName);
            Assert.False(stickInfo.IsInRightHand);
            Assert.False(stickInfo.IsInStorage);

            var crowbarInfo = Assert.Single(info.Items, x => x.ItemName == Crowbar);
            Assert.True(crowbarInfo.IsInStorage);
            Assert.Equal(HipHolster, crowbarInfo.HolsterName);
            Assert.False(crowbarInfo.IsInRightHand);
            Assert.False(crowbarInfo.IsInLeftHand);
        }
    }
}