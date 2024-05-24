using System.Collections.Generic;
using System.Linq;
using Moq;
using Strawhenge.Inventory.Apparel;
using Strawhenge.Inventory.Containers;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests.Context
{
    class TestContext
    {
        public TestContext(ITestOutputHelper testOutputHelper)
        {
            var logger = new TestOutputLogger(testOutputHelper);

            var apparelSlotsMock = new Mock<IApparelSlots>();
            apparelSlotsMock.SetupGet(x => x.All).Returns(ApparelSlots);

            ItemStorage = new StoredItems();
            Holsters = new Holsters(logger);
            Inventory = new Inventory(ItemStorage, new Hands(), Holsters, apparelSlotsMock.Object);
        }

        public StoredItems ItemStorage { get; }

        public Holsters Holsters { get; }

        public List<ApparelSlot> ApparelSlots { get; } = new List<ApparelSlot>();

        public IInventory Inventory { get; }

        public IApparelPiece CreateApparelPiece(string name, string slotName)
        {
            return new ApparelPiece(
                name,
                ApparelSlots.First(x => x.Name == slotName),
                new NullApparelView());
        }
    }
}