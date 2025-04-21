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

        public ItemFactory(
            Hands hands,
            Containers.Holsters holsters,
            StoredItems storedItems)
        {
            _hands = hands;
            _holsters = holsters;
            _storedItems = storedItems;
        }

        public Item Create(ItemData data)
        {
            throw new NotImplementedException();
        }
    }
}