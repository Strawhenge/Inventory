using Strawhenge.Inventory.Items;
using System;
using System.Linq;
using FunctionalUtilities;
using Strawhenge.Inventory.Containers;

namespace Strawhenge.Inventory.TransientItems
{
    public class TransientItemLocator
    {
        readonly Hands _hands;
        readonly Holsters _holsters;
        readonly StoredItems _storedItems;
        readonly IItemGenerator _itemGenerator;

        public TransientItemLocator(
            Hands hands,
            Holsters holsters,
            StoredItems storedItems, IItemGenerator itemGenerator)
        {
            _hands = hands;
            _holsters = holsters;
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
            var left = _hands.LeftHand.CurrentItem;

            return left.HasSome(out item) && IsItemName(item, name);
        }

        bool IsItemInRightHand(string name, out Item item)
        {
            var right = _hands.RightHand.CurrentItem;

            return right.HasSome(out item) && IsItemName(item, name);
        }

        bool IsItemInHolster(string name, out Item item)
        {
            item = _holsters
                .Select(x => x.CurrentItem)
                .WhereSome()
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