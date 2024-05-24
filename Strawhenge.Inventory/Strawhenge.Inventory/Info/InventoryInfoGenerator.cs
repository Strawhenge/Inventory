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
            var items = GetItemsInfo();
            var equippedApparel = GetEquippedApparel();

            return new InventoryInfo(items, equippedApparel);
        }

        IEnumerable<ItemInfo> GetItemsInfo()
        {
            return _inventory.StoredItems
                .Concat(_inventory.Holsters.SelectMany(x => x.CurrentItem.AsEnumerable()))
                .Concat(_inventory.LeftHand.CurrentItem.AsEnumerable())
                .Concat(_inventory.RightHand.CurrentItem.AsEnumerable())
                .Distinct()
                .Select(item => new ItemInfo(
                    item.Name,
                    holsterName: item.Holsters
                        .FirstOrNone(x => x.IsEquipped)
                        .Map(x => x.HolsterName)
                        .Reduce(() => string.Empty),
                    isInStorage: _inventory.StoredItems.Contains(item),
                    isInLeftHand: _inventory.LeftHand.IsCurrentItem(item),
                    isInRightHand: _inventory.RightHand.IsCurrentItem(item)
                ));
        }

        IEnumerable<string> GetEquippedApparel()
        {
            return _inventory.ApparelSlots
                .Select(x => x.CurrentPiece.HasSome(out var apparel) ? apparel.Name : null)
                .Where(x => x != null);
        }
    }
}