using Strawhenge.Inventory.Unity.Apparel;
using System.Collections.Generic;
using System.Linq;

namespace Strawhenge.Inventory.Unity.Loader
{
    public class InventoryLoadDataGenerator
    {
        readonly ApparelManager _apparelManager;

        public InventoryLoadDataGenerator(ApparelManager apparelManager)
        {
            _apparelManager = apparelManager;
        }

        public InventoryLoadData GenerateCurrentLoadData()
        {
            return new InventoryLoadData
            {
                EquippedApparel = GetEquippedApparelNames().ToArray()
            };
        }

        IEnumerable<string> GetEquippedApparelNames()
        {
            foreach (var slot in _apparelManager.Slots)
            {
                if (slot.CurrentPiece.HasSome(out var apparelPiece))
                    yield return apparelPiece.Name;
            }
        }
    }
}