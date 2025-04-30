using System;
using System.Collections.Generic;
using Strawhenge.Inventory.Apparel;
using Strawhenge.Inventory.Effects;
using Strawhenge.Inventory.Items;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests
{
    class InventoryTestContext
    {
        readonly ProcedureTracker _procedureTracker;
        readonly ApparelViewCallTracker _apparelViewTracker;
        readonly ItemRepositoryFake _itemRepository;

        public InventoryTestContext(ITestOutputHelper testOutputHelper)
        {
            var logger = new TestOutputLogger(testOutputHelper);
            _procedureTracker = new ProcedureTracker(logger);

            _itemRepository = new ItemRepositoryFake();

            var itemProceduresFactory = new ItemProceduresFactoryFake(_procedureTracker);

            _apparelViewTracker = new ApparelViewCallTracker(logger);
            var apparelViewFactory = new ApparelViewFactoryFake(_apparelViewTracker);

            Inventory = new Inventory(
                itemProceduresFactory,
                apparelViewFactory,
                NullEffectFactoryLocator.Instance,
                _itemRepository,
                logger);
        }

        public Inventory Inventory { get; }

        public void AddHolsters(IEnumerable<string> holsters)
        {
            foreach (var holster in holsters)
                AddHolster(holster);
        }

        public void AddHolster(string name) => Inventory.Holsters.Add(name);

        public void AddApparelSlot(string name) => Inventory.ApparelSlots.Add(name);

        public void SetStorageCapacity(int capacity) => Inventory.StoredItems.SetWeightCapacity(capacity);

        public void AddToRepository(ItemData item) => _itemRepository.Add(item);

        public Item CreateItem(string name, ItemSize? size = null, string[] holsterNames = null, bool storable = false)
        {
            size ??= ItemSize.OneHanded;
            holsterNames ??= Array.Empty<string>();

            var dataBuilder = ItemDataBuilder.Create(name, size.Value, storable, 1, _ =>
            {
            });

            foreach (var holsterName in holsterNames)
            {
                dataBuilder.AddHolster(holsterName, _ =>
                {
                });
            }

            var data = dataBuilder.Build();

            return Inventory.CreateItem(data);
        }

        public Item CreateTransientItem(string name, ItemSize? size = null)
        {
            size ??= ItemSize.OneHanded;

            var data = ItemDataBuilder
                .Create(name, size.Value, false, 1, _ =>
                {
                })
                .Build();

            return Inventory.CreateTemporaryItem(data);
        }

        public ApparelPiece CreateApparel(string name, string slotName)
        {
            var data = ApparelPieceDataBuilder
                .Create(name, slotName, Array.Empty<EffectData>(), _ =>
                {
                })
                .Build();

            return Inventory.CreateApparelPiece(data);
        }

        public void VerifyProcedures(params ProcedureInfo[] expectedProcedures) =>
            _procedureTracker.VerifyProcedures(expectedProcedures);

        public void VerifyApparelViewCalls(params ApparelViewCallInfo[] expectedCalls) =>
            _apparelViewTracker.VerifyCalls(expectedCalls);
    }
}