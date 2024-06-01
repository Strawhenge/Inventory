using System;
using System.Collections.Generic;
using System.Linq;
using FunctionalUtilities;
using Moq;
using Strawhenge.Common.Logging;
using Strawhenge.Inventory.Apparel;
using Strawhenge.Inventory.Containers;
using Strawhenge.Inventory.Items;
using Strawhenge.Inventory.Items.Consumables;
using Strawhenge.Inventory.Items.HolsterForItem;
using Strawhenge.Inventory.Items.Storables;
using Xunit.Abstractions;

namespace Strawhenge.Inventory.Tests.Context
{
    class TestContext
    {
        readonly ILogger _logger;

        public TestContext(ITestOutputHelper testOutputHelper)
        {
            _logger = new TestOutputLogger(testOutputHelper);

            var apparelSlotsMock = new Mock<IApparelSlots>();
            apparelSlotsMock.SetupGet(x => x.All).Returns(ApparelSlots);

            ItemStorage = new StoredItems();
            Holsters = new Holsters(_logger);
            Hands = new Hands();
            Inventory = new Inventory(ItemStorage, Hands, Holsters, apparelSlotsMock.Object);
        }

        public StoredItems ItemStorage { get; }

        public Holsters Holsters { get; }

        public Hands Hands { get; }

        public List<ApparelSlot> ApparelSlots { get; } = new List<ApparelSlot>();

        public IInventory Inventory { get; }

        public IApparelPiece CreateApparelPiece(string name, string slotName)
        {
            return new ApparelPiece(
                name,
                ApparelSlots.First(x => x.Name == slotName),
                new NullApparelView());
        }

        public IItem CreateItem(string name, ItemSize size, params string[] holsterNames)
        {
            var itemViewMock = new Mock<IItemView>();
            itemViewMock.Setup(x => x.Disappear(It.IsAny<Action>())).Callback<Action>(callback => callback?.Invoke());
            itemViewMock.Setup(x => x.DrawLeftHand(It.IsAny<Action>()))
                .Callback<Action>(callback => callback?.Invoke());
            itemViewMock.Setup(x => x.DrawRightHand(It.IsAny<Action>()))
                .Callback<Action>(callback => callback?.Invoke());
            itemViewMock.Setup(x => x.DropLeftHand(It.IsAny<Action>()))
                .Callback<Action>(callback => callback?.Invoke());
            itemViewMock.Setup(x => x.DropRightHand(It.IsAny<Action>()))
                .Callback<Action>(callback => callback?.Invoke());
            itemViewMock.Setup(x => x.LeftHandToRightHand(It.IsAny<Action>()))
                .Callback<Action>(callback => callback?.Invoke());
            itemViewMock.Setup(x => x.PutAwayLeftHand(It.IsAny<Action>()))
                .Callback<Action>(callback => callback?.Invoke());
            itemViewMock.Setup(x => x.PutAwayRightHand(It.IsAny<Action>()))
                .Callback<Action>(callback => callback?.Invoke());
            itemViewMock.Setup(x => x.RightHandToLeftHand(It.IsAny<Action>()))
                .Callback<Action>(callback => callback?.Invoke());
            itemViewMock.Setup(x => x.SpawnAndDrop(It.IsAny<Action>()))
                .Callback<Action>(callback => callback?.Invoke());


            return new Item(name, Hands, itemViewMock.Object, size, item =>
                {
                    return new HolstersForItem(
                        holsterNames.Select(x =>
                        {
                            var view = new Mock<IHolsterForItemView>();

                            view.Setup(x => x.DrawLeftHand(It.IsAny<Action>()))
                                .Callback<Action>(callback => callback?.Invoke());
                            view.Setup(x => x.DrawRightHand(It.IsAny<Action>()))
                                .Callback<Action>(callback => callback?.Invoke());
                            view.Setup(x => x.PutAwayLeftHand(It.IsAny<Action>()))
                                .Callback<Action>(callback => callback?.Invoke());
                            view.Setup(x => x.PutAwayRightHand(It.IsAny<Action>()))
                                .Callback<Action>(callback => callback?.Invoke());
                            view.Setup(x => x.Show(It.IsAny<Action>()))
                                .Callback<Action>(callback => callback?.Invoke());
                            view.Setup(x => x.Hide(It.IsAny<Action>()))
                                .Callback<Action>(callback => callback?.Invoke());
                            view.Setup(x => x.Drop(It.IsAny<Action>()))
                                .Callback<Action>(callback => callback?.Invoke());

                            return new HolsterForItem(item, Holsters.FindByName(x).ReduceUnsafe(), view.Object);
                        }),
                        _logger);
                },
                _ => Maybe.None<IConsumable>(),
                _ => Maybe.None<IStorable>());
        }
    }
}