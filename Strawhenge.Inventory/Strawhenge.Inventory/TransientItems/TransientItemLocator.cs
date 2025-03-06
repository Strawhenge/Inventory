using Strawhenge.Inventory.Items;
using System;
using System.Linq;
using FunctionalUtilities;

namespace Strawhenge.Inventory.TransientItems
{
    public class TransientItemLocator
    {
        readonly EquippedItems _equippedItems;
        readonly StoredItems _storedItems;
        readonly IItemGenerator _itemGenerator;

        public TransientItemLocator(EquippedItems equippedItems, StoredItems storedItems, IItemGenerator itemGenerator)
        {
            _equippedItems = equippedItems;
            _storedItems = storedItems;
            _itemGenerator = itemGenerator;
        }

        public Maybe<Item> GetItemByName(string name)
        {
            if (IsItemInLeftHand(name, out var item))
                return Maybe.Some(item);

            if (IsItemInRightHand(name, out item))
                return Maybe.Some(item);

            if (IsItemInHolster(name, out item))
                return Maybe.Some(item);

            if (IsItemInInventory(name, out item))
                return Maybe.Some(item);

            return _itemGenerator.GenerateByName(name);
        }

        bool IsItemInLeftHand(string name, out Item item)
        {
            var left = _equippedItems.GetItemInLeftHand();

            return left.HasSome(out item) && IsItemName(item, name);
        }

        bool IsItemInRightHand(string name, out Item item)
        {
            var right = _equippedItems.GetItemInRightHand();

            return right.HasSome(out item) && IsItemName(item, name);
        }

        bool IsItemInHolster(string name, out Item item)
        {
            item = _equippedItems.GetItemsInHolsters()
                .FirstOrDefault(x => IsItemName(x, name));

            return item != null;
        }

        bool IsItemInInventory(string name, out Item item)
        {
            item = _storedItems.Items
                .FirstOrDefault(x => IsItemName(x, name));

            return item != null;
        }

        bool IsItemName(Item item, string name) =>
            item.Name.Equals(name, StringComparison.OrdinalIgnoreCase);
    }
}