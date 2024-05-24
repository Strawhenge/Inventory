using System;
using System.Collections.Generic;
using System.Linq;

namespace Strawhenge.Inventory.Info
{
    public class InventoryInfoGenerator
    {
        readonly IInventory _inventory;

        public InventoryInfoGenerator(IInventory inventory)
        {
            _inventory = inventory;
        }

        public InventoryInfo GenerateCurrentInfo()
        {
            var equippedApparel = _inventory.ApparelSlots
                .Select(x => x.CurrentPiece.HasSome(out var apparel) ? apparel.Name : null)
                .Where(x => x != null);

            // var storedItems = _inventory.StoredItems.Select(x => new ItemInfo
            // {
            //     ItemName = x.Name,
            //     IsInStorage = true
            // });
            //
            // var holsteredItems = _inventory.Holsters
            //     .Select(x => x.CurrentItem.HasSome(out var item)
            //         ? new ItemInfo()
            //         {
            //             ItemName = item.Name,
            //             HolsterName = x.Name
            //         }
            //         : null)
            //     .Where(x => x != null);
            //
            // var heldItems = _inventory.LeftHand.CurrentItem.Map(x => new ItemInfo()
            // {
            //     ItemName = x.Name,
            //     IsInLeftHand = true
            // }).AsEnumerable().Concat(
            //     _inventory.RightHand.CurrentItem.Map(x => new ItemInfo()
            //     {
            //         ItemName = x.Name,
            //         IsInRightHand = true
            //     }).AsEnumerable());

            var items = GetItemsInfo();

            return new InventoryInfo(items, equippedApparel);
        }

        IEnumerable<ItemInfo> GetItemsInfo()
        {
            var items = _inventory
                .StoredItems
                .Concat(_inventory.Holsters.SelectMany(x => x.CurrentItem.AsEnumerable()))
                .Concat(_inventory.LeftHand.CurrentItem.AsEnumerable())
                .Concat(_inventory.RightHand.CurrentItem.AsEnumerable())
                .Distinct();

            return items.Select(x => new ItemInfo()
            {
                ItemName = x.Name,
                HolsterName = x.Holsters
                    .FirstOrNone(y => y.IsEquipped)
                    .Map(y => y.HolsterName)
                    .Reduce(() => string.Empty),
                IsInStorage = _inventory.StoredItems.Contains(x),
                IsInLeftHand = _inventory.LeftHand.IsCurrentItem(x),
                IsInRightHand = _inventory.RightHand.IsCurrentItem(x)
            });
        }
    }

    public class InventoryInfo
    {
        public InventoryInfo(
            IEnumerable<ItemInfo> items,
            IEnumerable<string> equippedApparel)
        {
            Items = items.ToArray();
            EquippedApparel = equippedApparel.ToArray();
        }

        public IReadOnlyList<ItemInfo> Items { get; }

        public IReadOnlyList<string> EquippedApparel { get; }
    }

    public class ItemInfo
    {
        public string ItemName { get; internal set; }

        public string HolsterName { get; internal set; } = string.Empty;

        public bool IsInStorage { get; internal set; }

        public bool IsInLeftHand { get; internal set; }

        public bool IsInRightHand { get; internal set; }
    }
}