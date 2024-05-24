﻿using System.Linq;
using Strawhenge.Inventory.Apparel;
using Strawhenge.Inventory.Info;
using Strawhenge.Inventory.Tests.Context;
using Xunit;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests.UnitTests
{
    public class InventoryInfoTests
    {
        const string HeadApparelSlot = "Head";
        const string TorsoApparelSlot = "Torso";

        readonly TestContext _context;
        readonly InventoryInfoGenerator _infoGenerator;

        public InventoryInfoTests(ITestOutputHelper testOutputHelper)
        {
            _context = new TestContext(testOutputHelper);
            _context.ApparelSlots.Add(new ApparelSlot(HeadApparelSlot));
            _context.ApparelSlots.Add(new ApparelSlot(TorsoApparelSlot));

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
    }
}