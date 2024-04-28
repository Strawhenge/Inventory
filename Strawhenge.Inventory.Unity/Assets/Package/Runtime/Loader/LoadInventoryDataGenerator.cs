using System.Collections.Generic;
using System.Linq;

namespace Strawhenge.Inventory.Unity.Loader
{
    public class LoadInventoryDataGenerator
    {
        readonly IInventory _inventory;

        public LoadInventoryDataGenerator(IInventory inventory)
        {
            _inventory = inventory;
        }

        public LoadInventoryData GenerateCurrentLoadData()
        {
            return new LoadInventoryData
            {
                HolsteredItems = GetHolsteredItems().ToArray(),
                EquippedApparel = GetEquippedApparelNames().ToArray()
            };
        }

        IEnumerable<HolsteredItemLoadInventoryData> GetHolsteredItems()
        {
            foreach (var holster in _inventory.Holsters)
            {
                if (holster.CurrentItem.HasSome(out var item))
                    yield return new HolsteredItemLoadInventoryData(item.Name, holster.Name);
            }
        }

        IEnumerable<string> GetEquippedApparelNames()
        {
            foreach (var slot in _inventory.ApparelSlots)
            {
                if (slot.CurrentPiece.HasSome(out var apparelPiece))
                    yield return apparelPiece.Name;
            }
        }
    }
}