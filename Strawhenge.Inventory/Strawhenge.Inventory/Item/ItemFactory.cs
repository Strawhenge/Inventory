using System;
using System.Linq;
using Strawhenge.Inventory.Containers;
using Strawhenge.Inventory.Items.Holsters;

namespace Strawhenge.Inventory.Items
{
    class ItemFactory
    {
        readonly Hands _hands;
        readonly Containers.Holsters _holsters;
        readonly StoredItems _storedItems;
        readonly IItemViewFactory _itemViewFactory;
        readonly IHolsterForItemViewFactory _holsterForItemViewFactory;

        public ItemFactory(
            Hands hands,
            Containers.Holsters holsters,
            StoredItems storedItems,
            IItemViewFactory itemViewFactory,
            IHolsterForItemViewFactory holsterForItemViewFactory)
        {
            _hands = hands;
            _holsters = holsters;
            _storedItems = storedItems;
            _itemViewFactory = itemViewFactory;
            _holsterForItemViewFactory = holsterForItemViewFactory;
        }

        public Item Create(ItemData data)
        {
            var view = _itemViewFactory.Create(data);
            
            var item = new Item(
                data.Name,
                _hands,
                view,
                data.Size
            );

            var holstersForItem = data.Holsters
                .Select(holsterData =>
                {
                    return _holsters
                        .FindByName(holsterData.HolsterName)
                        .Map(container =>
                        {
                            var holsterForItemView = _holsterForItemViewFactory.Create(holsterData);
                            return new HolsterForItem(item, container, holsterForItemView);
                        });
                })
                .SelectMany(x => x.AsEnumerable());

            item.SetupHolsters(new HolstersForItem(holstersForItem));

            if (data.IsStorable)
                item.SetupStorable(_storedItems, data.Weight);

            return item;
        }
    }

    public interface IItemViewFactory
    {
        IItemView Create(ItemData data);
    }

    public interface IHolsterForItemViewFactory
    {
        IHolsterForItemView Create(HolsterItemData data);
    }
}