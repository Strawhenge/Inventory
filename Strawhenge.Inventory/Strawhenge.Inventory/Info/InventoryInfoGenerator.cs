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
                .Select(x => new ItemInfo(x.Name)
                {
                    HolsterName = x.Holsters
                        .FirstOrNone(y => y.IsEquipped)
                        .Map(y => y.HolsterName)
                        .Reduce(() => string.Empty),
                    IsInStorage = _inventory.StoredItems.Contains(x),
                    IsInLeftHand = _inventory.LeftHand.IsCurrentItem(x),
                    IsInRightHand = _inventory.RightHand.IsCurrentItem(x)
                });
        }

        IEnumerable<string> GetEquippedApparel()
        {
            return _inventory.ApparelSlots
                .Select(x => x.CurrentPiece.HasSome(out var apparel) ? apparel.Name : null)
                .Where(x => x != null);
        }
    }
}