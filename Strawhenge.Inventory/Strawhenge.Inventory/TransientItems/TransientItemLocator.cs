using Strawhenge.Inventory.Items;
using System;
using System.Linq;
using FunctionalUtilities;

namespace Strawhenge.Inventory.TransientItems
{
    public class TransientItemLocator : ITransientItemLocator
    {
        private readonly IEquippedItems equippedItems;
        private readonly IItemInventory inventory;
        private readonly IItemGenerator itemGenerator;

        public TransientItemLocator(IEquippedItems equippedItems, IItemInventory inventory, IItemGenerator itemGenerator)
        {
            this.equippedItems = equippedItems;
            this.inventory = inventory;
            this.itemGenerator = itemGenerator;
        }

        public Maybe<IItem> GetItemByName(string name)
        {
            if (IsItemInLeftHand(name, out var item))
                return Maybe.Some(item);

            if (IsItemInRightHand(name, out item))
                return Maybe.Some(item);

            if (IsItemInHolster(name, out item))
                return Maybe.Some(item);

            if (IsItemInInventory(name, out item))
                return Maybe.Some(item);

            return TryGenerateItem(name);
        }

        private Maybe<IItem> TryGenerateItem(string name)
        {
            var item = itemGenerator.GenerateByName(name);

            item.Do(x => x.ClearFromHandsPreference = ClearFromHandsPreference.Disappear);

            return item;
        }

        private bool IsItemInLeftHand(string name, out IItem item)
        {
            var left = equippedItems.GetItemInLeftHand();

            return left.HasSome(out item) && IsItemName(item, name);
        }

        private bool IsItemInRightHand(string name, out IItem item)
        {
            var right = equippedItems.GetItemInRightHand();

            return right.HasSome(out item) && IsItemName(item, name);
        }

        private bool IsItemInHolster(string name, out IItem item)
        {
            item = equippedItems.GetItemsInHolsters()
                .FirstOrDefault(x => IsItemName(x, name));

            return item != null;
        }

        private bool IsItemInInventory(string name, out IItem item)
        {
            item = inventory.AllItems
                .FirstOrDefault(x => IsItemName(x, name));

            return item != null;
        }

        private bool IsItemName(IItem item, string name) =>
            item.Name.Equals(name, StringComparison.OrdinalIgnoreCase);
    }
}
