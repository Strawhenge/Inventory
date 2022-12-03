using Strawhenge.Inventory.Unity.Data;
using Strawhenge.Inventory.Unity.Items;
using Strawhenge.Inventory.Unity.Monobehaviours;
using System.Collections.Generic;
using FunctionalUtilities;

namespace Strawhenge.Inventory.Unity
{
    public class ItemManager : IItemManager
    {
        private readonly IEquippedItems equippedItems;
        private readonly IItemInventory inventory;
        private readonly IItemFactory itemFactory;

        public ItemManager(IEquippedItems equippedItems, IItemInventory inventory, IItemFactory itemFactory)
        {
            this.equippedItems = equippedItems;
            this.inventory = inventory;
            this.itemFactory = itemFactory;
        }

        public Maybe<IItem> ItemInLeftHand => equippedItems.GetItemInLeftHand();

        public Maybe<IItem> ItemInRightHand => equippedItems.GetItemInRightHand();

        public IEnumerable<IItem> ItemsInHolsters => equippedItems.GetItemsInHolsters();

        public IEnumerable<IItem> ItemsInInventory => inventory.AllItems;

        public IItem AddItemToInventory(ItemScript itemScript)
        {
            var item = itemFactory.Create(itemScript);
            return AddItemToInventory(item);
        }

        public IItem AddItemToInventory(IItemData itemData)
        {
            var item = itemFactory.Create(itemData);
            return AddItemToInventory(item);
        }

        public IItem AddItemToInventory(IItem item)
        {
            inventory.Add(item);
            return item;
        }

        public void RemoveItemFromInventory(IItem item)
        {
            inventory.Remove(item);
        }

        public IItem Manage(ItemScript itemScript)
        {
            return itemFactory.Create(itemScript);
        }

        public IItem Manage(IItemData itemData)
        {
            return itemFactory.Create(itemData);
        }
    }
}
