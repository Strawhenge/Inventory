using Strawhenge.Inventory.Unity.Data;
using Strawhenge.Inventory.Unity.Items;
using Strawhenge.Inventory.Unity.Monobehaviours;
using System.Collections.Generic;
using FunctionalUtilities;

namespace Strawhenge.Inventory.Unity
{
    public class ItemManager : IItemManager
    {
        readonly IEquippedItems _equippedItems;
        readonly IItemInventory _inventory;
        readonly IItemFactory _itemFactory;

        public ItemManager(IEquippedItems equippedItems, IItemInventory inventory, IItemFactory itemFactory)
        {
            _equippedItems = equippedItems;
            _inventory = inventory;
            _itemFactory = itemFactory;
        }

        public Maybe<IItem> ItemInLeftHand => _equippedItems.GetItemInLeftHand();

        public Maybe<IItem> ItemInRightHand => _equippedItems.GetItemInRightHand();

        public IEnumerable<IItem> ItemsInHolsters => _equippedItems.GetItemsInHolsters();

        public IEnumerable<IItem> ItemsInInventory => _inventory.AllItems;

        public IItem AddItemToInventory(ItemScript itemScript)
        {
            var item = _itemFactory.Create(itemScript);
            return AddItemToInventory(item);
        }

        public IItem AddItemToInventory(IItemData itemData)
        {
            var item = _itemFactory.Create(itemData);
            return AddItemToInventory(item);
        }

        public IItem AddItemToInventory(IItem item)
        {
            _inventory.Add(item);
            return item;
        }

        public void RemoveItemFromInventory(IItem item)
        {
            _inventory.Remove(item);
        }

        public IItem Manage(ItemScript itemScript)
        {
            return _itemFactory.Create(itemScript);
        }

        public IItem Manage(IItemData itemData)
        {
            return _itemFactory.Create(itemData);
        }
    }
}
